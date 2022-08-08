using System;
using System.Collections.Generic;
using System.Text;

namespace TextClassifierApp
{
     public interface IClassifier   
    {
        ClassificationRules ClassificationRules { get; set; }
        HashSet<string> MatchRules(List<string> data2classify, HashSet<string> data2Ignore=null);
    }
}
