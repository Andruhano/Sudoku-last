namespace Sudoku_new_algorithm.Utilities
{
    /// <summary>
    /// Provides utility methods for parsing time strings into TimeSpan objects.
    /// </summary>
    public static class TimeParser
    {
        /// <summary>
        /// Parses a time string formatted as "X min Y sec" into a <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="timeString">The time string to parse.</param>
        /// <returns>A <see cref="TimeSpan"/> representing the parsed time, or <see cref="TimeSpan.Zero"/> if parsing fails.</returns>
        public static TimeSpan ParseTime(string timeString)
        {
            var parts = timeString.Split(' ');
            if (parts.Length >= 4 &&
                int.TryParse(parts[0], out int minutes) &&
                int.TryParse(parts[2], out int seconds))
            {
                return new TimeSpan(0, minutes, seconds);
            }
            return TimeSpan.Zero;
        }
    }
}
