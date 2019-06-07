using EngineGL.Impl;
using NLog.Config;

namespace EngineGL.FormatMessage
{
    public class GameBuilder
    {
        private Game _game;

        public GameBuilder()
        {
            _game = new Game();
        }

        public GameBuilder setTitle(string title)
        {
            _game.Title = title;
            return this;
        }

        public GameBuilder setExceptinDialog(bool exceptionDialog)
        {
            _game.ExceptionDialog = exceptionDialog;
            return this;
        }

        public GameBuilder setDebugLogging(bool debugLogging)
        {
            _game.DebugLogging = debugLogging;
            return this;
        }


        public GameBuilder setLoggingConfiguration(LoggingConfiguration loggingConfiguration)
        {
            _game.LoggingConfiguration = loggingConfiguration;
            return this;
        }
    }
}