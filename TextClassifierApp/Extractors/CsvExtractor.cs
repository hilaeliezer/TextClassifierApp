using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TextClassifierApp
{
    public class CsvExtractor : IExtractor
    {
        public string FileExtension { get; set; }
        private IClassifier _classifier;
        public CsvExtractor(IClassifier classifier)
        {
            FileExtension = "csv";
            _classifier = classifier;

        }
        public bool IsPathExtension(string path)
        {
            try
            {
                var ext = System.IO.Path.GetExtension(path).Replace(".", "");
                return (FileExtension.ToLower() == ext.ToLower());
            }
            catch (Exception ex)
            {

                //log
                throw ex;
            }
        }
        public HashSet<string> Extract(string path, HashSet<string> data2Ignore = null)
        {
            try
            {
                HashSet<string> retData = new HashSet<string>();
                HashSet<string> existsData = (data2Ignore == null) ? new HashSet<string>() : data2Ignore;

                using (StreamReader file = new StreamReader(path))
                {
                    string ln;

                    while ((ln = file.ReadLine()) != null)
                    {

                        retData = _classifier.MatchRules((ln.Split(',')).ToList(), existsData);
                        existsData.UnionWith(retData);

                    }
                    file.Close();
                }


                return existsData;
            }
            catch (Exception ex)
            {
                //log
                throw ex;
            }
        }
    }

}
