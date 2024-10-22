
using Sudoku_new_algorithm.Audio;
using Sudoku_new_algorithm.Board;
using Sudoku_new_algorithm.Game;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        AudioPlayer audioPlayer = new AudioPlayer();
        string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Soundtracks", "sudoku.mp3");

        if (File.Exists(soundPath))
        {
            audioPlayer.PlaySound(soundPath);
        }
        else
        {
            Console.WriteLine("Файл sudoku.mp3 не найден.");
        }

        IBoardGenerator boardGenerator = new BoardGenerator();
        IGame game = new Game(boardGenerator);
        game.MainMenu();
    }
}
