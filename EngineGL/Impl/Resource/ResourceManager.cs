using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using EngineGL.Core.Resource;
using NAudio.Wave;
using OpenTK;
using OpenTK.Audio.OpenAL;
using OpenTK.Graphics.OpenGL;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace EngineGL.Impl.Resource
{
    public static class ResourceManager
    {
        public static ITexture LoadTexture2D(string filePath)
        {
            Bitmap bitmap = new Bitmap(filePath);
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            int ptr = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, ptr);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int) TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int) TextureMagFilter.Linear);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bitmap.UnlockBits(data);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            return new Texture2D(filePath, bitmap, ptr);
        }

        public static void UnloadTexture(ITexture texture)
        {
            texture.Dispose();
        }

        public static IAudio LoadWave(string filePath)
        {
            IntPtr device = Alc.OpenDevice(null);
            ContextHandle handle = Alc.CreateContext(device, (int[]) null);
            Alc.MakeContextCurrent(handle);

            int buffer = AL.GenBuffer();
            int source = AL.GenSource();

            WaveFileReader reader = new WaveFileReader(filePath);
            byte[] data = new byte[reader.Length];
            reader.Read(data, 0, data.Length);

            AL.BufferData(buffer, GetSoundFormat(reader.WaveFormat.Channels, reader.WaveFormat.BitsPerSample), data,
                data.Length, reader.WaveFormat.SampleRate);
            AL.Source(source, ALSourcei.Buffer, buffer);

            return new WaveAudio(filePath, source, buffer, handle);
        }

        private static ALFormat GetSoundFormat(int channels, int bits)
        {
            switch (channels)
            {
                case 1: return bits == 8 ? ALFormat.Mono8 : ALFormat.Mono16;
                case 2: return bits == 8 ? ALFormat.Stereo8 : ALFormat.Stereo16;
                default: throw new NotSupportedException("The specified sound format is not supported.");
            }
        }

        public static void UnloadWave(IAudio audio)
        {
            audio.Dispose();
        }
    }
}