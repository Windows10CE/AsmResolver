// AsmResolver - Executable file format inspection library 
// Copyright (C) 2016-2019 Washi
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 3.0 of the License, or (at your option) any later version.
// 
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA

namespace AsmResolver.PE.DotNet.Metadata.Tables.Rows
{
    /// <summary>
    /// Represents a single row in the constants metadata table.
    /// </summary>
    public readonly struct ConstantRow : IMetadataRow
    {
        /// <summary>
        /// Reads a single constant row from an input stream.
        /// </summary>
        /// <param name="reader">The input stream.</param>
        /// <param name="layout">The layout of the constants table.</param>
        /// <returns>The row.</returns>
        public static ConstantRow FromReader(IBinaryStreamReader reader, TableLayout layout)
        {
            return new ConstantRow(
                (ElementType) reader.ReadByte(),
                reader.ReadByte(),
                reader.ReadIndex((IndexSize) layout.Columns[2].Size),
                reader.ReadIndex((IndexSize) layout.Columns[3].Size));
        }
        
        public ConstantRow(ElementType type, byte padding, uint parent, uint value)
        {
            Type = type;
            Padding = padding;
            Parent = parent;
            Value = value;
        }

        /// <inheritdoc />
        public TableIndex TableIndex => TableIndex.Constant;

        /// <summary>
        /// Gets the type of constant that is stored in the blob stream. 
        /// </summary>
        /// <remarks>This field must always be a value-type.</remarks>
        public ElementType Type
        {
            get;
        }

        /// <summary>
        /// Gets the single padding byte between the type and parent columns.
        /// </summary>
        /// <remarks>This field should always be zero.</remarks>
        public byte Padding
        {
            get;
        }

        /// <summary>
        /// Gets a HasConstant index (an index into either the Field, Parameter or Property table) that is the owner
        /// of the constant. 
        /// </summary>
        public uint Parent
        {
            get;
        }

        /// <summary>
        /// Gets an index into the #Blob stream containing the serialized constant value.
        /// </summary>
        public uint Value
        {
            get;
        }

        /// <summary>
        /// Determines whether this row is considered equal to the provided constant row.
        /// </summary>
        /// <param name="other">The other row.</param>
        /// <returns><c>true</c> if the rows are equal, <c>false</c> otherwise.</returns>
        public bool Equals(ConstantRow other)
        {
            return Type == other.Type && Padding == other.Padding && Parent == other.Parent && Value == other.Value;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is ConstantRow other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (int) Type;
                hashCode = (hashCode * 397) ^ Padding.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) Parent;
                hashCode = (hashCode * 397) ^ (int) Value;
                return hashCode;
            }
        }
        
    }
}
