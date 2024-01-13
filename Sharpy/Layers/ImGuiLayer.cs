using Sharpy.Events;
using Sharpy.Window;
using Silk.NET.OpenGL.Extensions.ImGui;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Layers
{

    /// <summary>
    /// Layer for ImGui
    /// </summary>
    /// <remarks>Included in Sharpy as a bonus</remarks>
    public class ImGuiLayer : LayerBase
    {

        #region Declarations

        private ImGuiController? m_ctrlImGui = null;

        #endregion

        #region LayerBase implementation

        public override void OnClose()
        {
            m_ctrlImGui?.Dispose();
        }

        public override void OnEvent(EventArgsBase t_evtArgs)
        {
            // we don't need to handle this event, because ImGuiController takes care of that for us
        }

        public override void OnInit(WindowBase window)
        {
            Debug.Assert(window is WindowsWindow, "imGui only supports OpenGL");
            var winCtx = ((WindowsWindow)window).GetWindowContext();
            m_ctrlImGui = new ImGuiController(
                winCtx.Item1,
                winCtx.Item2,
                winCtx.Item3
            );
        }

        public override void OnRender(double t_fElapsedTime)
        {
            if (null == m_ctrlImGui)
            {
                return;
            }
            ImGuiNET.ImGui.ShowDemoWindow();
            m_ctrlImGui.Render();
        }

        public override void OnUpdate(double t_fElapsedTime)
        {
            m_ctrlImGui?.Update((float)t_fElapsedTime);
        }

        #endregion

    }
}
