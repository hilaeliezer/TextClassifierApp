using System;
using System.Collections.Generic;
using System.Text;

namespace TextClassifierApp
{
   public interface IExtractor
    {
         string FileExtension { get; set; }
         bool IsPathExtension(string path);
         HashSet<string> Extract(string path,HashSet<string> data2Ignor=null);

    }
}
