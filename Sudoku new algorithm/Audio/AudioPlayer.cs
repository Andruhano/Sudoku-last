using NAudio.Wave;

namespace Sudoku_new_algorithm.Audio
{
    /// <summary>
    /// Class responsible for playing audio files.
    /// </summary>
    public class AudioPlayer
    {
        /// <summary>
        /// The audio playback device.
        /// </summary>
        private IWavePlayer waveOutDevice;
        /// <summary>
        /// The audio file reader used to read the audio file for playback.
        /// </summary>
        private AudioFileReader audioFileReader;
        /// <summary>
        /// Plays an audio file from the specified file path.
        /// </summary>
        /// <param name="filePath">The path to the audio file to be played.</param>
        public void PlaySound(string filePath)
        {
            Thread playThread = new Thread(() =>
            {
                try
                {
                    waveOutDevice = new WaveOutEvent();
                    audioFileReader = new AudioFileReader(filePath);
                    waveOutDevice.Init(audioFileReader);
                    waveOutDevice.Play();

                    while (waveOutDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(100);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при воспроизведении аудио: {ex.Message}");
                }
                finally
                {
                    Stop(); // Освобождаем ресурсы в случае ошибки
                }
            });

            playThread.Start();
        }
        /// <summary>
        /// Stops the audio playback and releases any resources used.
        /// </summary>
        public void Stop()
        {
            waveOutDevice?.Stop();
            waveOutDevice?.Dispose();
            audioFileReader?.Dispose();
        }
    }
}
