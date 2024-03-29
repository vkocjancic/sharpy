﻿using Sandbox.Layers;
using Sharpy.EntryPoint;

internal class Program
{
    private static void Main(string[] args)
    {
        var app = new SharpyApplication();
        app.m_stackLayers.PushLayer(new ExampleLayer());
        app.m_stackLayers.PushOverlay(new Sharpy.Layers.ImGuiLayer());
        app.Run();
    }
}