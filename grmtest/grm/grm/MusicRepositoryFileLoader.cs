using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace grm
{
    /* handle file I/O to repostitory */
    public class MusicRepositoryFileLoader : IMusicRepositoryFileLoader
    {

        private const char COLUMN_DELIMITTER = '|';

        /* two possible formats in file. ideally should only be one */
        private const String GRM_DATE_FORMAT1 = "d MMM yyyy";
        private const String GRM_DATE_FORMAT2 = "d MMMM yyyy";

        private IMusicRepository musicRepository;

        public void LoadFlatFile(string fileLocation, MusicFileType musicFileType)
        {
            int lineCount = 0;
            if (!File.Exists(fileLocation))
                throw new FileNotFoundException("Invalid file location " + fileLocation);
            string[] fileLines = File.ReadAllLines(fileLocation);

            foreach(string line in fileLines)
            {
                /* skip header lines and add line to repository */
                if (lineCount >=2)
                    AddItemRepository(line, musicFileType);
                lineCount++;
            }

        }

        public MusicRepositoryFileLoader()
        {
        }

        public MusicRepositoryFileLoader(string filelocation, MusicFileType musicFileType)
        {
            LoadFlatFile(filelocation, musicFileType);
        }

        /* parse a line and add to relevant music store item */
        public void AddItemRepository(String line, MusicFileType musicFileType)
        {
            switch(musicFileType)
            {
                case MusicFileType.MusicContracts:
                    List<MusicContract> musicContracts = LineParseCreateMusicContracts(line);
                    AddMusicContractsRepository(musicContracts);
                    break;
                case MusicFileType.DistributionPartnerContracts:
                    DistributionPartnerContract distributionPartnerContract = LineParseCreateDistributionPartnerContracts(line);
                    musicRepository.AddDistributionPartnerContracts(distributionPartnerContract);
                    break;
                default:
                    throw new InvalidDataException("Unknown Music Repository item " + musicFileType);
            }
        }

        public void AddMusicContractsRepository(List<MusicContract> musicContracts)
        {
            foreach (var musicContract in musicContracts)
            {
                musicRepository.AddMusicContract(musicContract);
            }
        }

        /* parse a file line and create a DistributionPartnerContract */
        public DistributionPartnerContract LineParseCreateDistributionPartnerContracts(String line)
        {
            DistributionPartnerContract distributionPartnerContracts;
            var cols = line.Split(COLUMN_DELIMITTER);
            if (cols.Length >= 2)
            {
                distributionPartnerContracts = new DistributionPartnerContract(cols[0], cols[1]);
                return distributionPartnerContracts;
            } else
            {
                throw new InvalidDataException("Unable to Parse Distribution Parner Contracts File line: " + line);
            }
        }

        /* parse a file line and create a list of MusicContract.
         * multiple usages are reason for multiple records
         */

        public List<MusicContract> LineParseCreateMusicContracts(String line)
        {
            var cols = line.Split(COLUMN_DELIMITTER);

            if (cols.Length >=5)
            {
            
                DateTime startDateTime;
             
                startDateTime = DateTimeParser(cols[3]);
                
               DateTime endDateTime;
               endDateTime = cols[4] != "" ? DateTimeParser(cols[4]) : DateTime.MaxValue; ;
               MusicContractsBuilder musicContractsBuilder = new MusicContractsBuilder(cols[0], cols[1], cols[2], startDateTime, endDateTime);

                return musicContractsBuilder.GetMusicContracts();
            } else
            {
                throw new InvalidDataException("Unable to Parse Music Contracts File line: " + line);
            }
           
        }

        public MusicRepositoryFileLoader(IMusicRepository musicRepository)
        {
            this.musicRepository = musicRepository;
        }
        /* the following two methods really should be in another class or two for CRM date formatting */
        /* from a given date in CRM preferred format(s) find the DateTime */

        public static DateTime DateTimeParser(String lineDate)
        {
            lineDate = lineDate.Replace("nd", "");
            lineDate = lineDate.Replace("rd", "");
            lineDate = lineDate.Replace("st", "");
            lineDate = lineDate.Replace("th", "");

            DateTime parsedDateTime;
            /* try two different formats */
            try
            {
                parsedDateTime = DateTime.ParseExact(lineDate, GRM_DATE_FORMAT1, CultureInfo.InvariantCulture);
            }
            catch (FormatException ex)
            {
                parsedDateTime = DateTime.ParseExact(lineDate, GRM_DATE_FORMAT2, CultureInfo.InvariantCulture);
            }
            return parsedDateTime;
        }

        /* for DateTime find the CRM format */
        public static String GetDateTimeFormatted(DateTime dateTime)
        {
            String formattedDateTime;
            formattedDateTime = IntegerExtensions.AsOrdinal(dateTime.Day) + " " +
                                dateTime.ToString("MMM yyyy");
            return formattedDateTime;
        }


    }
}
