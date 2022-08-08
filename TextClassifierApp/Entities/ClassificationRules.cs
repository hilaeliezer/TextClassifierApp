using System;
using System.Collections.Generic;
using System.Text;

namespace TextClassifierApp
{
  public  class ClassificationRules
    {
        public List<Rule> classification_rules { get; set; }

    }
    public class Rule
    {
        public string domain { get; set; }
        public List<string> indicators { get; set; }

    }
}
