using Sudoku_new_algorithm.Board;
using Sudoku_new_algorithm.UI;
using Sudoku_new_algorithm.Utilities;
using System.Diagnostics;

namespace Sudoku_new_algorithm.Game
{
    /// <summary>
    /// The main class that handles the flow of the Sudoku game, including the main menu, game start, and user interactions.
    /// </summary>
    public class Game : IGame
    {
        /// <summary>
        /// Manages the results of previous games.
        /// </summary>
        private readonly ResultsManager resultsManager = new ResultsManager();
        /// <summary>
        /// Handles user input for the game.
        /// </summary>
        private readonly UserInputHandler inputHandler = new UserInputHandler();
        /// <summary>
        /// Prints the current state of the Sudoku board.
        /// </summary>
        private BoardPrinter boardPrinter;
        /// <summary>
        /// The original, unaltered Sudoku board as generated at the start of the game.
        /// </summary>
        private int[,] originalBoard;
        /// <summary>
        /// The current state of the Sudoku board during gameplay.
        /// </summary>
        private int[,] currentBoard;
        /// <summary>
        /// Tracks the number of mistakes made by the player.
        /// </summary>
        private int mistakes = 0;
        /// <summary>
        /// The maximum number of mistakes allowed before the game ends.
        /// </summary>
        private int maxAllowedMistakes = 3;
        /// <summary>
        /// The difficulty level of the current game.
        /// </summary>
        private string difficulty;
        /// <summary>
        /// Generates the Sudoku board.
        /// </summary>
        private readonly IBoardGenerator boardGenerator;
        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="boardGenerator">The Sudoku board generator.</param>
        public Game(IBoardGenerator boardGenerator)
        {
            this.boardGenerator = boardGenerator;
        }
        /// <summary>
        /// Displays the main menu, allowing the player to start the game, view developer info, check results, or exit.
        /// </summary>
        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Start the game");
                Console.WriteLine("2. About the developer");
                Console.WriteLine("3. Top 3 results");
                Console.WriteLine("4. Exit");

                string choice = inputHandler.GetMainMenuChoice();
                switch (choice)
                {
                    case "1":
                        StartGame();
                        break;
                    case "2":
                        DeveloperInfo();
                        break;
                    case "3":
                        resultsManager.DisplayTop3Results();
                        break;
                    case "4":
                        ExitGame();
                        break;
                    default:
                        Console.WriteLine("Wrong choice.");
                        break;
                }
            }
        }
        /// <summary>
        /// Displays information about the developer.
        /// </summary>
        private void DeveloperInfo()
        {
            Console.Clear();
            Console.WriteLine("Разработчик: Andrey Mikhailenko.");
            Console.ReadKey();
        }
        /// <summary>
        /// Exits the game application.
        /// </summary>
        private void ExitGame()
        {
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);
        }
        /// <summary>
        /// Starts a new Sudoku game by selecting the difficulty level and generating the game board.
        /// </summary>
        public void StartGame()
        {
            Console.Clear();
            Console.WriteLine("Select the level of difficulty: 1. Easy 2. Medium 3. Heavy");

            string choice = inputHandler.GetMainMenuChoice();
            difficulty = choice switch
            {
                "1" => "Easy",
                "2" => "Medium",
                "3" => "Heavy",
                _ => throw new ArgumentException("Incorrect difficulty selection.")
            };

            originalBoard = boardGenerator.GenerateSudoku(choice);
            currentBoard = (int[,])originalBoard.Clone();
            PlaySudoku(currentBoard);
        }
        /// <summary>
        /// Plays the Sudoku game, allowing the user to input numbers and tracking mistakes.
        /// </summary>
        /// <param name="board">The Sudoku board to play on.</param>
        public void PlaySudoku(int[,] board)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            boardPrinter = new BoardPrinter(originalBoard);

            while (mistakes < maxAllowedMistakes)
            {
                Console.Clear();
                boardPrinter.PrintBoard(board);

                Console.WriteLine("\nEnter the line number (0-8):");
                int x = inputHandler.GetValidCoordinate();

                Console.WriteLine("Enter the column number (0-8):");
                int y = inputHandler.GetValidCoordinate();

                if (board[x, y] != 0)
                {
                    Console.WriteLine("This cell is already full.");
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine("Enter a number (1-9):");
                int num = inputHandler.GetValidNumber();

                if (boardGenerator.IsValidMove(board, x, y, num))
                {
                    board[x, y] = num;
                    NotifyExtraMistake(board, x, y);
                }
                else
                {
                    mistakes++;
                    Console.WriteLine($"Error! Attempts left: {maxAllowedMistakes - mistakes}");
                    Console.ReadKey();
                }

                if (IsBoardCompleted(board))
                {
                    timer.Stop();
                    Console.Clear();
                    boardPrinter.PrintBoard(board);

                    TimeSpan timeSpan = timer.Elapsed;
                    string formattedTime = $"{(int)timeSpan.TotalMinutes} min {timeSpan.Seconds} sec";

                    Console.WriteLine($"Congratulations! You made it through the game for {formattedTime} with {mistakes} mistakes.");
                    resultsManager.SaveResult(difficulty, formattedTime);
                    Console.ReadKey();
                    MainMenu();
                }
            }

            Console.WriteLine("Game over! The number of errors has been exceeded.");
            timer.Stop();
            Console.ReadKey();
            MainMenu();
        }
        /// <summary>
        /// Checks whether the Sudoku board is fully completed.
        /// </summary>
        /// <param name="board">The current Sudoku board.</param>
        /// <returns>True if the board is fully completed, otherwise false.</returns>
        private bool IsBoardCompleted(int[,] board)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (board[row, col] == 0)
                        return false;
                }
            }
            return true;
        }

        private void NotifyExtraMistake(int[,] board, int row, int col)
        {
            bool extraChanceGiven = false;

            if (boardGenerator.IsRowFilled(board, row))
            {
                maxAllowedMistakes++;
                Console.WriteLine("Received an extra try for completing the line!");
                extraChanceGiven = true;
            }

            if (boardGenerator.IsColumnFilled(board, col))
            {
                maxAllowedMistakes++;
                Console.WriteLine("Received an extra try for completing the column!");
                extraChanceGiven = true;
            }

            if (boardGenerator.IsBoxFilled(board, row - row % 3, col - col % 3))
            {
                maxAllowedMistakes++;
                Console.WriteLine("Received an extra try for completing a 3x3 box!");
                extraChanceGiven = true;
            }

            if (extraChanceGiven)
            {
                Console.WriteLine($"You now have {maxAllowedMistakes - mistakes} remaining attempts.");
                Console.ReadKey();
            }
        }
    }
}
