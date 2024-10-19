
using Sudoku_new_algorithm.Audio;
using Sudoku_new_algorithm.Board;
using Sudoku_new_algorithm.Game;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        AudioPlayer audioPlayer = new AudioPlayer();
        audioPlayer.PlaySound(@"C:\Users\andre\Downloads\Sudoku-new-algorithm-master\Sudoku-new-algorithm-master\Soundtracks\sudoku.mp3");
        IBoardGenerator boardGenerator = new BoardGenerator();
        IGame game = new Game(boardGenerator);
        game.MainMenu();
    }
}
