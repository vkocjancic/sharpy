using Silk.NET.Vulkan.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Window
{

    /// <summary>
    /// Struct used to store window options
    /// </summary>
    public struct WindowOptions
    {

        #region Fields

        /// <summary>
        /// Window height
        /// </summary>
        public uint m_unHeight;

        /// <summary>
        /// Window width
        /// </summary>
        public uint m_unWidth;

        /// <summary>
        /// Window title
        /// </summary>
        public string m_sTitle;

        /// <summary>
        /// VSync
        /// </summary>
        public bool m_bVsyncEnabled;

        #endregion

    }
}
