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
            Log.Debug("APP: {0}", t_evtArgs);
        }

        public override void OnRender()
        {
            throw new NotImplementedException();
        }

        public override void OnUpdate()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
