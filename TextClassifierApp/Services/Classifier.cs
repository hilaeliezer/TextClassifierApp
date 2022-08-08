using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextClassifierApp
{
    public class Classifier : IClassifier
    {
        public ClassificationRules ClassificationRules { get; set; }

        public HashSet<string> MatchRules(List<string> data2classify, HashSet<string> data2Ignore = null)
        {
            try
            {
                HashSet<string> rulesExists = new HashSet<string>();
                List<Rule> rules2Find = ClassificationRules.classification_rules;
                rules2Find.RemoveAll(_rule => data2Ignore.Contains(_rule.domain));

                foreach (var item in data2classify)
                {
                    rules2Find.RemoveAll(x => rulesExists.Contains(x.domain));

                    foreach (var rule in rules2Find)
                    {
                        if (IsMatch(item, rule))
                        {
                            rulesExists.Add(rule.domain);
                        }

                    }
                }
                return rulesExists;
            }
            catch (Exception ex)
            {
                //log
                throw ex;
            }
        }

        private bool IsMatch(string d, Rule r)
        {
            foreach (var i in r.indicators)
            {
                if ((d.ToLower()).Contains(i.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
