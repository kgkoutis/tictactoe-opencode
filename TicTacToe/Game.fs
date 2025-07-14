module Game

type Player = X | O

type Cell = Empty | Occupied of Player

type Board = Cell[,]

type GameState = 
    | Playing of Player
    | Won of Player
    | Draw

type Position = int * int

let createBoard () : Board =
    Array2D.create 3 3 Empty

let isValidPosition (row, col) : bool =
    row >= 0 && row < 3 && col >= 0 && col < 3

let isCellEmpty (board: Board) (row, col) : bool =
    match board.[row, col] with
    | Empty -> true
    | _ -> false

let makeMove (board: Board) (row, col) (player: Player) : Board option =
    if isValidPosition (row, col) && isCellEmpty board (row, col) then
        let newBoard = Array2D.copy board
        newBoard.[row, col] <- Occupied player
        Some newBoard
    else
        None

let checkWinner (board: Board) : Player option =
    let lines = [
        // Rows
        [(0,0); (0,1); (0,2)]
        [(1,0); (1,1); (1,2)]
        [(2,0); (2,1); (2,2)]
        // Columns
        [(0,0); (1,0); (2,0)]
        [(0,1); (1,1); (2,1)]
        [(0,2); (1,2); (2,2)]
        // Diagonals
        [(0,0); (1,1); (2,2)]
        [(0,2); (1,1); (2,0)]
    ]
    
    let checkLine positions =
        let cells = positions |> List.map (fun (r, c) -> board.[r, c])
        match cells with
        | [Occupied p1; Occupied p2; Occupied p3] when p1 = p2 && p2 = p3 -> Some p1
        | _ -> None
    
    lines |> List.tryPick checkLine

let isBoardFull (board: Board) : bool =
    let mutable full = true
    for row in 0..2 do
        for col in 0..2 do
            if board.[row, col] = Empty then
                full <- false
    full

let getGameState (board: Board) (currentPlayer: Player) : GameState =
    match checkWinner board with
    | Some winner -> Won winner
    | None ->
        if isBoardFull board then
            Draw
        else
            Playing currentPlayer

let switchPlayer (player: Player) : Player =
    match player with
    | X -> O
    | O -> X

let getAllEmptyPositions (board: Board) : Position list =
    [ for row in 0..2 do
        for col in 0..2 do
            if board.[row, col] = Empty then
                yield (row, col) ]

type Difficulty = Easy | Medium | Hard

type GameMode = PlayerVsPlayer | PlayerVsComputer of Difficulty

let evaluateBoard (board: Board) (player: Player) : int =
    match checkWinner board with
    | Some winner when winner = player -> 10
    | Some _ -> -10
    | None -> 0

let rec minimax (board: Board) (depth: int) (isMaximizing: bool) (player: Player) : int =
    let score = evaluateBoard board player
    
    if score = 10 then score - depth
    elif score = -10 then score + depth
    elif isBoardFull board then 0
    else
        let emptyPositions = getAllEmptyPositions board
        if isMaximizing then
            emptyPositions
            |> List.map (fun (row, col) ->
                match makeMove board (row, col) player with
                | Some newBoard -> minimax newBoard (depth + 1) false player
                | None -> -1000)
            |> List.max
        else
            let opponent = switchPlayer player
            emptyPositions
            |> List.map (fun (row, col) ->
                match makeMove board (row, col) opponent with
                | Some newBoard -> minimax newBoard (depth + 1) true player
                | None -> 1000)
            |> List.min

let getBestMove (board: Board) (player: Player) : Position option =
    let emptyPositions = getAllEmptyPositions board
    if emptyPositions.IsEmpty then None
    else
        let bestMove = 
            emptyPositions
            |> List.map (fun (row, col) ->
                match makeMove board (row, col) player with
                | Some newBoard -> 
                    let score = minimax newBoard 0 false player
                    ((row, col), score)
                | None -> ((row, col), -1000))
            |> List.maxBy snd
            |> fst
        Some bestMove

let getRandomMove (board: Board) : Position option =
    let emptyPositions = getAllEmptyPositions board
    if emptyPositions.IsEmpty then None
    else
        let random = System.Random()
        let index = random.Next(emptyPositions.Length)
        Some emptyPositions.[index]

let getComputerMove (board: Board) (player: Player) (difficulty: Difficulty) : Position option =
    let emptyPositions = getAllEmptyPositions board
    if emptyPositions.IsEmpty then None
    else
        match difficulty with
        | Easy -> getRandomMove board
        | Medium ->
            let random = System.Random()
            if random.NextDouble() < 0.7 then
                getBestMove board player
            else
                getRandomMove board
        | Hard -> getBestMove board player