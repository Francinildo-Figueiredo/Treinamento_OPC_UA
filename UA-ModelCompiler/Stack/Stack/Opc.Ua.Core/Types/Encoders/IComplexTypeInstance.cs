/* Copyright (c) 1996-2022 The OPC Foundation. All rights reserved.
   The source code in this file is covered under a dual-license scenario:
     - RCL: for OPC Foundation Corporate Members in good-standing
     - GPL V2: everybody else
   RCL license terms accompanied with this source code. See http://opcfoundation.org/License/RCL/1.00/
   GNU General Public License as published by the Free Software Foundation;
   version 2 of the License are accompanied with this source code. See http://opcfoundation.org/License/GPLv2
   This source code is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
*/

using System.Xml;

namespace Opc.Ua
{
    /// <summary>
    /// Interface implemented by complex data type instances.
    /// </summary>
    public interface IComplexTypeInstance
    {
        /// <summary>
        /// Gets or sets the UA type identifier for the encodeable type.
        /// </summary>
        /// <value>
        /// The UA type identifier.
        /// </value>
        ExpandedNodeId TypeId { get; set; }
    }

    /// <summary>
    /// Interface implemented by complex data type instances that discover and provide type information dynamically
    /// </summary>
    public interface IDynamicComplexTypeInstance : IComplexTypeInstance
    {
        /// <summary>
        /// Returns the xml qualified name for the type instance
        /// </summary>
        /// <returns>The xml qualified name for the type instance</returns>
        XmlQualifiedName GetXmlName(IServiceMessageContext context);
    }

}
