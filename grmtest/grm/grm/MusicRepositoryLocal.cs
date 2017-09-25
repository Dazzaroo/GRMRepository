using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grm
{
    /* example local repository with non-persistent storage.
     * Easy enough to create a version for database.
     */

    public class MusicRepositoryLocal : IMusicRepository
    {
        private List<MusicContract> musicContracts = new List<MusicContract>();
        private List<DistributionPartnerContract> distributionPartnerContracts = new List<DistributionPartnerContract>();

        public void AddMusicContract(MusicContract musicContract)
        {
            this.musicContracts.Add(musicContract);
        }

        public void AddDistributionPartnerContracts(DistributionPartnerContract distributionPartnerContracts)
        {
            this.distributionPartnerContracts.Add(distributionPartnerContracts);
        }

        public IEnumerable<MusicContract> GetMusicContracts()
        {
            return musicContracts;
        }

        public IEnumerable<DistributionPartnerContract> GetDistributionPartnerContracts()
        {
            return distributionPartnerContracts;
        }
    }
}
