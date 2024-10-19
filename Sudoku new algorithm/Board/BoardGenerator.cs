namespace Sudoku_new_algorithm.Board
{
    /// <summary>
    /// Generates a Sudoku board and manages Sudoku operations such as validating moves and solving the puzzle.
    /// </summary>
    public class BoardGenerator : IBoardGenerator
    {
        /// <summary>
        /// Random number generator used for generating and shuffling the board.
        /// </summary>
        private Random rand = new Random();
        /// <summary>
        /// Generates a Sudoku board with the specified difficulty level.
        /// </summary>
        /// <param name="difficulty">The difficulty level as a string ("1" for easy, "2" for medium, "3" for hard).</param>
        /// <returns>A 9x9 integer array representing the Sudoku board.</returns>
        /// <exception cref="ArgumentException">Thrown if an invalid difficulty is passed.</exception>
        public int[,] GenerateSudoku(string difficulty)
        {
            int[,] board = GenerateFullSudoku();
            int removeCount = difficulty switch
            {
                "1" => 2,
                "2" => 20,
                "3" => 30,
                _ => throw new ArgumentException("Incorrect difficulty selection.")
            };

            RemoveNumbersWithSolutionCheck(board, removeCount);
            return board;
        }
        /// <summary>
        /// Generates a fully completed and valid Sudoku board.
        /// </summary>
        /// <returns>A 9x9 integer array representing a full Sudoku board.</returns>
        public int[,] GenerateFullSudoku()
        {
            int[,] board = new int[9, 9];
            FillBoard(board);
            return board;
        }
        /// <summary>
        /// Validates whether a number can be placed at the specified row and column of the board.
        /// </summary>
        /// <param name="board">The current Sudoku board.</param>
        /// <param name="row">The row index.</param>
        /// <param name="col">The column index.</param>
        /// <param name="num">The number to check for placement.</param>
        /// <returns>True if the number can be placed, otherwise false.</returns>
        public bool IsValidMove(int[,] board, int row, int col, int num)
        {
            for (int x = 0; x < 9; x++)
            {
                if (board[row, x] == num || board[x, col] == num ||
                    board[row - row % 3 + x / 3, col - col % 3 + x % 3] == num)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Checks if the specified row in the Sudoku board is fully filled.
        /// </summary>
        /// <param name="board">The current Sudoku board.</param>
        /// <param name="row">The row index to check.</param>
        /// <returns>True if the row is fully filled, otherwise false.</returns>
        public bool IsRowFilled(int[,] board, int row)
        {
            for (int col = 0; col < 9; col++)
                if (board[row, col] == 0)
                    return false;
            return true;
        }
        /// <summary>
        /// Checks if the specified column in the Sudoku board is fully filled.
        /// </summary>
        /// <param name="board">The current Sudoku board.</param>
        /// <param name="col">The column index to check.</param>
        /// <returns>True if the column is fully filled, otherwise false.</returns>
        public bool IsColumnFilled(int[,] board, int col)
        {
            for (int row = 0; row < 9; row++)
                if (board[row, col] == 0)
                    return false;
            return true;
        }
        /// <summary>
        /// Checks if the specified 3x3 box in the Sudoku board is fully filled.
        /// </summary>
        /// <param name="board">The current Sudoku board.</param>
        /// <param name="startRow">The starting row of the 3x3 box.</param>
        /// <param name="startCol">The starting column of the 3x3 box.</param>
        /// <returns>True if the box is fully filled, otherwise false.</returns>
        public bool IsBoxFilled(int[,] board, int startRow, int startCol)
        {
            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 3; col++)
                    if (board[startRow + row, startCol + col] == 0)
                        return false;
            return true;
        }


        /// <summary>
        /// Recursively fills the Sudoku board with valid numbers.
        /// </summary>
        /// <param name="board">The Sudoku board to fill.</param>
        /// <returns>True if the board is successfully filled, otherwise false.</returns>
        public bool FillBoard(int[,] board)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (board[row, col] == 0)
                    {
                        int[] numbers = ShuffleNumbers();
                        foreach (int num in numbers)
                        {
                            if (IsValidMove(board, row, col, num))
                            {
                                board[row, col] = num;
                                if (FillBoard(board))
                                    return true;

                                board[row, col] = 0;
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Removes numbers from the Sudoku board while ensuring it has a unique solution.
        /// </summary>
        /// <param name="board">The current Sudoku board.</param>
        /// <param name="amountToRemove">The number of cells to remove from the board.</param>
        public void RemoveNumbersWithSolutionCheck(int[,] board, int amountToRemove)
        {
            int removed = 0;
            while (removed < amountToRemove)
            {
                int row = rand.Next(0, 9);
                int col = rand.Next(0, 9);

                if (board[row, col] != 0)
                {
                    int backup = board[row, col];
                    board[row, col] = 0;

                    if (HasUniqueSolution(board))
                        removed++;
                    else
                        board[row, col] = backup;
                }
            }
        }
        /// <summary>
        /// Checks if the Sudoku board has a unique solution.
        /// </summary>
        /// <param name="board">The current Sudoku board.</param>
        /// <returns>True if the board has a unique solution, otherwise false.</returns>
        private bool HasUniqueSolution(int[,] board)
        {
            int[,] copy = (int[,])board.Clone();
            int solutions = 0;
            SolveSudoku(copy, ref solutions);
            return solutions == 1;
        }
        /// <summary>
        /// Solves the Sudoku board and counts the number of possible solutions.
        /// </summary>
        /// <param name="board">The current Sudoku board.</param>
        /// <param name="solutions">A reference to the solution counter.</param>
        /// <param name="row">The row index (default is 0).</param>
        /// <param name="col">The column index (default is 0).</param>
        /// <returns>True if the board is solvable, otherwise false.</returns>
        private bool SolveSudoku(int[,] board, ref int solutions, int row = 0, int col = 0)
        {
            if (solutions > 1) return false;
            if (row == 9)
            {
                solutions++;
                return true;
            }
            if (col == 9) return SolveSudoku(board, ref solutions, row + 1, 0);

            if (board[row, col] != 0) return SolveSudoku(board, ref solutions, row, col + 1);

            for (int num = 1; num <= 9; num++)
            {
                if (IsValidMove(board, row, col, num))
                {
                    board[row, col] = num;
                    if (SolveSudoku(board, ref solutions, row, col + 1))
                        return true;
                    board[row, col] = 0;
                }
            }
            return false;
        }
        /// <summary>
        /// Shuffles numbers from 1 to 9 for random placement in the Sudoku board.
        /// </summary>
        /// <returns>An array of shuffled numbers from 1 to 9.</returns>
        private int[] ShuffleNumbers()
        {
            int[] numbers = new int[9];
            for (int i = 0; i < 9; i++) numbers[i] = i + 1;
            for (int i = 0; i < 9; i++)
            {
                int j = rand.Next(9);
                int tmp = numbers[i];
                numbers[i] = numbers[j];
                numbers[j] = tmp;
            }
            return numbers;
        }
    }
}
