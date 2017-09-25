using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grm
{
    /* basic Repository to hold music contracts and distribution partner contracts */
    public interface IMusicRepository
    {
        void AddMusicContract(MusicContract musicContract);
        void AddDistributionPartnerContracts(DistributionPartnerContract distributionPartnerContracts);
        IEnumerable<MusicContract> GetMusicContracts();
        IEnumerable<DistributionPartnerContract> GetDistributionPartnerContracts();
    }
}
