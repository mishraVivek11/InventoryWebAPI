using System;
using System.Collections.Generic;
using System.Text;

namespace SGTIN.Handler.Models
{
    internal class PartitionValue
    {
        private readonly int companyPrefixLength;
        private readonly int itemReferenceLength;

        public int CompanyPrefixLength { get { return companyPrefixLength; } }

        public int ItemReferenceLength { get { return itemReferenceLength; } }

        public PartitionValue(int companyPrefixLength, int itemReferenceLength)
        {
            this.companyPrefixLength = companyPrefixLength;
            this.itemReferenceLength = itemReferenceLength;
        }
    }
}
