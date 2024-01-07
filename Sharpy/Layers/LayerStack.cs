using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Layers
{

    /// <summary>
    /// Stack for storing all layers used by the engine
    /// </summary>
    /// <remarks>
    /// The stack takes care of overlays being allways at the end of the stack. Becaus eoverlays need to be at the end.
    /// Normal layers are inserted mid stack.
    /// </remarks>
    public class LayerStack : IEnumerable<LayerBase>
    {

        #region Fields

        /// <summary>
        /// List containing all layers
        /// </summary>
        private List<LayerBase> m_stackLayers = new List<LayerBase>();

        /// <summary>
        /// Start index of overlay layers
        /// </summary>
        private int m_nOverlayBegin = 0;

        #endregion


        #region IEnumerable implementation

        public IEnumerator<LayerBase> GetEnumerator()
        {
            return m_stackLayers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion


        #region Public members

        /// <summary>
        /// Gets layer from stack by index (enables to have normal for loop)
        /// </summary>
        /// <param name="index">Index of item to peek</param>
        /// <returns>Item peeked</returns>
        public LayerBase this[int index]
        {
            get { return m_stackLayers[index]; }
            protected set { m_stackLayers[index] = value; }
        }

        /// <summary>
        /// Push layer to stack
        /// </summary>
        /// <param name="t_layer">Layer to push to stack</param>
        public void PushLayer(LayerBase t_layer)
        {
            Debug.Assert(t_layer != null);
            m_stackLayers.Insert(m_nOverlayBegin, t_layer);
            m_nOverlayBegin++;
        }

        /// <summary>
        /// Push overlay layer to stack
        /// </summary>
        /// <param name="t_layer">Overlay layer to push to stack</param>
        public void PushOverlay(LayerBase t_layer)
        {
            Debug.Assert(t_layer != null);
            m_stackLayers.Add(t_layer);
        }

        /// <summary>
        /// Pop layer from stack
        /// </summary>
        /// <returns>Layer removed from stack</returns>
        public LayerBase PopLayer()
        {
            int nLayerEnd = m_nOverlayBegin - 1;
            LayerBase layer = m_stackLayers[nLayerEnd];
            m_stackLayers.RemoveAt(nLayerEnd);
            m_nOverlayBegin = nLayerEnd;
            return layer;
        }

        /// <summary>
        /// Pop overlay layer from stack
        /// </summary>
        /// <returns>Overlay layer removed from stack</returns>
        public LayerBase PopOverlay()
        {
            int nOverlayEnd = m_stackLayers.Count() - 1;
            Debug.Assert(m_nOverlayBegin <= nOverlayEnd);
            LayerBase layer = m_stackLayers[nOverlayEnd];
            m_stackLayers.RemoveAt(nOverlayEnd);
            return layer;
        }

        #endregion

    }
}
