using NAudio.Wave;

namespace Metamon.Game
{
    public static class Speakers
    {
        public enum SoundKey
        {
            CROAK
        }

        private static readonly Dictionary<SoundKey, string> _soundFiles = new()
        {
            { SoundKey.CROAK,  "Sounds/croak.mp3" },
        };

        public static void Play(SoundKey soundEffect)
        {
            var path = _soundFiles[soundEffect];

            try
            {
                // Using disposal-friendly wrapper
                var audioFile = new AudioFileReader(path);
                var outputDevice = new WaveOutEvent();

                outputDevice.Init(audioFile);
                outputDevice.Play();

                // Cleanup when done
                outputDevice.PlaybackStopped += (s, e) =>
                {
                    outputDevice.Dispose();
                    audioFile.Dispose();
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to play sound: {ex.Message}");
            }
        }
    }
}
