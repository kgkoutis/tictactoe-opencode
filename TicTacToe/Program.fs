open System
open System.Threading
open Game
open UI

let mutable lastBoard = Array2D.create 3 3 Empty

let boardsEqual (board1: Board) (board2: Board) : bool =
    let mutable equal = true
    for row in 0..2 do
        for col in 0..2 do
            if board1.[row, col] <> board2.[row, col] then
                equal <- false
    equal

let setCursorPosition row col =
    Console.SetCursorPosition(col, row)

let optimizedPrintBoard (board: Board) =
    if not (boardsEqual board lastBoard) then
        clearScreen()
        printTitle()
        printBoard board
        Array2D.blit board 0 0 lastBoard 0 0 3 3

let rec gameLoop (board: Board) (currentPlayer: Player) (gameMode: GameMode) : unit =
    optimizedPrintBoard board
    
    let gameState = getGameState board currentPlayer
    printGameState gameState (Some gameMode)
    
    match gameState with
    | Playing player ->
        match gameMode with
        | PlayerVsComputer difficulty when player = O ->
            Thread.Sleep(1000)
            match getComputerMove board player difficulty with
            | Some (row, col) ->
                match makeMove board (row, col) player with
                | Some newBoard ->
                    let nextPlayer = switchPlayer player
                    gameLoop newBoard nextPlayer gameMode
                | None ->
                    printError "Computer made an invalid move!"
                    waitForKeyPress()
                    gameLoop board currentPlayer gameMode
            | None ->
                printError "No valid moves available for computer!"
                waitForKeyPress()
                gameLoop board currentPlayer gameMode
        | _ ->
            printInstructions (Some gameMode)
            let input = getUserInput()
            
            if input.ToLower() = "quit" then
                ()
            else
                match parseMove input with
                | Some (row, col) ->
                    match makeMove board (row, col) player with
                    | Some newBoard ->
                        let nextPlayer = switchPlayer player
                        gameLoop newBoard nextPlayer gameMode
                    | None ->
                        printError "Invalid move! Cell is already occupied or position is out of bounds."
                        waitForKeyPress()
                        gameLoop board currentPlayer gameMode
                | None ->
                    printError "Invalid input! Please enter row and column numbers (0-2)."
                    waitForKeyPress()
                    gameLoop board currentPlayer gameMode
    
    | Won _ | Draw ->
        printGameOverOptions gameMode
        let choice = getUserInput()
        match gameMode, choice with
        | PlayerVsPlayer, "1" -> startNewGame gameMode
        | PlayerVsPlayer, "2" -> showMenu()
        | PlayerVsPlayer, "3" -> ()
        | PlayerVsComputer difficulty, "1" -> startNewGame gameMode
        | PlayerVsComputer difficulty, "2" -> 
            match changeDifficulty difficulty with
            | Some newDifficulty -> startNewGame (PlayerVsComputer newDifficulty)
            | None -> gameLoop board currentPlayer gameMode
        | PlayerVsComputer _, "3" -> showMenu()
        | PlayerVsComputer _, "4" -> ()
        | _ ->
            let maxChoice = match gameMode with PlayerVsPlayer -> "3" | PlayerVsComputer _ -> "4"
            printError (sprintf "Invalid choice! Please enter 1-%s." maxChoice)
            waitForKeyPress()
            gameLoop board currentPlayer gameMode

and changeDifficulty (currentDifficulty: Difficulty) : Difficulty option =
    clearScreen()
    printDifficultyChangeMenu currentDifficulty
    let choice = getUserInput()
    
    match choice with
    | "1" -> Some Easy
    | "2" -> Some Medium  
    | "3" -> Some Hard
    | "4" -> Some currentDifficulty
    | "5" -> None
    | _ ->
        printError "Invalid choice! Please enter 1, 2, 3, 4, or 5."
        waitForKeyPress()
        changeDifficulty currentDifficulty

and startNewGame (gameMode: GameMode) : unit =
    let board = createBoard()
    lastBoard <- Array2D.create 3 3 Empty
    gameLoop board X gameMode

and selectDifficulty () : Difficulty option =
    clearScreen()
    printDifficultySelection()
    let choice = getUserInput()
    
    match choice with
    | "1" -> Some Easy
    | "2" -> Some Medium
    | "3" -> Some Hard
    | "4" -> None
    | _ ->
        printError "Invalid choice! Please enter 1, 2, 3, or 4."
        waitForKeyPress()
        selectDifficulty()

and selectGameMode () : GameMode option =
    clearScreen()
    printGameModeSelection()
    let choice = getUserInput()
    
    match choice with
    | "1" -> Some PlayerVsPlayer
    | "2" -> 
        match selectDifficulty() with
        | Some difficulty -> Some (PlayerVsComputer difficulty)
        | None -> selectGameMode()
    | "3" -> None
    | _ ->
        printError "Invalid choice! Please enter 1, 2, or 3."
        waitForKeyPress()
        selectGameMode()

and showMenu () : unit =
    clearScreen()
    printMenu()
    let choice = getUserInput()
    
    match choice with
    | "1" -> 
        match selectGameMode() with
        | Some gameMode -> startNewGame gameMode
        | None -> showMenu()
    | "2" -> startNewGame (PlayerVsComputer Easy)
    | "3" -> startNewGame (PlayerVsComputer Medium)
    | "4" -> startNewGame (PlayerVsComputer Hard)
    | "5" -> ()
    | _ ->
        printError "Invalid choice! Please enter 1, 2, 3, 4, or 5."
        waitForKeyPress()
        showMenu()

[<EntryPoint>]
let main argv =
    try
        Console.OutputEncoding <- Text.Encoding.UTF8
        Console.Title <- "F# TUI Tic Tac Toe"
        Console.CursorVisible <- false
        showMenu()
        0
    with
    | ex ->
        printfn "An error occurred: %s" ex.Message
        1