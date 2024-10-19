namespace Sudoku_new_algorithm.Board
{
    /// <summary>
    /// Interface defining the contract for generating and manipulating a Sudoku board.
    /// </summary>
    public interface IBoardGenerator
    {
        /// <summary>
        /// Generates a Sudoku board based on the specified difficulty level.
        /// </summary>
        /// <param name="difficulty">The difficulty level ("1" for easy, "2" for medium, "3" for hard).</param>
        /// <returns>A 9x9 integer array representing the Sudoku board.</returns>
        int[,] GenerateSudoku(string difficulty);
        /// <summary>
        /// Generates a fully completed and valid Sudoku board.
        /// </summary>
        /// <returns>A 9x9 integer array representing a full Sudoku board.</returns>
        int[,] GenerateFullSudoku();
        /// <summary>
        /// Checks if placing a number on the board at the specified row and column is a valid move.
        /// </summary>
        /// <param name="board">The current Sudoku board.</param>
        /// <param name="row">The row index where the number is being placed.</param>
        /// <param name="col">The column index where the number is being placed.</param>
        /// <param name="num">The number to be placed on the board.</param>
        /// <returns>True if the move is valid, otherwise false.</returns>
        bool IsValidMove(int[,] board, int row, int col, int num);
        /// <summary>
        /// Checks if the specified row in the Sudoku board is fully filled.
        /// </summary>
        /// <param name="board">The current Sudoku board.</param>
        /// <param name="row">The row index to check.</param>
        /// <returns>True if the row is fully filled, otherwise false.</returns>
        bool IsRowFilled(int[,] board, int row);
        /// <summary>
        /// Checks if the specified column in the Sudoku board is fully filled.
        /// </summary>
        /// <param name="board">The current Sudoku board.</param>
        /// <param name="col">The column index to check.</param>
        /// <returns>True if the column is fully filled, otherwise false.</returns>
        bool IsColumnFilled(int[,] board, int col);
        /// <summary>
        /// Checks if the specified 3x3 box in the Sudoku board is fully filled.
        /// </summary>
        /// <param name="board">The current Sudoku board.</param>
        /// <param name="startRow">The starting row index of the 3x3 box.</param>
        /// <param name="startCol">The starting column index of the 3x3 box.</param>
        /// <returns>True if the box is fully filled, otherwise false.</returns>
        bool IsBoxFilled(int[,] board, int startRow, int startCol);
    }
}
