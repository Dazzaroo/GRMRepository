using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grm
{
    /* load file into music repository */
    public interface IMusicRepositoryFileLoader
    {
        void LoadFlatFile(String fileLocation, MusicFileType musicFileType);
    }
}
