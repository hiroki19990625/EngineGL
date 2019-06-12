using System;
using EngineGL.Core.Resource;
using NAudio.Wave;
using Newtonsoft.Json;
using OpenTK;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace EngineGL.Impl.Resource
{
    public class WaveAudio : IAudio
    {
        public string FileName { get; }
        [JsonIgnore] public int SourceHash { get; }
        [JsonIgnore] public int BufferHash { get; }

        private ContextHandle _handle;

        public WaveAudio(string fileName, int source, int buffer, ContextHandle handle)
        {
            FileName = fileName;

            SourceHash = source;
            BufferHash = buffer;
            _handle = handle;
        }

        public void Play()
        {
            AL.BindBufferToSource(SourceHash, BufferHash);
            AL.SourcePlay(SourceHash);
        }

        public void Parse()
        {
            AL.BindBufferToSource(SourceHash, BufferHash);
            AL.SourcePause(SourceHash);
        }

        public void Stop()
        {
            AL.BindBufferToSource(SourceHash, BufferHash);
            AL.SourceStop(SourceHash);
        }

        public void SetLoop(bool loop)
        {
            AL.BindBufferToSource(SourceHash, BufferHash);
            AL.Source(SourceHash, ALSourceb.Looping, loop);
        }

        public void SetVolume(float volume)
        {
            AL.BindBufferToSource(SourceHash, BufferHash);
            AL.Listener(ALListenerf.Gain, volume);
        }

        public int GetState()
        {
            AL.BindBufferToSource(SourceHash, BufferHash);
            AL.GetSource(SourceHash, ALGetSourcei.SourceState, out int state);
            return state;
        }

        public void Dispose()
        {
            Stop();
            AL.DeleteSource(SourceHash);
            AL.DeleteBuffer(BufferHash);
            Alc.DestroyContext(_handle);
        }
    }
}