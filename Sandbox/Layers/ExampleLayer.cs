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

        #region LayerBase implementation

        public override void OnClose()
        {
            Log.Info("APP: Close");
        }

        public override void OnEvent(EventArgsBase t_evtArgs)
        {
            Log.Info("APP: {0}", t_evtArgs);
        }

        public override void OnInit(WindowBase window)
        {
            Log.Info("APP: Init");
        }

        public override void OnRender(double t_fElapsedTime)
        {
            var apiRender = RenderApi.GetInstance();
            apiRender.DrawQuad(
                new Vector2D<decimal>(-0.5M, 0.5M),
                new Vector2D<decimal>(0.5M, -0.5M)
            );
        }

        public override void OnUpdate(double t_fElapsedTime)
        {
            
        }

        #endregion

    }
}
