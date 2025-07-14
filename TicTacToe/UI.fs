module UI

open System
open Game

let clearScreen () =
    Console.Clear()

let setColor color =
    Console.ForegroundColor <- color

let resetColor () =
    Console.ResetColor()

let printTitle () =
    setColor ConsoleColor.Cyan
    printfn "╔═══════════════════════════════╗"
    printfn "║        TIC TAC TOE            ║"
    printfn "╚═══════════════════════════════╝"
    resetColor()
    printfn ""

let playerToString (player: Player) : string =
    match player with
    | X -> "X"
    | O -> "O"

let playerToChar (player: Player) : char =
    match player with
    | X -> 'X'
    | O -> 'O'

let cellToDisplay (cell: Cell) : char =
    match cell with
    | Empty -> ' '
    | Occupied player -> playerToChar player

let printBoard (board: Board) =
    setColor ConsoleColor.Yellow
    printfn "   0   1   2"
    resetColor()
    
    for row in 0..2 do
        setColor ConsoleColor.Yellow
        printf "%d " row
        resetColor()
        
        for col in 0..2 do
            let cell = cellToDisplay board.[row, col]
            match board.[row, col] with
            | Occupied X -> 
                setColor ConsoleColor.Red
                printf " %c " cell
                resetColor()
            | Occupied O -> 
                setColor ConsoleColor.Blue
                printf " %c " cell
                resetColor()
            | Empty -> 
                setColor ConsoleColor.DarkGray
                printf " %c " cell
                resetColor()
            
            if col < 2 then
                setColor ConsoleColor.White
                printf "│"
                resetColor()
        
        printfn ""
        if row < 2 then
            setColor ConsoleColor.White
            printfn "  ───┼───┼───"
            resetColor()
    
    printfn ""

let printGameState (state: GameState) (gameMode: GameMode option) =
    match state with
    | Playing player ->
        setColor ConsoleColor.Green
        match gameMode with
        | Some (PlayerVsComputer _) when player = O ->
            printfn "Computer (O) is thinking..."
        | _ ->
            printfn "Player %s's turn" (playerToString player)
        resetColor()
    | Won player ->
        setColor ConsoleColor.Magenta
        match gameMode with
        | Some (PlayerVsComputer _) when player = O ->
            printfn "🤖 Computer wins! 🤖"
        | Some (PlayerVsComputer _) when player = X ->
            printfn "🎉 You win! 🎉"
        | _ ->
            printfn "🎉 Player %s wins! 🎉" (playerToString player)
        resetColor()
    | Draw ->
        setColor ConsoleColor.Yellow
        printfn "It's a draw! 🤝"
        resetColor()

let printInstructions (gameMode: GameMode option) =
    setColor ConsoleColor.Gray
    match gameMode with
    | Some (PlayerVsComputer _) ->
        printfn "Enter your move as: row col (e.g., '1 2' for row 1, column 2)"
        printfn "You are X, Computer is O"
    | _ ->
        printfn "Enter your move as: row col (e.g., '1 2' for row 1, column 2)"
    printfn "Or type 'quit' to exit the game"
    resetColor()

let printError (message: string) =
    setColor ConsoleColor.Red
    printfn "Error: %s" message
    resetColor()

let getUserInput () : string =
    setColor ConsoleColor.White
    printf "> "
    resetColor()
    Console.ReadLine()

let parseMove (input: string) : Position option =
    let parts = input.Trim().Split([|' '; '\t'|], StringSplitOptions.RemoveEmptyEntries)
    if parts.Length = 2 then
        match Int32.TryParse(parts.[0]), Int32.TryParse(parts.[1]) with
        | (true, row), (true, col) -> Some (row, col)
        | _ -> None
    else
        None

let printMenu () =
    setColor ConsoleColor.Cyan
    printfn "╔═══════════════════════════════╗"
    printfn "║            MENU               ║"
    printfn "╠═══════════════════════════════╣"
    printfn "║ 1. Player vs Player           ║"
    printfn "║ 2. Player vs Computer (Easy)  ║"
    printfn "║ 3. Player vs Computer (Medium)║"
    printfn "║ 4. Player vs Computer (Hard)  ║"
    printfn "║ 5. Quit                       ║"
    printfn "╚═══════════════════════════════╝"
    resetColor()

let printGameModeSelection () =
    setColor ConsoleColor.Cyan
    printfn "╔═══════════════════════════════╗"
    printfn "║        GAME MODE              ║"
    printfn "╠═══════════════════════════════╣"
    printfn "║ 1. Player vs Player           ║"
    printfn "║ 2. Player vs Computer         ║"
    printfn "║ 3. Return to main menu        ║"
    printfn "╚═══════════════════════════════╝"
    resetColor()

let printDifficultySelection () =
    setColor ConsoleColor.Cyan
    printfn "╔═══════════════════════════════╗"
    printfn "║        DIFFICULTY             ║"
    printfn "╠═══════════════════════════════╣"
    printfn "║ 1. Easy (Random moves)        ║"
    printfn "║ 2. Medium (Mixed strategy)    ║"
    printfn "║ 3. Hard (Perfect play)        ║"
    printfn "║ 4. Return to game mode        ║"
    printfn "╚═══════════════════════════════╝"
    resetColor()

let difficultyToString (difficulty: Difficulty) : string =
    match difficulty with
    | Easy -> "Easy"
    | Medium -> "Medium"
    | Hard -> "Hard"

let gameModeToString (gameMode: GameMode) : string =
    match gameMode with
    | PlayerVsPlayer -> "Player vs Player"
    | PlayerVsComputer difficulty -> sprintf "Player vs Computer (%s)" (difficultyToString difficulty)

let printGameOverOptions (gameMode: GameMode) =
    printfn ""
    setColor ConsoleColor.Green
    printfn "What would you like to do?"
    printfn "1. Play again"
    match gameMode with
    | PlayerVsComputer difficulty ->
        printfn "2. Change difficulty (%s)" (difficultyToString difficulty)
        printfn "3. Return to menu"
        printfn "4. Quit"
    | PlayerVsPlayer ->
        printfn "2. Return to menu"
        printfn "3. Quit"
    resetColor()

let printDifficultyChangeMenu (currentDifficulty: Difficulty) =
    setColor ConsoleColor.Cyan
    printfn "╔═══════════════════════════════╗"
    printfn "║      CHANGE DIFFICULTY        ║"
    printfn "╠═══════════════════════════════╣"
    printfn "║ Current: %-20s║" (difficultyToString currentDifficulty)
    printfn "╠═══════════════════════════════╣"
    printfn "║ 1. Easy (Random moves)        ║"
    printfn "║ 2. Medium (Mixed strategy)    ║"
    printfn "║ 3. Hard (Perfect play)        ║"
    printfn "║ 4. Keep current & play again  ║"
    printfn "║ 5. Return to game over menu   ║"
    printfn "╚═══════════════════════════════╝"
    resetColor()

let waitForKeyPress () =
    setColor ConsoleColor.Gray
    printfn "Press any key to continue..."
    resetColor()
    Console.ReadKey() |> ignore
    printfn ""