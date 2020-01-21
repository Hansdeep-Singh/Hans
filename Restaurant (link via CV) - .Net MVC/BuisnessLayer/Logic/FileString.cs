﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Logic
{
    public class FileString
    {
        public string FileByteToString(byte[] b)
        {
            return "data:image/png;base64," + Convert.ToBase64String(b);
        }
        public string stringforbyte = "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";

        public string thumb = "data:image/png;base64,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";

        public string isnah(string value)
        {
            if (value == "N/A") { return null; }
            else return value;
        }
    }
}
