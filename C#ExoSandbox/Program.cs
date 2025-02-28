using Spectre.Console;
using System.Diagnostics;
using System.Threading;

namespace ExoSandbox
{
    class Program
    {
        private static volatile bool isExerciseRunning = false;
        private static CancellationTokenSource? exerciseCts;
        private static CancellationTokenSource? mainCts;

        static async Task Main(string[] args)
        {
            mainCts = new CancellationTokenSource();
            var token = mainCts.Token;

            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                if (isExerciseRunning)
                {
                    exerciseCts?.Cancel();
                    Console.WriteLine("\nCancelling current exercise...");
                }
                else
                {
                    mainCts?.Cancel();
                    Console.WriteLine("\nShutting down application...");
                }
            };

            try
            {
                while (!token.IsCancellationRequested)
                {
                    Console.Clear();
                    var currentDirectory = Directory.GetCurrentDirectory();
                    var pathExercices = Path.Combine(currentDirectory, "Exercises");
                    var exercices = new List<string>();

                    if (!Directory.Exists(pathExercices))
                        Directory.CreateDirectory(pathExercices);

                    exercices.AddRange(Directory.GetDirectories(pathExercices)
                        .Select(d => Path.GetFileName(d)!));
                    exercices.Add("Generate new exo boilerplate");
                    exercices.Add("Quit"); // Nouvelle option

                    var selection = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Choose an exercise to run :")
                            .AddChoices(exercices));

                    if (selection == "Generate new exo boilerplate")
                    {
                        await GenerateNewExoAsync();
                    }
                    else if (selection == "Quit")
                    {
                        mainCts.Cancel();
                        break;
                    }
                    else
                    {
                        var selectedPath = Path.Combine(pathExercices, selection);
                        if (Directory.Exists(selectedPath))
                        {
                            await StartExerciseAsync(selectedPath);
                        }
                    }

                    // Attente non-bloquante
                    if (!token.IsCancellationRequested)
                    {
                        Console.WriteLine("\nPress any key to continue...");
                        var keyTask = Task.Run(() => Console.ReadKey(true), token);
                        await Task.WhenAny(keyTask, Task.Delay(250));
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("\nApplication shutdown completed.");
            }
            finally
            {
                mainCts?.Dispose();
                exerciseCts?.Dispose();
            }
        }

        private static async Task GenerateNewExoAsync()
        {
            string? projectName;
            do
            {
                Console.WriteLine("Give the name of the new project");
                projectName = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(projectName));

            var currentDirectory = Directory.GetCurrentDirectory();
            var exercisesPath = Path.Combine(currentDirectory, "Exercises");

            var projectDirectory = Path.Combine(exercisesPath, projectName);
            if (Directory.Exists(projectDirectory))
            {
                Console.WriteLine($"The project {projectName} already exists. ");
                return;
            }

            Console.WriteLine($"Create project : {projectName}...");
            var startInfo = new ProcessStartInfo("dotnet", $"new console -n {projectName}")
            {
                WorkingDirectory = exercisesPath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            var process = Process.Start(startInfo);
            if (process != null)
                await process.WaitForExitAsync();
            else
                Console.WriteLine("The project creation process encountered an error.");

            if (!Directory.Exists(projectDirectory))
            {
                Console.WriteLine($"Project creation failed. {projectName}.");
                return;
            }

            Console.WriteLine($"The project {projectName} was created successfully.");

            var programFile = Path.Combine(projectDirectory, "Program.cs");

            var classicProgram = $@"using System;

                namespace {projectName}
                {{
                    class Program
                    {{
                        static void Main(string[] args)
                        {{
                            Console.WriteLine(""Hello World!"");
                        }}
                    }}
                }}
                ";

            try
            {
                await File.WriteAllTextAsync(programFile, classicProgram);
                Console.WriteLine("Program.cs file updated with classic boilerplate.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating file Program.cs : " + ex.Message);
            }
        }

        private static async Task StartExerciseAsync(string exercisePath)
        {
            isExerciseRunning = true;
            exerciseCts = new CancellationTokenSource();

            try
            {
                var process = new Process();
                process.StartInfo = new ProcessStartInfo("dotnet", "watch run")
                {
                    WorkingDirectory = exercisePath,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                process.OutputDataReceived += (sender, e) =>
                    Console.WriteLine(e.Data);
                process.ErrorDataReceived += (sender, e) =>
                    Console.WriteLine(e.Data);

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                var processTask = process.WaitForExitAsync(exerciseCts.Token);
                await Task.WhenAny(processTask, Task.Delay(-1, exerciseCts.Token));

                if (!process.HasExited)
                {
                    process.Kill();
                    await process.WaitForExitAsync();
                }
            }
            finally
            {
                isExerciseRunning = false;
                exerciseCts?.Dispose();
                exerciseCts = null;
            }
        }
    }
}