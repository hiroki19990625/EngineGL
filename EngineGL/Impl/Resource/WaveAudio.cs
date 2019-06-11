using System;
using EngineGL.Core.Resource;
using NAudio.Wave;
using Newtonsoft.Json;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace EngineGL.Impl.Resource
{
    public class WaveAudio : IAudio
    {
        public string FileName { get; }
        [JsonIgnore] public WaveFileReader Reader { get; }
        private WaveOut _wave;

        public WaveAudio(string fileName, WaveFileReader reader)
        {
            FileName = fileName;
            Reader = reader;

            _wave = new WaveOut();
            _wave.Init(reader);
        }

        public void Play()
        {
            _wave.Play();
        }

        public void Parse()
        {
            _wave.Pause();
        }

        public void Stop()
        {
            _wave.Stop();
        }

        public PlaybackState GetState()
        {
            return _wave.PlaybackState;
        }

        public void Dispose()
        {
            _wave.Dispose();
            Reader.Dispose();
        }
    }
}