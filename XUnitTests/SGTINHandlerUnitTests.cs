using SGTIN.Handler;
using SGTIN.Handler.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTests
{
    public class SGTINHandlerUnitTests
    {
        private readonly SGTIN96 sut;
        public SGTINHandlerUnitTests()
        {
            sut = new SGTIN96();
        }

        
        [Theory]
        [InlineData(3, "3074257BF7194E4000001A85")]
        [InlineData(1, "303A6135128F9C8012E981F1")]
        [InlineData(2, "3058325EA2A9DB800C6ED957")]
        [InlineData(1, "3038438104C7E1003001F9F7")]
        public void DecodeEPCSGTIN96ToValidFilterValue( int expFilter, string sgtinEPC)
        {
            Tag tag = sut.GetTagFromEPC(sgtinEPC);

            Assert.Equal(expFilter, tag.Filter);
        }


        [Theory]
        [InlineData(5, "3074257BF7194E4000001A85")]
        [InlineData(6, "303A6135128F9C8012E981F1")]
        [InlineData(6, "3058325EA2A9DB800C6ED957")]
        [InlineData(6, "3038438104C7E1003001F9F7")]
        public void DecodeEPCSGTIN96ToValidPartitionValue(int expPartition, string sgtinEPC)
        {
            Tag tag = sut.GetTagFromEPC(sgtinEPC);

            Assert.Equal(expPartition, tag.Partition);
        }

        [Theory]
        [InlineData(614141, "3074257BF7194E4000001A85")]
        [InlineData(623828, "303A6135128F9C8012E981F1")]
        [InlineData(51578, "3058325EA2A9DB800C6ED957")]
        [InlineData(69124, "3038438104C7E1003001F9F7")]
        public void DecodeEPCSGTIN96ToValidCompanyPrefixValue(long expCompanyPrfx, string sgtinEPC)
        {
            Tag tag = sut.GetTagFromEPC(sgtinEPC);

            Assert.Equal(expCompanyPrfx, tag.CompanyPrefix);
        }

        [Theory]
        [InlineData(812345, "3074257BF7194E4000001A85")]
        [InlineData(4865650, "303A6135128F9C8012E981F1")]
        [InlineData(9086830, "3058325EA2A9DB800C6ED957")]
        [InlineData(1253252, "3038438104C7E1003001F9F7")]
        public void DecodeEPCSGTIN96ToValidItemReferenceValue(int expItemRef, string sgtinEPC)
        {
            Tag tag = sut.GetTagFromEPC(sgtinEPC);

            Assert.Equal(expItemRef, tag.ItemReference);
        }

        [Theory]
        [InlineData(6789, "3074257BF7194E4000001A85")]
        [InlineData(317293041, "303A6135128F9C8012E981F1")]
        [InlineData(208591191, "3058325EA2A9DB800C6ED957")]
        [InlineData(805435895, "3038438104C7E1003001F9F7")]
        public void DecodeEPCSGTIN96ToValidSerialReferenceValue(long expSerialRef, string sgtinEPC)
        {
            Tag tag = sut.GetTagFromEPC(sgtinEPC);

            Assert.Equal(expSerialRef, tag.SerialReference);
        }

        [Fact]
        public void DecodeEPCSGTIN96ToInvalidPartitionKey()
        {
            string sgtinEPC = "30FF4FFFFFFFFFFFFFFFF9F7";

            Action act = () => sut.GetTagFromEPC(sgtinEPC);

            Assert.Throws<KeyNotFoundException>(act);
        }

        [Fact]
        public void DecodeEPCSGTIN96ToInvalidEncodingLengthExceedingLimit()
        {
            string sgtinEPC = "3058325EA2A9DB800C6ED95745";

            Action act = () => sut.GetTagFromEPC(sgtinEPC);

            Assert.Throws<FormatException>(act);
        }

        [Fact]
        public void DecodeEPCSGTIN96ToInvalidEPCHeaderValue()
        {
            string sgtinEPC = "2038438104C7E1003001F9F7";

            Action act = () => sut.GetTagFromEPC(sgtinEPC);

            Assert.Throws<FormatException>(act);
        }


    }
}
