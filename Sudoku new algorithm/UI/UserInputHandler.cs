namespace Sudoku_new_algorithm.UI
{
    /// <summary>
    /// Handles user input for the Sudoku game, ensuring valid entries for coordinates and numbers.
    /// </summary>
    public class UserInputHandler
    {
        /// <summary>
        /// Prompts the user for a coordinate input (row or column) and validates the input.
        /// </summary>
        /// <returns>A valid coordinate as an integer between 0 and 8.</returns>
        public int GetValidCoordinate()
        {
            int coordinate;
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out coordinate) && coordinate >= 0 && coordinate <= 8)
                {
                    return coordinate;
                }
                else
                {
                    Console.WriteLine("Invalid entry. Enter a number between 0 and 8:");
                }
            }
        }
        /// <summary>
        /// Prompts the user for a number input and validates the input.
        /// </summary>
        /// <returns>A valid number as an integer between 1 and 9.</returns>
        public int GetValidNumber()
        {
            int number;
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out number) && number >= 1 && number <= 9)
                {
                    return number;
                }
                else
                {
                    Console.WriteLine("Invalid entry. Enter a number between 1 and 9:");
                }
            }
        }
        /// <summary>
        /// Reads the user's choice from the main menu.
        /// </summary>
        /// <returns>The user's choice as a string.</returns>
        public string GetMainMenuChoice()
        {
            return Console.ReadLine();
        }
    }
}
