# Agent Guidelines for F# TUI Tic Tac Toe

## Build/Test Commands
- Build: `dotnet build`
- Run: `dotnet run`
- Test: `dotnet test` (if tests are added)
- Clean: `dotnet clean`
- Restore packages: `dotnet restore`

## Project Structure
- `TicTacToe/TicTacToe.fsproj` - F# project file
- `TicTacToe/Game.fs` - Core game logic, AI, and state management
- `TicTacToe/UI.fs` - Terminal UI rendering and input handling  
- `TicTacToe/Program.fs` - Main entry point and game loop

## Game Features
- **Player vs Player** - Traditional two-player mode
- **Player vs Computer** with 3 difficulty levels:
  - Easy: Random moves
  - Medium: 70% optimal moves, 30% random
  - Hard: Perfect minimax algorithm play
- **Optimized rendering** - Only redraws when board changes
- **Colorful TUI** - Different colors for X, O, and UI elements

## Code Style Guidelines
- Use F# naming conventions: PascalCase for types, camelCase for functions/values
- Prefer immutable data structures and functional programming patterns
- Use pattern matching extensively for control flow
- Keep functions pure when possible, isolate side effects
- Use meaningful names for functions and values
- Prefer composition over inheritance
- Use type annotations for public APIs
- Handle all cases in pattern matching (avoid incomplete patterns)
- Use pipe operator |> for data flow
- Prefer discriminated unions over class hierarchies
- Keep modules focused and cohesive

## F# Specific Guidelines
- Use `let` for immutable bindings, `let mutable` only when necessary
- Prefer `List` over `Array` unless performance is critical
- Use `Option` type instead of null values
- Use `Result` type for error handling
- Prefer functional composition over imperative loops
- Use active patterns for complex pattern matching
- Keep indentation consistent (4 spaces)

## AI Implementation
- Uses minimax algorithm for optimal play
- Recursive evaluation with depth-based scoring
- Computer plays as O, human as X
- Thread.Sleep for realistic computer "thinking" time

## Before Committing
- Run `dotnet build` to ensure compilation
- Run `dotnet run` to test functionality
- Test all game modes and difficulty levels
- Ensure no compiler warnings
- Check that all pattern matches are complete