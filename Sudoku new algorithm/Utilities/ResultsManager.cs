namespace Sudoku_new_algorithm.Utilities
{
    /// <summary>
    /// Manages the saving and displaying of game results for the Sudoku game.
    /// </summary>
    public class ResultsManager
    {
        /// <summary>
        /// The file path where results are stored.
        /// </summary>
        private const string ResultsFilePath = "results.txt";
        /// <summary>
        /// Saves the game result, including difficulty level, time taken, and the current date and time.
        /// </summary>
        /// <param name="difficulty">The difficulty level of the game.</param>
        /// <param name="time">The time taken to complete the game.</param>
        public void SaveResult(string difficulty, string time)
        {
            string result = $"{difficulty} | {time} | {DateTime.Now}";
            File.AppendAllText(ResultsFilePath, result + Environment.NewLine);
            Console.WriteLine("The result is preserved.");
        }
        /// <summary>
        /// Displays the top three results for each difficulty level from the results file.
        /// </summary>
        public void DisplayTop3Results()
        {
            if (!File.Exists(ResultsFilePath))
            {
                Console.WriteLine("There is no data on passages.");
                Console.WriteLine("\nPress any key to return to the menu.");
                Console.ReadKey();
                return;
            }

            var results = File.ReadAllLines(ResultsFilePath)
                .Select(line => line.Split('|').Select(part => part.Trim()).ToArray())
                .Where(parts => parts.Length == 3)
                .Select(parts => new
                {
                    Difficulty = parts[0],
                    Time = TimeParser.ParseTime(parts[1]),
                    Date = parts[2]
                })
                .GroupBy(r => r.Difficulty)
                .Select(g => new { Difficulty = g.Key, TopResults = g.OrderBy(r => r.Time).Take(3) });

            foreach (var difficultyGroup in results)
            {
                Console.WriteLine($"\nTop 3 walkthroughs for difficulty:  {difficultyGroup.Difficulty}");
                foreach (var result in difficultyGroup.TopResults)
                {
                    Console.WriteLine($"Time: {result.Time.Minutes} min {result.Time.Seconds} sec | Date: {result.Date}");
                }
            }

            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadKey();
        }
    }
}
