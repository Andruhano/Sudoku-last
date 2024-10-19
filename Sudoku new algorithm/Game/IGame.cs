namespace Sudoku_new_algorithm.Game
{
    /// <summary>
    /// Interface defining the contract for a Sudoku game, including methods for starting the game, playing, and displaying the main menu.
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// Starts a new game of Sudoku by initializing the game state and generating a board.
        /// </summary>
        void StartGame();
        /// <summary>
        /// Handles the gameplay mechanics, allowing the user to input their moves on the provided Sudoku board.
        /// </summary>
        /// <param name="board">The current state of the Sudoku board to play on.</param>
        void PlaySudoku(int[,] board);
        /// <summary>
        /// Displays the main menu of the game, allowing the user to navigate through game options.
        /// </summary>
        void MainMenu();
    }
}
