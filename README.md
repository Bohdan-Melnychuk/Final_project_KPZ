# ♟️ Chess WPF

Двогравцева шахова гра, написана на **C# та WPF (.NET Framework)**, з повною реалізацією правил шахів, графічним інтерфейсом та таймером відліку часу для кожного гравця.

---

## 🚀 Як запустити локально

1. Клонуй репозиторій:
   ```bash
   git clone https://github.com/Bohdan-Melnychuk/Final_project_KPZ.git
   ```
2. Відкрий `kursach wpf .framework.sln` у **Visual Studio 2022**
3. Встанови `kursach wpf .framework` як стартовий проєкт
4. Натисни **F5** для збірки та запуску

**Вимоги:** Visual Studio 2022, .NET Framework 4.7.2

---

## 🎮 Функціональність

### Інтерфейс користувача
- **Стартовий екран** — запуск гри або вихід з діалогом підтвердження ([start.xaml.cs](kursach%20wpf%20.framework/start.xaml.cs))
- **Шахова дошка** — відмальована на WPF Canvas з кольоровими клітинками та декоративною рамкою ([Board.cs, рядки 187–215](kursach%20wpf%20.framework/Board.cs#L187-L215))
- **Маркери ходів** — точки для доступних ходів, іконки атаки для захоплення ворожих фігур, червона підсвітка при шаху королю ([Board.cs, рядки 256–295](kursach%20wpf%20.framework/Board.cs#L256-L295))
- **Діалог перетворення пішака** — інтерактивне вікно вибору фігури: Ферзь, Тура, Слон або Кінь ([ChoosingFigure.xaml.cs, рядки 29–31](kursach%20wpf%20.framework/ChoosingFigure.xaml.cs#L29-L31))
- **Екран результату** — відображає переможця або нічию з кнопкою повернення в меню ([Page1.xaml.cs](kursach%20wpf%20.framework/Page1.xaml.cs))

### Шахова логіка
- Усі 6 типів фігур з правильними правилами ходів: Король, Ферзь, Тура, Слон, Кінь, Пішак
- **Виявлення шаху** — усі ходи що залишають Короля під шахом заблоковані ([Figure.cs, рядки 93–95](kursach%20wpf%20.framework/Figure.cs#L93-L95))
- **Виявлення мату та пату** ([Board.cs, рядки 473–476](kursach%20wpf%20.framework/Board.cs#L473-L476))
- **Рокіровка** — коротка та довга ([King.cs, рядки 155–188](kursach%20wpf%20.framework/King.cs#L155-L188))
- **Перетворення пішака** — асинхронний діалог при досягненні останньої горизонталі ([Pawn.cs, рядки 126–147](kursach%20wpf%20.framework/Pawn.cs#L126-L147))
- **Умови нічиї** — недостатній матеріал (≤3 фігури) та правило 50 ходів ([Board.cs, рядки 539–554](kursach%20wpf%20.framework/Board.cs#L539-L554))

### Таймер
- Кожен гравець має **5-хвилинний таймер** зворотнього відліку ([Board.cs, рядки 40–87](kursach%20wpf%20.framework/Board.cs#L40-L87))
- Таймери перемикаються автоматично після кожного ходу ([Board.cs, рядки 127–140](kursach%20wpf%20.framework/Board.cs#L127-L140))
- Закінчення часу викликає миттєву перемогу суперника ([Board.cs, рядки 95–125](kursach%20wpf%20.framework/Board.cs#L95-L125))

---

## 🏗️ Структура проєкту

```
kursach wpf .framework/
├── Figure.cs              # Абстрактний базовий клас для всіх шахових фігур
├── Board.cs               # Дошка, логіка ходів, таймер, стан гри
├── Cell.cs                # Представлення клітинки дошки
├── King.cs                # Рух короля, рокіровка, уникнення шаху
├── Queen.cs               # Рух ферзя та захисна логіка
├── Rook.cs                # Рух тури
├── Bishop.cs              # Рух слона
├── Knight.cs              # Рух коня
├── Pawn.cs                # Рух пішака, перетворення
├── MainWindow.xaml(.cs)   # Головне вікно гри
├── start.xaml(.cs)        # Стартовий екран
├── Page1.xaml(.cs)        # Екран результату гри
├── ChoosingFigure.xaml.cs # Діалог перетворення пішака
└── Image/                 # PNG іконки для всіх шахових фігур
```

---

## 🧩 Design Patterns

### 1. Template Method — [`Figure.cs`](kursach%20wpf%20.framework/Figure.cs)

Абстрактний клас `Figure` визначає скелет алгоритму валідації ходу у методі [`CanMoveTo()` (рядки 81–97)](kursach%20wpf%20.framework/Figure.cs#L81-L97):
1. Перевірка меж дошки
2. Перевірка зіткнення зі своєю фігурою
3. Виклик абстрактного [`CanReach()` (рядок 99)](kursach%20wpf%20.framework/Figure.cs#L99) — перевизначається у кожному підкласі
4. Симуляція ходу через [`WouldKingBeInCheck()` (рядки 104–117)](kursach%20wpf%20.framework/Figure.cs#L104-L117) — перевіряє чи Король залишився б під шахом

Абстрактні методи що зобов'язані реалізувати всі підкласи оголошені на [рядках 28–30](kursach%20wpf%20.framework/Figure.cs#L28-L30). Підкласи перевизначають лише потрібні кроки, не змінюючи загальний алгоритм.

### 2. Strategy — [`Queen.cs`](kursach%20wpf%20.framework/Queen.cs), [`Rook.cs`](kursach%20wpf%20.framework/Rook.cs), [`Bishop.cs`](kursach%20wpf%20.framework/Bishop.cs), [`Knight.cs`](kursach%20wpf%20.framework/Knight.cs), [`Pawn.cs`](kursach%20wpf%20.framework/Pawn.cs)

Кожна фігура реалізує власну стратегію руху у `MoveFigure()`, перемикаючись між двома алгоритмами залежно від стану гри:
- [`StandardMoves()` (Queen.cs, рядки 68–79)](kursach%20wpf%20.framework/Queen.cs#L68-L79) — звичайні ходи коли Король не під шахом
- [`DefensiveMoves()` (Queen.cs, рядки 38–66)](kursach%20wpf%20.framework/Queen.cs#L38-L66) — захисні ходи коли Король під шахом

Перемикання між стратегіями відбувається через перевірку [`IsKingInCheck()` (Queen.cs, рядок 30)](kursach%20wpf%20.framework/Queen.cs#L30). `Board` викликає `MoveFigure()` однаково для всіх фігур ([Board.cs, рядок 475](kursach%20wpf%20.framework/Board.cs#L475)), не знаючи конкретної стратегії кожної фігури.

### 3. Composite — [`Board.cs`](kursach%20wpf%20.framework/Board.cs)

`Board` керує всією колекцією `Figure` та взаємодіє з ними однаково через абстрактний клас:
- [`GetAllFigures()` (рядки 426–454)](kursach%20wpf%20.framework/Board.cs#L426-L454) — повертає плаский список усіх фігур для однорідної ітерації
- [`RefreshAllAttackCell()` (рядки 363–375)](kursach%20wpf%20.framework/Board.cs#L363-L375) — викликає `FillArrayAttacksCell()` на кожній фігурі без знання їхніх конкретних типів
- [`IsSquareUnderAttack()` (рядки 378–390)](kursach%20wpf%20.framework/Board.cs#L378-L390) — викликає `IsAttacking()` на всіх фігурах поліморфно

---

## 🔧 Programming Principles

### 1. Single Responsibility Principle (SRP)

Кожен клас має одну причину для зміни:
- `Figure` — лише дані фігури та валідація ходу ([Figure.cs, рядки 17–184](kursach%20wpf%20.framework/Figure.cs#L17-L184))
- `Board` — лише стан дошки, рендеринг та ігровий процес ([Board.cs](kursach%20wpf%20.framework/Board.cs))
- `ChoosingFigure` — лише UI перетворення пішака ([ChoosingFigure.xaml.cs, рядки 29–93](kursach%20wpf%20.framework/ChoosingFigure.xaml.cs#L29-L93))
- `Page1` — лише відображення результату гри ([Page1.xaml.cs](kursach%20wpf%20.framework/Page1.xaml.cs))

### 2. Open/Closed Principle (OCP)

`Figure` відкритий для розширення, але закритий для модифікації. Абстрактні методи [`IsAttacking()`, `MoveFigure()`, `FillArrayAttacksCell()` (рядки 28–30)](kursach%20wpf%20.framework/Figure.cs#L28-L30) дозволяють додати нову фігуру лише через створення нового підкласу — без жодних змін у `Board` або існуючих фігурах.

### 3. Liskov Substitution Principle (LSP)

Всі підкласи `Figure` можна використовувати там де очікується `Figure`. `Board` зберігає всі фігури у масиві [`ArrFigure[,]` (рядок 13)](kursach%20wpf%20.framework/Board.cs#L13) типу `Figure[,]` та отримує їх через [`GetAllFigures()` (рядок 426)](kursach%20wpf%20.framework/Board.cs#L426) — працюючи з ними поліморфно через `IsAttacking()` та `MoveFigure()` без перевірки конкретного типу.

### 4. DRY (Don't Repeat Yourself)

Спільна логіка сканування напрямків винесена у базовий клас і не дублюється у жодній фігурі:
- [`ScanDiagonal()` (рядки 161–172)](kursach%20wpf%20.framework/Figure.cs#L161-L172) — спільний для Ферзя і Слона
- [`ScanStraight()` (рядки 175–184)](kursach%20wpf%20.framework/Figure.cs#L175-L184) — спільний для Ферзя і Тури
- [`CheckCell()` (рядки 49–79)](kursach%20wpf%20.framework/Figure.cs#L49-L79) — централізована перевірка меж та зіткнень для всіх фігур

### 5. Fail Fast

[`WouldKingBeInCheck()` (рядки 104–117)](kursach%20wpf%20.framework/Figure.cs#L104-L117) симулює хід на дошці, одразу перевіряє чи Король опиниться під шахом і скасовує симуляцію. Якщо так — хід негайно відхиляється у [`CanMoveTo()` (рядок 93)](kursach%20wpf%20.framework/Figure.cs#L93) ще до відображення маркера гравцю.

### 6. Separation of Concerns

Ігрова логіка повністю відокремлена від UI. `Figure` та підкласи містять лише правила шахів. `Board` відповідає за стан гри та рендеринг маркерів на Canvas. `MainWindow`, `Page1`, `start` — лише UI без жодної ігрової логіки. Таймер інкапсульований у `Board` через [`InitializeTimers()` (рядки 40–87)](kursach%20wpf%20.framework/Board.cs#L40-L87).

---

## 🔨 Refactoring Techniques

### 1. Extract Method

Довга логіка ходів розбита на приватні методи у кожній фігурі:
- [`StandardMoves()` та `DefensiveMoves()` (Queen.cs, рядки 38–79)](kursach%20wpf%20.framework/Queen.cs#L38-L79)
- Спільне сканування винесено у [`ScanDiagonal()` та `ScanStraight()` (Figure.cs, рядки 161–184)](kursach%20wpf%20.framework/Figure.cs#L161-L184)

### 2. Extract Superclass

Спільні поля (`Color`, `X`, `Y`, `ImageFigure`, `CellsUnderAttack`) та методи (`CheckCell`, `WouldKingBeInCheck`, `IsBlockedPath`, `ScanDiagonal`, `ScanStraight`) винесені в абстрактний клас [`Figure` (рядки 17–184)](kursach%20wpf%20.framework/Figure.cs#L17-L184), усуваючи дублювання між усіма 6 типами фігур.

### 3. Replace Conditional with Polymorphism

Замість великого `switch` на тип фігури — кожен підклас перевизначає `IsAttacking()`, `MoveFigure()` та `FillArrayAttacksCell()`. `Board` викликає ці методи поліморфно через [`RefreshAllAttackCell()` (рядки 363–375)](kursach%20wpf%20.framework/Board.cs#L363-L375) та [`IsSquareUnderAttack()` (рядки 378–390)](kursach%20wpf%20.framework/Board.cs#L378-L390).

### 4. Replace Magic Number with Named Constant

Розмір дошки `8` та розмір клітинки `80` зберігаються як іменовані поля [`boardSize` та `tileSize` (Board.cs, рядки 24–25)](kursach%20wpf%20.framework/Board.cs#L24-L25), що дозволяє змінити розмір дошки в одному місці без пошуку магічних чисел по всьому коду.

### 5. Separate Query from Modifier

Запити без побічних ефектів відокремлені від модифікуючих методів:
- Запити: [`IsKingInCheck()` (рядки 420–423)](kursach%20wpf%20.framework/Board.cs#L420-L423), [`GetAllFigures()` (рядки 426–454)](kursach%20wpf%20.framework/Board.cs#L426-L454), [`GetKing()` (рядки 458–470)](kursach%20wpf%20.framework/Board.cs#L458-L470), [`IsSquareUnderAttack()` (рядки 378–390)](kursach%20wpf%20.framework/Board.cs#L378-L390)
- Модифікатори: [`MoveFigure()` (рядки 298–360)](kursach%20wpf%20.framework/Board.cs#L298-L360), [`RefreshAllAttackCell()` (рядки 363–375)](kursach%20wpf%20.framework/Board.cs#L363-L375)

### 6. Introduce Parameter Object

Методи сканування [`ScanDiagonal()` та `ScanStraight()` (рядки 161–184)](kursach%20wpf%20.framework/Figure.cs#L161-L184) приймають `(startX, startY, stepX, stepY)` — узгоджену групу параметрів що представляє вектор напрямку. Це замінює розкидану арифметику координат у кожній фігурі єдиним стандартним викликом.
