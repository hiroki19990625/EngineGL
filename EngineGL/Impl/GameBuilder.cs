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

        public GameBuilder SetTitle(string title)
        {
            _game.Title = title;
            return this;
        }

        public GameBuilder SetExceptinDialog(bool exceptionDialog)
        {
            _game.ExceptionDialog = exceptionDialog;
            return this;
        }

        public GameBuilder SetDebugLogging(bool debugLogging)
        {
            _game.DebugLogging = debugLogging;
            return this;
        }


        public GameBuilder SetLoggingConfiguration(LoggingConfiguration loggingConfiguration)
        {
            _game.LoggingConfiguration = loggingConfiguration;
            return this;
        }

        public Game Build()
        {
            return _game;
        }
    }
}