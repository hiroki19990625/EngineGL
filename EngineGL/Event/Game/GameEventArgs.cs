using System;
using EngineGL.Core;

namespace EngineGL.Event.Game
{
    public class GameEventArgs : EventArgs
    {
        public IGame Game { get; }

        public GameEventArgs(IGame game)
        {
            Game = game;
        }
    }
}