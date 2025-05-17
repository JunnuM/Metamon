using NAudio.Wave;

namespace Metamon.Game
{
    public static class Speakers
    {
        public enum SoundKey
        {
            CROAK,
            MUSIC
        }

        private static readonly Dictionary<SoundKey, string> _soundFiles = new()
        {
            { SoundKey.CROAK, "Sounds/croak.mp3" },
            { SoundKey.MUSIC, "Sounds/music.wav" } // Music composed by Juhani Junkala
        };

        public static void Play(SoundKey soundEffect, bool loop = false)
        {
            var path = _soundFiles[soundEffect];

            try
            {
                var reader = new AudioFileReader(path);
                WaveStream stream = loop ? new LoopStream(reader) : reader;

                var outputDevice = new WaveOutEvent();
                outputDevice.Init(stream);
                outputDevice.Play();

                // Optional: Cleanup on non-looping sounds
                if (!loop)
                {
                    outputDevice.PlaybackStopped += (s, e) =>
                    {
                        outputDevice.Dispose();
                        stream.Dispose();
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to play sound: {ex.Message}");
            }
        }
    }

    public class LoopStream : WaveStream
    {
        private readonly WaveStream _sourceStream;

        public LoopStream(WaveStream sourceStream)
        {
            _sourceStream = sourceStream;
        }

        public override WaveFormat WaveFormat => _sourceStream.WaveFormat;
        public override long Length => long.MaxValue; // Pretend it's infinite
        public override long Position
        {
            get => _sourceStream.Position;
            set => _sourceStream.Position = value;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalBytesRead = 0;

            while (totalBytesRead < count)
            {
                int bytesRead = _sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
                if (bytesRead == 0)
                {
                    _sourceStream.Position = 0; // Loop
                }
                else
                {
                    totalBytesRead += bytesRead;
                }
            }

            return totalBytesRead;
        }
    }
}
