using System;

namespace EngineGL.Core.Resource
{
    /// <summary>
    /// 再生可能な音を実装します。
    /// </summary>
    public interface IAudio : IDisposable
    {
        /// <summary>
        /// 音のファイル名を表します。
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// 音ソースのメモリハッシュ。
        /// </summary>
        int SourceHash { get; }

        /// <summary>
        /// 音バッファーのメモリハッシュ。
        /// </summary>
        int BufferHash { get; }

        /// <summary>
        /// 音を再生します。
        /// </summary>
        void Play();

        /// <summary>
        /// 音を一時停止します。
        /// </summary>
        void Parse();

        /// <summary>
        /// 音を停止します。
        /// </summary>
        void Stop();

        /// <summary>
        /// ループ再生を設定します。
        /// </summary>
        /// <param name="loop"></param>
        void SetLoop(bool loop);

        /// <summary>
        /// 音のボリュームを設定します。
        /// </summary>
        /// <param name="volume"></param>
        void SetVolume(float volume);

        /// <summary>
        /// 音の状態を取得します。
        /// </summary>
        /// <returns></returns>
        int GetState();
    }
}