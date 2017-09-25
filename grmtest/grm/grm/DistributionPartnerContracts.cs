using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grm
{
    public class DistributionPartnerContract
    {

        private String partner;
        private String usage;


        public string Partner { get => partner; set => partner = value; }
        public string Usage { get => usage; set => usage = value; }

        public DistributionPartnerContract()
        {
            
        }

        public DistributionPartnerContract(string partner, string usage)
        {
            Partner = partner;
            Usage = usage;
        }
    }
}
