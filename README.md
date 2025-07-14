# F# TUI Tic Tac Toe

A modern, feature-rich terminal-based Tic Tac Toe game built with F# and functional programming principles.

![F# TUI Tic Tac Toe](https://img.shields.io/badge/F%23-TUI-blue) ![.NET](https://img.shields.io/badge/.NET-9.0-purple) ![License](https://img.shields.io/badge/license-MIT-green)

## 🎮 Features

### Game Modes
- **Player vs Player** - Classic two-player mode
- **Player vs Computer** - Challenge AI with three difficulty levels:
  - 🟢 **Easy**: Random move selection
  - 🟡 **Medium**: 70% optimal play, 30% random moves
  - 🔴 **Hard**: Perfect minimax algorithm (unbeatable)

### User Experience
- **Colorful TUI** with UTF-8 box drawing characters
- **Smart rendering** - Only redraws when board changes
- **Dynamic difficulty adjustment** - Change AI difficulty after each game
- **Intuitive menus** with clear navigation
- **Real-time feedback** and error handling

### Technical Features
- **Immutable data structures** following F# best practices
- **Minimax algorithm** with depth-based scoring for optimal AI play
- **Pattern matching** for clean, readable code
- **Optimized console rendering** for smooth performance

## 🚀 Quick Start

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later

### Installation & Running
```bash
# Clone the repository
git clone <repository-url>
cd TicTacToe

# Build the project
dotnet build

# Run the game
dotnet run
```

## 🎯 How to Play

1. **Choose game mode** from the main menu
2. **Select difficulty** (for Player vs Computer mode)
3. **Make moves** by entering row and column coordinates (0-2)
4. **Adjust difficulty** after each game without returning to main menu
5. **Type 'quit'** during gameplay to exit

### Controls
- Enter moves as: `row col` (e.g., `1 2` for center-right)
- Navigate menus with number keys
- Type `quit` to exit during gameplay

## 🏗️ Project Structure

```
TicTacToe/
├── TicTacToe.fsproj    # F# project file
├── Game.fs             # Core game logic and AI
├── UI.fs               # Terminal UI rendering
└── Program.fs          # Main entry point and game loop
```

## 🧠 AI Implementation

The computer player uses different strategies based on difficulty:

- **Easy**: Completely random move selection
- **Medium**: Probabilistic mix of optimal and random moves
- **Hard**: Minimax algorithm with recursive game tree evaluation

The minimax implementation includes:
- Depth-based scoring for optimal play
- Complete game state evaluation
- Unbeatable perfect play on Hard difficulty

## 🛠️ Development

### Building
```bash
dotnet build
```

### Running Tests
```bash
dotnet test  # (when tests are added)
```

### Code Style
This project follows F# best practices:
- Immutable data structures
- Pattern matching over conditionals
- Function composition
- Discriminated unions for type safety

## 🤖 Development Credits

This project was developed iteratively using:
- **[OpenCode](https://opencode.ai)** - AI-powered development environment
- **Claude 4 Sonnet** - Advanced AI assistant for code generation and architecture

The entire project was built through collaborative AI-assisted development, demonstrating the power of modern AI tools in software creation.

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🎨 Screenshots

```
╔═══════════════════════════════╗
║        TIC TAC TOE            ║
╚═══════════════════════════════╝

   0   1   2
0  X │   │ O 
  ───┼───┼───
1    │ X │   
  ───┼───┼───
2  O │   │ X 

Player X's turn
```

## 🚀 Future Enhancements

- [ ] Network multiplayer support
- [ ] Game statistics and win tracking
- [ ] Custom board sizes (4x4, 5x5)
- [ ] Tournament mode
- [ ] Save/load game states
- [ ] Advanced AI personalities

---

*Built with ❤️ and F# functional programming*