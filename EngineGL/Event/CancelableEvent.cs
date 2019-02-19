namespace EngineGL.Event
{
    public interface CancelableEvent
    {
        bool IsCanceled { get; set; }
    }
}