# ExoSandbox 🏋️♂️

A smart C# exercise manager with live-reload capabilities and boilerplate generation. Perfect for coding practice sessions!
PS: Please add the 'Exercices' folder to your antivirus exceptions to ensure smooth functionality.

[![.NET Version](https://img.shields.io/badge/.NET-6.0+-%23512bd4)](https://dotnet.microsoft.com/)
[![Spectre.Console](https://img.shields.io/badge/Spectre.Console-0.46.0-blue)](https://spectreconsole.net/)

## Features ✨

- **📂 Smart Exercise Management**
  - Auto-discovers exercises in `/Exercises` directory
  - Dynamic menu with arrow-key navigation
  - Quick exit with dedicated "Quit" option

- **⚡ Live Development Experience**
  - Integrated `dotnet watch run` for instant feedback
  - Cross-platform console output handling
  - Clean process cancellation (Ctrl+C support)

- **🚀 Project Scaffolding**
  - Generates classic C# boilerplate (non-top-level statements)
  - Automatic namespace handling
  - Safe project isolation

## Installation ⚙️

**Prerequisites:**
- .NET 6 SDK or newer
- Terminal with ANSI support

```bash
# Clone & Run
git clone https://github.com/Rashy-hub/C-ExoSandbox.git
cd C-ExoSandbox
dotnet restore
dotnet run
```

## Usage 🕹️

```text
Choose an exercise to run:

› MyExercise
  Generate new exo boilerplate
  Quit
```

## Key Controls:

- ↑/↓ : Navigate menu
- Enter : Confirm selection
- Ctrl+C : Cancel current operation
- Any key : Return to menu

## Project Structure:

```text
ExoSandbox/
├── Exercises/               # Container for all exercises
│   └── MyExercise/         # Individual exercise project
│       ├── Program.cs      # Exercise entry point
│       └── MyExercise.csproj
├── Program.cs              # Main application logic
└── ExoSandbox.csproj       # Project configuration
```