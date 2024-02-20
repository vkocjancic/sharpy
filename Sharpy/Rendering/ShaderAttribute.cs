using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Rendering
{

    /// <summary>
    /// Contains shader attribute properties
    /// </summary>
    public struct ShaderAttribute
    {

        #region Declarations

        public enum DataType
        {
            None = 0,
            Float,
            Float2,
            Float3,
            Float4
        };

        #endregion


        #region Fields

        public bool m_bNormalized;
        public string m_sName;
        public uint m_unOffset;
        public uint m_unSize;
        public DataType m_typeOfData;

        #endregion


        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="t_typeOfData">Data type value</param>
        /// <param name="t_sName">Attribute name</param>
        /// <param name="t_bNormalized">Is data normalized or not. Defaults to false or not normalized</param>
        public ShaderAttribute(DataType t_typeOfData, string t_sName, bool t_bNormalized = false)
        {
            m_bNormalized = t_bNormalized;
            m_sName = t_sName;
            m_unOffset = 0;
            m_unSize = GetDataTypeSize(t_typeOfData);
            m_typeOfData = t_typeOfData;
        }

        #endregion


        #region Public methods

        /// <summary>
        /// Gets byte size for attribute type
        /// </summary>
        /// <returns>Byte size for attribute type. If no match is found, it asserts and returns 0.</returns>
        public int GetComponentCount()
        {
            switch (m_typeOfData)
            {
                case DataType.Float: 
                    return 1;
                case DataType.Float2: 
                    return 2;
                case DataType.Float3: 
                    return 3;
                case DataType.Float4: 
                    return 4;
            }

            Debug.Fail("Unknown shader attribute data type");
            return 0;
        }

        #endregion


        #region Helper methods

        /// <summary>
        /// Gets size for DataType enum type
        /// </summary>
        /// <param name="t_typeOfData">Data type</param>
        /// <returns>Data type size</returns>
        private static uint GetDataTypeSize(DataType t_typeOfData)
        {
            switch (t_typeOfData)
            {
                case DataType.Float:
                    return sizeof(float);
                case DataType.Float2:
                    return sizeof(float) * 2;
                case DataType.Float3:
                    return sizeof(float) * 3;
                case DataType.Float4:
                    return sizeof(float) * 4;
            }
            Debug.Fail("Unknown shader attribute data type");
            return 0;
        }

        #endregion

    }
}
