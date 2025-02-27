# ExoSandbox

While practicing C# exercises, I realized I needed a custom tool that could streamline the process. ExoSandbox is a modular, dynamic C# execution environment designed to centralize and run your code exercises in complete isolation through an interactive menu.

## Features

- Work in progress 🚧
- 

## Installation

To get started, ensure you have [Git](https://git-scm.com/) and the [.NET 7 SDK](https://dotnet.microsoft.com/download) (or higher) installed.

```bash
# Clone the repository
git clone 

# Change into the project directory
cd ExoSandbox

# Restore NuGet packages
dotnet restore

# Build the project
dotnet build

# Run the projet

dotnet watch run


## Project Structure

- **/Exercises**: Contains individual C# exercise files (*.cs) that are discovered at runtime.
- **/Plugins**: Houses additional assemblies loaded dynamically.
- **Program.cs**: The application entry point, managing menu display and dynamic code execution.

