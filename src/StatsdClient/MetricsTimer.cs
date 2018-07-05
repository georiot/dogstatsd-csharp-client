using System;

namespace StatsdClient
{
    public class MetricsTimer : IDisposable
    {
        private readonly Stopwatch _stopWatch;
        private bool _disposed;
        private readonly double _sampleRate;

        public MetricsTimer(string name, double sampleRate = 1.0, string[] tags = null)
        {
            Name = name;
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            _sampleRate = sampleRate;
            Tags = tags;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _stopWatch.Stop();
                DogStatsd.Timer(Name, _stopWatch.ElapsedMilliseconds(), _sampleRate, Tags);
            }
        }

        /// <summary>
        /// The name of this timer. This field is allowed to be mutable so that we can change it after the timer starts.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The tags for this timer. This field is allowed to be mutable so that we can change it after the timer starts.
        /// </summary>
        public string[] Tags { get; set; }
    }
}