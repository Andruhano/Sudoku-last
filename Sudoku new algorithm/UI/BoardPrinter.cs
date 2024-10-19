namespace Sudoku_new_algorithm.UI
{
    /// <summary>
    /// Responsible for printing the Sudoku board to the console.
    /// </summary>
    public class BoardPrinter
    {
        /// <summary>
        /// The original Sudoku board that holds the correct values.
        /// </summary>
        private int[,] originalBoard;
        /// <summary>
        /// Initializes a new instance of the <see cref="BoardPrinter"/> class.
        /// </summary>
        /// <param name="originalBoard">The original Sudoku board.</param>
        public BoardPrinter(int[,] originalBoard)
        {
            this.originalBoard = originalBoard;
        }
        /// <summary>
        /// Prints the current state of the Sudoku board to the console.
        /// </summary>
        /// <param name="currentBoard">The current state of the Sudoku board, which may contain user input.</param>
        public void PrintBoard(int[,] currentBoard)
        {
            Console.WriteLine("  0 1 2  3 4 5  6 7 8");
            Console.WriteLine("  ---------------------");

            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0 && i != 0)
                    Console.WriteLine("  ---------------------");

                Console.Write(i + "|");

                for (int j = 0; j < 9; j++)
                {
                    if (j % 3 == 0 && j != 0)
                        Console.Write("|");

                    if (originalBoard[i, j] != 0)
                    {
                        Console.Write(originalBoard[i, j] + " ");
                    }
                    else
                    {
                        if (currentBoard[i, j] != 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(currentBoard[i, j] + " ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(". ");
                        }
                    }
                }

                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }
}
