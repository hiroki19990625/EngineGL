using System;
using NAudio.Wave;

namespace EngineGL.Core.Resource
{
    public interface IAudio : IDisposable
    {
        string FileName { get; }
        int SourceHash { get; }
        int BufferHash { get; }

        void Play();
        void Parse();
        void Stop();

        void SetLoop(bool loop);
        void SetVolume(float volume);
        int GetState();
    }
}