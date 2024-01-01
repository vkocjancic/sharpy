using Sharpy.EntryPoint;

internal class Program
{
    private static void Main(string[] args)
    {
        var app = new SharpyApplication();
        // TODO: do something useful with events
        app.EventDispatcher.KeyPressed += (s, e) => { };
        app.EventDispatcher.KeyReleased += (s, e) => { };
        app.EventDispatcher.MouseButtonPressed += (s, e) => { };
        app.EventDispatcher.MouseButtonReleased += (s, e) => { };
        app.EventDispatcher.MouseMoved += (s, e) => { };
        app.EventDispatcher.MouseScrolled += (s, e) => { };
        app.EventDispatcher.WindowResize += (s, e) => { };
        app.Run();
    }
}