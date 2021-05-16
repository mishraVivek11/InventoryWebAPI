namespace SGTIN.Handler.Models
{
    /// <summary>
    /// Tag model class to hold all the decoded values from the SGTIN EPC.
    /// </summary>
    public class Tag
    {
        private readonly int header;
        private readonly int filter;
        private readonly int partition;
        private readonly long companyPrefix;
        private readonly int itemReference;
        private readonly long serialReference;

        public int Header { get { return header; } }
        public int Filter { get { return filter; } }
        public int Partition { get { return partition; } }
        public long CompanyPrefix { get { return companyPrefix; } }
        public int ItemReference { get { return itemReference; } }
        public long SerialReference { get { return serialReference; } }
       
        public Tag(int header, int filter, int partition, long companyPrefix, int itemReference, long serialReference)
        {
            this.header = header;
            this.filter = filter;
            this.partition = partition;
            this.companyPrefix = companyPrefix;
            this.itemReference = itemReference;
            this.serialReference = serialReference;
        }

    }
}
