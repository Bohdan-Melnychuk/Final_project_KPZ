# ♟️ Chess WPF

A two-player chess game built with **C# and WPF (.NET Framework)**, featuring full chess rules, a graphical interface, and a countdown timer for each player.

---

## 🚀 How to Run Locally

1. Clone the repository:
   ```bash
   git clone https://github.com/Bohdan-Melnychuk/Final_project_KPZ.git
   ```
2. Open `kursach wpf .framework.sln` in **Visual Studio 2022**
3. Set `kursach wpf .framework` as the startup project
4. Press **F5** to build and run

**Requirements:** Visual Studio 2022, .NET Framework 4.7.2

---

## 🎮 Features

### User Interface
- **Start screen** — launch game or exit with confirmation dialog ([start.xaml.cs](kursach%20wpf%20.framework/start.xaml.cs))
- **Chessboard** — rendered on WPF Canvas with alternating colored tiles and a decorative frame ([Board.cs](kursach%20wpf%20.framework/Board.cs))
- **Move markers** — dots for valid moves, attack icons for enemy captures, red highlight when King is in check ([Board.cs](kursach%20wpf%20.framework/Board.cs))
- **Pawn promotion dialog** — interactive popup to choose Queen, Rook, Bishop, or Knight ([ChoosingFigure.xaml.cs](kursach%20wpf%20.framework/ChoosingFigure.xaml.cs))
- **Game result screen** — displays winner or draw with a return-to-menu button ([Page1.xaml.cs](kursach%20wpf%20.framework/Page1.xaml.cs))

### Chess Logic
- All 6 piece types with correct movement rules: King, Queen, Rook, Bishop, Knight, Pawn ([King.cs](kursach%20wpf%20.framework/King.cs), [Queen.cs](kursach%20wpf%20.framework/Queen.cs), [Rook.cs](kursach%20wpf%20.framework/Rook.cs), [Bishop.cs](kursach%20wpf%20.framework/Bishop.cs), [Knight.cs](kursach%20wpf%20.framework/Knight.cs), [Pawn.cs](kursach%20wpf%20.framework/Pawn.cs))
- **Check detection** — all moves that leave the King in check are blocked ([Figure.cs](kursach%20wpf%20.framework/Figure.cs))
- **Checkmate & stalemate detection** ([Board.cs](kursach%20wpf%20.framework/Board.cs))
- **Castling** — both kingside and queenside ([King.cs](kursach%20wpf%20.framework/King.cs))
- **Pawn promotion** — async dialog on reaching the last rank ([Pawn.cs](kursach%20wpf%20.framework/Pawn.cs))
- **Draw conditions** — insufficient material (≤3 pieces) and 50-move rule ([Board.cs](kursach%20wpf%20.framework/Board.cs))

### Timer
- Each player has a **5-minute countdown** timer displayed on screen
- Timers switch automatically after each move
- Time running out triggers an immediate win for the opponent ([Board.cs](kursach%20wpf%20.framework/Board.cs))

---

## 🏗️ Project Structure

```
kursach wpf .framework/
├── Figure.cs              # Abstract base class for all chess pieces
├── Board.cs               # Game board, move logic, timer, game state
├── Cell.cs                # Board cell representation
├── King.cs                # King movement, castling, check avoidance
├── Queen.cs               # Queen movement and defensive logic
├── Rook.cs                # Rook movement
├── Bishop.cs              # Bishop movement
├── Knight.cs              # Knight movement
├── Pawn.cs                # Pawn movement, promotion
├── MainWindow.xaml(.cs)   # Main game window
├── start.xaml(.cs)        # Start screen
├── Page1.xaml(.cs)        # Game result screen
├── ChoosingFigure.xaml.cs # Pawn promotion dialog
└── Image/                 # PNG icons for all chess pieces
```

---

## 🧩 Design Patterns

### 1. Template Method — [`Figure.cs`](kursach%20wpf%20.framework/Figure.cs)
The abstract class `Figure` defines the skeleton algorithm for move validation in `CanMoveTo()`:
1. Check board boundaries
2. Check friendly piece collision
3. Call abstract `CanReach()` (overridden per piece)
4. Simulate move and check if King would be in check via `WouldKingBeInCheck()`

Each piece subclass overrides only `CanReach()` and `MoveFigure()`, keeping the validation algorithm centralized.

### 2. Strategy — [`Queen.cs`](kursach%20wpf%20.framework/Queen.cs), [`Rook.cs`](kursach%20wpf%20.framework/Rook.cs), [`Bishop.cs`](kursach%20wpf%20.framework/Bishop.cs), [`Knight.cs`](kursach%20wpf%20.framework/Knight.cs), [`Pawn.cs`](kursach%20wpf%20.framework/Pawn.cs)
Each piece implements its own `MoveFigure()` strategy, switching between `StandardMoves()` and `DefensiveMoves()` depending on whether the King is in check. The `Board` calls `MoveFigure()` uniformly through the `Figure` interface, unaware of the concrete strategy.

### 3. Composite — [`Board.cs`](kursach%20wpf%20.framework/Board.cs)
The `Board` manages the entire collection of `Figure` objects via `ArrFigure[,]` and treats them uniformly through the abstract `Figure` interface — calling `FillArrayAttacksCell()`, `MoveFigure()`, and `IsAttacking()` on all pieces without knowing their concrete types. `GetAllFigures()` returns a flat list for uniform iteration.

---

## 🔧 Programming Principles

### 1. Single Responsibility Principle (SRP)
Each class has one reason to change:
- `Figure` — piece data and movement validation only ([Figure.cs](kursach%20wpf%20.framework/Figure.cs))
- `Board` — board state, rendering, and game flow ([Board.cs](kursach%20wpf%20.framework/Board.cs))
- `ChoosingFigure` — pawn promotion UI only ([ChoosingFigure.xaml.cs](kursach%20wpf%20.framework/ChoosingFigure.xaml.cs))
- `Page1` — result display only ([Page1.xaml.cs](kursach%20wpf%20.framework/Page1.xaml.cs))

### 2. Open/Closed Principle (OCP)
`Figure` is open for extension (new piece types) but closed for modification. Adding a new piece (e.g. `Archbishop`) requires only creating a new subclass and overriding `IsAttacking()`, `MoveFigure()`, and `FillArrayAttacksCell()` — no changes to `Board` or existing pieces. ([Figure.cs](kursach%20wpf%20.framework/Figure.cs))

### 3. Liskov Substitution Principle (LSP)
All piece subclasses (`King`, `Queen`, `Rook`, `Bishop`, `Knight`, `Pawn`) can be used anywhere a `Figure` is expected. `Board.GetAllFigures()` returns `List<Figure>` and calls virtual methods polymorphically without needing to know concrete types. ([Board.cs](kursach%20wpf%20.framework/Board.cs))

### 4. DRY (Don't Repeat Yourself)
Shared movement scanning logic is extracted into `ScanDiagonal()` and `ScanStraight()` in the base `Figure` class, reused by Queen, Rook, and Bishop without duplication. `CheckCell()` centralizes boundary and collision checking. ([Figure.cs](kursach%20wpf%20.framework/Figure.cs))

### 5. Fail Fast
`WouldKingBeInCheck()` simulates a move on the board, immediately checks if it leaves the King in check, and reverts — blocking invalid moves before they are shown to the player. ([Figure.cs](kursach%20wpf%20.framework/Figure.cs))

### 6. Separation of Concerns
Game logic (`Board`, `Figure` subclasses) is fully separated from UI (`MainWindow`, `Page1`, `start`). The board renders markers and figures on a WPF Canvas but delegates all chess rules to the model layer.

---

## 🔨 Refactoring Techniques

### 1. Extract Method
Long movement logic in each piece was split into private `StandardMoves()` and `DefensiveMoves()` methods, and shared scanning was extracted into `ScanDiagonal()` / `ScanStraight()` in the base class. ([Figure.cs](kursach%20wpf%20.framework/Figure.cs), [Queen.cs](kursach%20wpf%20.framework/Queen.cs))

### 2. Extract Superclass
Common fields (`Color`, `X`, `Y`, `ImageFigure`, `CellsUnderAttack`) and shared methods (`CheckCell`, `WouldKingBeInCheck`, `IsBlockedPath`, `ScanDiagonal`, `ScanStraight`) were pulled into the abstract `Figure` base class, eliminating duplication across all 6 piece types. ([Figure.cs](kursach%20wpf%20.framework/Figure.cs))

### 3. Replace Conditional with Polymorphism
Instead of a large `if/else` or `switch` on piece type for movement and attack logic, each piece class overrides `IsAttacking()`, `MoveFigure()`, and `FillArrayAttacksCell()`. The board calls these methods polymorphically. ([Board.cs](kursach%20wpf%20.framework/Board.cs))

### 4. Introduce Parameter Object
Move scanning methods use `(startX, startY, stepX, stepY)` as a consistent parameter group representing direction vectors, avoiding scattered individual coordinate arithmetic throughout piece classes. ([Figure.cs](kursach%20wpf%20.framework/Figure.cs))

### 5. Separate Query from Modifier
Read-only queries like `IsAttacking()`, `IsKingInCheck()`, `IsSquareUnderAttack()`, `GetAllFigures()`, `GetKing()` are kept side-effect-free and separate from state-modifying methods like `MoveFigure()` and `RefreshAllAttackCell()`. ([Board.cs](kursach%20wpf%20.framework/Board.cs))

### 6. Replace Magic Number with Named Concept
Board size `8` is stored as `board.boardSize` and tile size as `board.tileSize`, so resizing the board requires changing only one place. ([Board.cs](kursach%20wpf%20.framework/Board.cs))
