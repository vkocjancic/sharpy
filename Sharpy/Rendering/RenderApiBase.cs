using Sharpy.Logging;
using Sharpy.Rendering.OpenGL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Rendering
{

    /// <summary>
    /// Base class for render APIs
    /// </summary>
    public abstract class RenderApiBase
    {

        #region Declarations

        protected static RenderApiBase? m_sapiInstance;
        protected static object m_oLock = new object();

        #endregion


        #region Properties

        /// <summary>
        /// Get / Set renderer context
        /// </summary>
        public RenderContextBase? RenderContext { get; set; }

        #endregion


        #region Abstract methods

        /// <summary>
        /// Clean up buffers
        /// </summary>
        /// <param name="obj">Object for which buffers need to be cleaned up</param>
        public abstract void Close(RenderableObjectBase obj);

        /// <summary>
        /// Draw renderable object to the screen
        /// </summary>
        /// <param name="obj">Object to draw</param>
        public abstract void Draw(RenderableObjectBase obj);

        /// <summary>
        /// Initializes renderable object on api
        /// </summary>
        /// <param name="obj"></param>
        public abstract void Init(RenderableObjectBase obj);

        #endregion


        #region Factory methods

        /// <summary>
        /// Get instance. If it does not exist, create one.
        /// </summary>
        /// <param name="t_ctxRender">Render graphics context</param>
        /// <returns>Redner API instance</returns>
        public static RenderApiBase GetInstance(RenderContextBase t_ctxRender)
        {
            lock (m_oLock)
            {
                if (null == m_sapiInstance)
                {
                    switch(t_ctxRender)
                    {
                        case OpenGlRenderContext:
                            m_sapiInstance = new OpenGlRenderApi();
                            m_sapiInstance.RenderContext = t_ctxRender;
                            break;
                        default:
                            SharpyAssert.Fail("Unknown render context");
                            break;
                    }
                }
            }
            return m_sapiInstance;
        }

        /// <summary>
        /// Get instance, if it is already created. If not, assertion is triggered!
        /// </summary>
        /// <returns>Render API instance</returns>
        public static RenderApiBase GetInstance()
        {
            SharpyAssert.Assert(null != m_sapiInstance, "Instance is not set!");
            return m_sapiInstance;
        }

        #endregion

    }
}
