using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupidPictureHashComparison.WPF.Common
{
    public  class ImageHash
    {
        public string Path { get; set; }
        public string Hash { get; set; }

        public ImageHash(string path, string hash)
        {
            Path = path;
            Hash = hash;
        }
    }
}
