using SGTIN.Handler.Models;

namespace SGTIN.Handler
{
    public interface ISGTINConverter
    {
        /// <summary>
        /// Returns decoded tag information for a SGTIN96 EPC.
        /// </summary>
        /// <param name="sgtinEPC">sgtinEPC string</param>
        /// <returns>Tag object</returns>
        Tag GetTagFromEPC(string sgtinEPC);
    }
}
