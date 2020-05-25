using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Utilities
{
    public class FileUtilities
    {
        //download file from internet via url
        public byte[] DownloadFile(string url)
        {
            return new System.Net.WebClient().DownloadData(url);
        }

        //compare 2 files' hashes, return true if equal
        public bool FilesAreIdentical(byte[] file1, byte[] file2)
        {
            //TODO: there are better ways
            return file1.GetHashCode() == file2.GetHashCode();
        }
    }
}
