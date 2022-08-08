using System;
using System.Collections.Generic;
using System.Text;

namespace TextClassifierApp
{
   public interface IPathClassifier
    {
        HashSet<string> GetClassifyPath(string path);
    }
}
