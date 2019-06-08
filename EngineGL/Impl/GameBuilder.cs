using System;
using System.Drawing;
using NLog.Config;
using OpenTK;

namespace EngineGL.Impl
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

        public GameBuilder SetExceptionExit(bool exit)
        {
            _game.ExceptionExit = exit;
            return this;
        }

        public GameBuilder SetExceptinDialog(bool exceptionDialog)
        {
            _game.ExceptionDialog = exceptionDialog;
            return this;
        }

        public GameBuilder SetShowExitErrorDialog(bool exitErrorDialog)
        {
            _game.ShowExitErrorDialog = exitErrorDialog;
            return this;
        }

        public GameBuilder SetDebugLogging(bool debugLogging)
        {
            _game.DebugLogging = debugLogging;
            return this;
        }

        public GameBuilder SetVSync(VSyncMode mode)
        {
            _game.VSync = mode;
            return this;
        }

        public GameBuilder SetWindowState(WindowState state)
        {
            _game.WindowState = state;
            return this;
        }

        public GameBuilder SetClientSize(Size size)
        {
            _game.ClientSize = size;
            return this;
        }

        public GameBuilder SetIcon(Icon icon)
        {
            _game.Icon = icon;
            return this;
        }

        public GameBuilder SetWindowBorder(WindowBorder border)
        {
            _game.WindowBorder = border;
            return this;
        }

        public GameBuilder SetLoggingConfiguration(LoggingConfiguration loggingConfiguration)
        {
            _game.LoggingConfiguration = loggingConfiguration;
            return this;
        }

        public GameBuilder AddLoadEvent(EventHandler<EventArgs> action)
        {
            _game.Load += action;
            return this;
        }

        public GameBuilder AddResizeEvent(EventHandler<EventArgs> action)
        {
            _game.Resize += action;
            return this;
        }

        public GameBuilder AddRenderFrameEvent(EventHandler<FrameEventArgs> action)
        {
            _game.RenderFrame += action;
            return this;
        }

        public Game Build()
        {
            return _game;
        }
    }
}