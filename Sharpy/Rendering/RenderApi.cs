using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Rendering
{

    /// <summary>
    /// Api class for renderrer
    /// </summary>
    /// <remarks>
    /// This is a singleton class!!!
    /// </remarks>
    public class RenderApi
    {

        #region Declarations

        private static RenderApi? m_sapiInstance;
        private static object m_oLock = new object();

        private RenderContextBase m_ctxRender;

        #endregion


        #region Constructors

        /// <summary>
        /// Instance constructor
        /// </summary>
        /// <param name="t_ctxRender">Render graphics contexts</param>
        private RenderApi(RenderContextBase t_ctxRender) 
        { 
            m_ctxRender = t_ctxRender;
        }

        #endregion


        #region Public members

        /// <summary>
        /// Perform object initialization
        /// </summary>
        public void Init() { }

        #endregion


        #region Factory methods

        /// <summary>
        /// Get instance. If it does not exist, create one.
        /// </summary>
        /// <param name="t_ctxRender">Render graphics context</param>
        /// <returns>Redner API instance</returns>
        public static RenderApi GetInstance(RenderContextBase t_ctxRender)
        {
            lock(m_oLock)
            {
                if (null == m_sapiInstance) 
                {
                    m_sapiInstance = new RenderApi(t_ctxRender);
                }
            }
            return m_sapiInstance;
        }

        /// <summary>
        /// Get instance, if it is already created. If not, assertion is triggered!
        /// </summary>
        /// <returns>Render API instance</returns>
        public static RenderApi GetInstance()
        {
            Debug.Assert(null != m_sapiInstance, "Instance is not set!");
            return m_sapiInstance;
        }

        #endregion

    }

}
