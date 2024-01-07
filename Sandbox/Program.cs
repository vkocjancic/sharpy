using Sandbox.Layers;
using Sharpy.EntryPoint;

internal class Program
{
    private static void Main(string[] args)
    {
        var app = new SharpyApplication();
        app.m_stackLayers.PushLayer(new ExampleLayer());
        app.Run();
    }
}