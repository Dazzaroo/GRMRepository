using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grm
{
    /* dedicated searcher for our Music Repository.
     * Parse search items and find results via Linq
     */

    public class MusicRepositorySearcher
    {
        private const String RESULTS_DELIMITTER = "|";

        private IMusicRepository musicRepository;
        private MusicSearchItems musicSearchItems;

        private String searchResults = "";

        public string SearchResults { get => searchResults; set => searchResults = value; }

        public MusicRepositorySearcher(String searchString, IMusicRepository musicRepository)
        {
            musicSearchItems = new MusicSearchItems(searchString);
            this.musicRepository = musicRepository;
            DoSearch();
        }

        /* get results header. Ideally have a MusicRepositorySearchResultsFormatter class and deal with there. Avoid the hard coding of "Artist" etc via some sort of reflection on the MusicContract model.
         * Simplified for time. */

        private String GetSearchHeader()
        {
            return "Artist" + RESULTS_DELIMITTER + "Title" + RESULTS_DELIMITTER + "Usage" + RESULTS_DELIMITTER + "StartDate" + RESULTS_DELIMITTER + "EndDate"; 
        }

        /* Deal specifically with End Date which is Max value for nulls */

        private String GetDateNullMax(DateTime date)
        {
            String newDate;
            if (date == DateTime.MaxValue)
                newDate = "";
            else
                newDate = MusicRepositoryFileLoader.GetDateTimeFormatted(date);
            return newDate;

        }

        /* Do search with Linq */
        private void DoSearch()
        {
            IEnumerable<MusicContract> musicContracts = musicRepository.GetMusicContracts();
            IEnumerable<DistributionPartnerContract> distributionPartnerContracts = musicRepository.GetDistributionPartnerContracts();

            var results = from mc in musicContracts
                          join dpc in distributionPartnerContracts on
                               mc.Usage equals dpc.Usage
                          where musicSearchItems.EffectiveDate >= mc.StartDate
                          where musicSearchItems.EffectiveDate <= mc.EndDate
                          where musicSearchItems.Partner == dpc.Partner
                          orderby mc.Artist, mc.Title
                          select mc;

            searchResults = GetSearchHeader();

            /* return results. ideally have formatter class to seperate concerns */
            foreach (var result in results)
            {
               
                if (searchResults != "")
                    searchResults = searchResults + "\n";
             
                searchResults += result.Artist + RESULTS_DELIMITTER + result.Title + RESULTS_DELIMITTER + result.Usage + RESULTS_DELIMITTER + MusicRepositoryFileLoader.GetDateTimeFormatted(result.StartDate) +
                                               RESULTS_DELIMITTER + GetDateNullMax(result.EndDate);
            }

        }

        public String GetSearchResults()
        {
            return searchResults;
        }
    }
}
