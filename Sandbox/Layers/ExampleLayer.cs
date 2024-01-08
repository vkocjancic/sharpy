using Sharpy.Events;
using Sharpy.Layers;
using Sharpy.Logging;
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

        public override void OnEvent(EventArgsBase t_evtArgs)
        {
            Log.Info("APP: {0}", t_evtArgs);
        }

        public override void OnRender(double t_fElapsedTime)
        {
            Log.Debug("APP: Render {0} ms", t_fElapsedTime);
        }

        public override void OnUpdate(double t_fElapsedTime)
        {
            Log.Debug("APP: Update {0} ms", t_fElapsedTime);
        }

        #endregion

    }
}
