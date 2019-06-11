using System;
using NAudio.Wave;

namespace EngineGL.Core.Resource
{
    public interface IAudio : IDisposable
    {
        string FileName { get; }
        WaveFileReader Reader { get; }

        void Play();
        void Parse();
        void Stop();
        PlaybackState GetState();
    }
}