using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TextClassifierApp
{
    public class PathClassifier : IPathClassifier
    {
        private IEnumerable<IExtractor> _extractors;
        public PathClassifier(IEnumerable<IExtractor> extractors)
        {
            _extractors = extractors;
        }

        public HashSet<string> GetClassifyPath(string path)
        {
            try
            {


                FileAttributes attr = File.GetAttributes(path);

                if (attr.HasFlag(FileAttributes.Directory))
                {
                    return GetClassifyDirectory(path);

                }
                else
                {
                    return GetClassifyFile(path);
                }
            }
            catch (Exception ex)
            {
                //log
                throw ex;
            }
        }


        private HashSet<string> GetClassifyDirectory(string path, HashSet<string> data2Ignore = null)
        {
            try
            {
                HashSet<string> retData = new HashSet<string>();
                HashSet<string> existsData = (data2Ignore == null) ? new HashSet<string>() : data2Ignore;
                var files = Directory.GetFiles(path);
                var directories = Directory.GetDirectories(path);
                foreach (var d in directories)
                {
                    retData = GetClassifyDirectory(d, existsData);
                    if (retData != null)
                    {
                        existsData.UnionWith(retData);
                    }
                }
                foreach (var f in files)
                {
                    retData = GetClassifyFile(f, existsData);
                    if (retData != null)
                    {
                        existsData.UnionWith(retData);
                    }

                }
                return existsData;
            }
            catch (Exception ex)
            {
                //log
                throw ex;
            }
        }



        private HashSet<string> GetClassifyFile(string path, HashSet<string> data2Ignore = null)
        {
            try
            {
                IExtractor extractor = _extractors.Where(_extractor => _extractor.IsPathExtension(path)).FirstOrDefault();
                if (extractor != null)
                {
                    return extractor.Extract(path, data2Ignore);
                }
                return null;
            }
            catch (Exception ex)
            {
                //log missing file extention handeling
                throw ex;
            }

        }

    }
}
