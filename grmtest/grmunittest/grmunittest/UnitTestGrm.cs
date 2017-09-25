using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using grm;

namespace grmunittest
{

    /* unit tests as specified in brief.
   / * Full Unit Tests here to show complete end to end functionality. In actuall TDD we would use more mocking of file loading etc. and give more detailed class testing.
    */

    [TestClass]
    public class UnitTestGrm
    {
        IMusicRepository repos;

        [TestInitialize]
        public void Initialize()
        {
            repos = new MusicRepositoryLocal();
            IMusicRepositoryFileLoader reposFileLoader = new MusicRepositoryFileLoader(repos);
            reposFileLoader.LoadFlatFile("c:\\grmtest\\loadFiles\\DistributionMasterContracts.txt", MusicFileType.DistributionPartnerContracts);
            reposFileLoader.LoadFlatFile("c:\\grmtest\\loadFiles\\MusicContracts.txt", MusicFileType.MusicContracts);

        }


        [TestMethod]
          public void Test1()
          {
              MusicRepositorySearcher search = new MusicRepositorySearcher("ITunes 1st March 2012", repos);
              String results = search.GetSearchResults();
              String correctResults = "Artist|Title|Usage|StartDate|EndDate\n" +
                                "Monkey Claw|Black Mountain|digital download|1st Feb 2012|\n" +
                                "Monkey Claw|Motor Mouth|digital download|1st Mar 2011|\n" +
                                "Tinie Tempah|Frisky (Live from SoHo)|digital download|1st Feb 2012|\n" +
                                "Tinie Tempah|Miami 2 Ibiza|digital download|1st Feb 2012|";
              Assert.AreEqual(results, correctResults );

          }
        [TestMethod]
        public void Test2()
        {
            MusicRepositorySearcher search = new MusicRepositorySearcher("YouTube 1st April 2012", repos);
            String results = search.GetSearchResults();
            String correctResults = "Artist|Title|Usage|StartDate|EndDate\n" +
                                  "Monkey Claw|Motor Mouth|streaming|1st Mar 2011|\n" +
                                  "Tinie Tempah|Frisky (Live from SoHo)|streaming|1st Feb 2012|";
            Assert.AreEqual(results, correctResults);


        }
        [TestMethod]
        public void Test3()
        {
            MusicRepositorySearcher search = new MusicRepositorySearcher("YouTube 27th Dec 2012", repos);
            String results = search.GetSearchResults();
            String correctResults = "Artist|Title|Usage|StartDate|EndDate\n" +
                                "Monkey Claw|Christmas Special|streaming|25th Dec 2012|31st Dec 2012\n" +
                                "Monkey Claw|Iron Horse|streaming|1st Jun 2012|\n" +
                                "Monkey Claw|Motor Mouth|streaming|1st Mar 2011|\n" +
                                "Tinie Tempah|Frisky (Live from SoHo)|streaming|1st Feb 2012|";
            Assert.AreEqual(results, correctResults);

        }
   
    }

      
}
