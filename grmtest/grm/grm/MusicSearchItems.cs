using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grm
{
    /* parse search items for use in our searching */
    public class MusicSearchItems
    {
        private const char SEARCH_DELIMITTER = ' ';
        private String partner;
        private DateTime effectiveDate;

        public DateTime EffectiveDate { get => effectiveDate; set => effectiveDate = value; }
        public string Partner { get => partner; set => partner = value; }

        public MusicSearchItems(String searchString)
        {
            var cols = searchString.Split(new char[] { SEARCH_DELIMITTER }, 2);
            partner = cols[0];
            EffectiveDate = MusicRepositoryFileLoader.DateTimeParser(cols[1]);
        }
    }
}
