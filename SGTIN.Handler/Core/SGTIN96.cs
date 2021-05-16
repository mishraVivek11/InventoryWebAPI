using SGTIN.Handler.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGTIN.Handler
{
    /// <summary>
    /// SGTIN96 converter implementation
    /// </summary>
    public class SGTIN96 : ISGTINConverter
    {
        private readonly string defaultHeaderValue = "00110000";
        private readonly int encodingLength = 96;
        private readonly int headerLength = 8;
        private readonly int filterLength = 3;
        private readonly int partitionLength = 3;
        private readonly Dictionary<int, PartitionValue> partitionMap = new Dictionary<int, PartitionValue>()
        {
            {0, new PartitionValue(40,4)},
            {1, new PartitionValue(37,7)},
            {2, new PartitionValue(34,10)},
            {3, new PartitionValue(30,14)},
            {4, new PartitionValue(27,17)},
            {5, new PartitionValue(24,20)},
            {6, new PartitionValue(20,24)}
        };

        /// <summary>
        /// Returns decoded tag information for a SGTIN96 EPC.
        /// </summary>
        /// <param name="sgtinEPC">sgtinEPC string</param>
        /// <returns>Tag object</returns>
        public Tag GetTagFromEPC(string sgtinEPC)
        {
            string binaryRep = GetBinaryFromEPCString(sgtinEPC);
            ValidateSGTINEPC(binaryRep);

            string header = binaryRep.Substring(0, headerLength);

            if (header != defaultHeaderValue)
                throw new FormatException("Invalid header");

            int headerValue = (Int32)GetDecimalFromBinary(header);

            string filter = binaryRep.Substring(headerLength, filterLength);
            int filterValue = (Int32)GetDecimalFromBinary(filter);

            string partition = binaryRep.Substring(headerLength + filterLength, partitionLength);
            int partitionValue = (Int32)GetDecimalFromBinary(partition);

            PartitionValue partitionMapValue = partitionMap[partitionValue];

            string companyPrefix = binaryRep.Substring(headerLength + filterLength + partitionLength, partitionMapValue.CompanyPrefixLength);
            long companyPrefixValue = GetDecimalFromBinary(companyPrefix);

            string itemRef = binaryRep.Substring(headerLength + filterLength + partitionLength + partitionMapValue.CompanyPrefixLength, partitionMapValue.ItemReferenceLength);
            int itemRefValue = (Int32)GetDecimalFromBinary(itemRef);

            string serialRef = binaryRep.Substring(headerLength + filterLength + partitionLength + partitionMapValue.CompanyPrefixLength + partitionMapValue.ItemReferenceLength + 2);
            long serialRefValue = GetDecimalFromBinary(serialRef);

            return new Tag(headerValue, filterValue, partitionValue, companyPrefixValue, itemRefValue, serialRefValue);
        }

        /// <summary>
        /// Convaerts EPC(Hex) string to binary string.
        /// </summary>
        /// <param name="sgtinEPC">Hex string for SGTIN 96</param>
        /// <returns>binary string</returns>
        private string GetBinaryFromEPCString(string sgtinEPC)
        {
            string binarystring = String.Join(String.Empty, sgtinEPC.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
            return binarystring;
        }

        /// <summary>
        /// Gets decimal value from binary.
        /// </summary>
        /// <param name="binaryValue">binary string</param>
        /// <returns>long decimal value</returns>
        private long GetDecimalFromBinary(string binaryValue)
        {
            long decimalValue;
            decimalValue = Convert.ToInt64(binaryValue, 2);           
            return decimalValue;
        }

        /// <summary>
        /// Checks if the HEX has a valid binary string value.
        /// For SGTIN 96 binary string length should be 96
        /// </summary>
        /// <param name="binaryEPC"></param>
        private void ValidateSGTINEPC(string binaryEPC)
        {
            if (binaryEPC.Length != encodingLength)
                throw new FormatException("Invalid input");
        }
    }
}
