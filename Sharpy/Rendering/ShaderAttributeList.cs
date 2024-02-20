using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Rendering
{

    /// <summary>
    /// Custom list for shader attribute.
    /// </summary>
    /// <remarks>Shader attribute list can only be set and not reset, so all remove methods throw exceptions</remarks>
    public class ShaderAttributeList : List<ShaderAttribute>
    {

        #region Properties

        /// <summary>
        /// Gets stride value
        /// </summary>
        public uint Stride { get; private set; } = 0;

        #endregion


        #region Overriden methods

        public new void Add(ShaderAttribute t_attribute)
        {
            t_attribute.m_unOffset = Stride;
            Stride += t_attribute.m_unSize;
            base.Add(t_attribute);
        }

        public new void AddRange(IEnumerable<ShaderAttribute> t_rgAttributes)
        {
            foreach(ShaderAttribute attribute in t_rgAttributes)
            {
                Add(attribute);
            }
        }

        public new void Clear()
        {
            Stride = 0;
            base.Clear();
        }

        public new void Remove(ShaderAttribute t_attribute)
        {
            throw new NotImplementedException();
        }

        public new void RemoveAll(Predicate<ShaderAttribute> t_match)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}
