using Sandbox.Assets.ExampleQuad;
using Sharpy.Events;
using Sharpy.Layers;
using Sharpy.Logging;
using Sharpy.Rendering;
using Sharpy.Window;
using Silk.NET.Maths;
using Silk.NET.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Layers
{
    public class ExampleLayer : LayerBase
    {

        #region Fields

        private ExampleQuad m_robjQuad = new ExampleQuad();

        #endregion


        #region LayerBase implementation

        public override void OnClose()
        {
            Log.Info("APP: Close");
            m_robjQuad.Close();
        }

        public override void OnEvent(EventArgsBase t_evtArgs)
        {
            Log.Info("APP: {0}", t_evtArgs);
        }

        public override void OnInit(WindowBase window)
        {
            Log.Info("APP: Init");
            m_robjQuad.Init(); 
        }

        public override void OnRender(double t_fElapsedTime)
        {
            m_robjQuad.Render();
        }

        public override void OnUpdate(double t_fElapsedTime)
        {
            
        }

        #endregion

    }
}
