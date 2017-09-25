using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grm
{
    /* create multiple music contracts from multiple usages */
    public class MusicContractsBuilder
    {
        private const char USAGE_DELIMITTER = ',';

        private List<MusicContract> musicContracts = new List<MusicContract>();

        public  MusicContractsBuilder(String artist, String title, String Usages, DateTime startDate, DateTime endDate)
        {
        
            foreach (var usage in Usages.Split(USAGE_DELIMITTER)) {
                musicContracts.Add(new MusicContract(artist, title, usage.Trim(), startDate, endDate));
            }
      
        }

        public List<MusicContract> GetMusicContracts()
        {
            return musicContracts;
        }
    }
}
