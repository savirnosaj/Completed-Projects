// Class Library containing many functions, e.g: console.writeling();
using System;

// Namespace - container holding classes and functions;
namespace NumberGuesser
{
    // Main class - used to create objects; inside containing functions(methods) and variables(properties);
    class Program
    {
        // static: refrencing self function only; not instantiating;
        // void: won't return anything;

        // Storing Game Description;
        static void GetAppInfo()
        {
            //Setting all app variables;
            string appName = "Number Guesser";
            string appVersion = "1.0.0";
            string appAuthor = "Jason Rivas";
            // UI: Title Name, Color;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("{0}: Version {1} by {2}", appName, appVersion, appAuthor);
            Console.WriteLine();
            // Console Color Reset;
            Console.ResetColor();
        }

        // Starting Game w/ Welcome Screen;
        static void StartingGame()
        {
            // User UX;
            // Game Starting;
            Console.WriteLine("What is your name?");

            // UX: get user input;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Name: ");
            Console.ResetColor();
            string nameinput = Console.ReadLine();
            // UI: Display user input with game greeting;
            Console.Clear();
            GetAppInfo();
            Console.Write("");
            Console.Write("Hello {0}, let's play a game .. ", nameinput);
        }

        static void EndGame()
        {
            // UI: Output success message;
            Console.Clear();
            GetAppInfo();
            PrintColoredMessage(ConsoleColor.Green, "You guessed correctly! Congratulations");


            // UI: Play again?;
            Console.WriteLine();
            Console.WriteLine("Play Again? [Y or N]");
            // UX: User Input;
            string useranswer = Console.ReadLine().ToUpper();
            if (useranswer == "Y")
            {
                Console.Clear();
                GetAppInfo();
            }
            else if (useranswer == "N")
            {
                // UI: Output ending message;
                Console.WriteLine();
                PrintColoredMessage(ConsoleColor.DarkCyan, "Hope you enjoyed playing! Bye-bye");
                Console.WriteLine();
                return;
            }
            else
            {
                // UI: Play again?;
                Console.WriteLine("Play Again? [Y or N]");
                // UX: User Input;
                string newuseranswer = Console.ReadLine().ToUpper();
                if (newuseranswer == "Y")
                {
                    Console.Clear();
                }
                else if (newuseranswer == "N")
                {
                    // UI: Output ending message;
                    PrintColoredMessage(ConsoleColor.DarkCyan, "Hope you enjoyed playing! Bye-bye");
                    return;
                }
                else
                {
                    // UI: Play again?;
                    Console.WriteLine("Play Again? [Y or N]");
                    // UX: User Input;
                    string againnewuseranswer = Console.ReadLine().ToUpper();
                    if (againnewuseranswer == "Y")
                    {
                        Console.Clear();
                    }
                    else if (againnewuseranswer == "N")
                    {
                        // UI: Output ending message;
                        PrintColoredMessage(ConsoleColor.DarkCyan, "Hope you enjoyed playing! Bye-bye");
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        // Entry Point Method;
        static void Main(string[] args)
        {
            // Pulling Title info from GetAppInfo;
            GetAppInfo();

            StartingGame();

            while (true)
            {
                // Generating random object;
                Random randomnumber = new Random();
                // Random number
                int correctnumber = randomnumber.Next(1, 10);

                // Default int value;
                int numberguess = 0;

                // UI: Asking for users guess;
                Console.WriteLine("Guess a number between 1-10");
                PrintColoredMessage(ConsoleColor.Cyan, "Answer: ");

                // UX: Will keeping asking until the guessed number matches correct value;
                while (numberguess != correctnumber)
                {
                    // Getting User input -
                    string numberinput = Console.ReadLine();

                    // Make sure input is an int;
                    if (!int.TryParse(numberinput, out numberguess))
                    {
                        // UI: Output Error message(not an int);
                        Console.WriteLine();
                        PrintColoredMessage(ConsoleColor.Red, "Error: Please enter an integar.");
                        // UI: Re-ask question;
                        Console.WriteLine();
                        Console.WriteLine("Guess a number between 1-10");
                        PrintColoredMessage(ConsoleColor.Yellow, "Answer: ");

                        continue;
                    }

                    // - & conveting answer to int value that can now be used;
                    numberguess = Int32.Parse(numberinput);

                    // Matching guess to correct answer;
                    if (numberguess != correctnumber)
                    {
                        // UI: Display Error
                        Console.WriteLine();
                        PrintColoredMessage(ConsoleColor.Red, "Oops, wrong answer, guess again ..");
                        Console.WriteLine();
                        // UI: Display retry;
                        PrintColoredMessage(ConsoleColor.Yellow, "Answer: ");
                    }
                }

                EndGame();
            }
        }

        static void PrintColoredMessage(ConsoleColor color, string message)
        {
            // UI: ColoredMessage Message, Color;
            // UI: Output Colored Message;
            Console.ForegroundColor = color;
            Console.Write(message);
            // Console Color Reset;
            Console.ResetColor();
        }
    }
}
