using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TextClassifierApp
{
    public class TextClassifier : ITextClassifier
    {
        private IPathClassifier _pathClassifier;
        private IJson2Rules _json2Rules;
        private IClassifier _classifier;
        private readonly ILogger<TextClassifier> _logger;

        public TextClassifier(IPathClassifier pathClassifier, IJson2Rules json2Rules, IClassifier classifier, ILoggerFactory loggerFactory)
        {
            _pathClassifier = pathClassifier;
            _json2Rules = json2Rules;
            _classifier = classifier;
            _logger = loggerFactory.CreateLogger<TextClassifier>();
        }

        public ClassifierResponse Classify(string[] path)
        {
            try
            {
                string filePath = path[0];
                string jsonPath = path[1];
              
                //validate input path
                if ((!Directory.Exists(filePath) && !File.Exists(filePath)) || !File.Exists(jsonPath))
                {
                    //log
                    return new ClassifierResponse() { IsSuccess = false, ErrorMsg = "Invalid Arguments" };
                }
                
                //Get Rules from json 
                var rules = _json2Rules.GetRules(jsonPath);
                if (rules == null)
                {
                    //log
                    return new ClassifierResponse() { IsSuccess = false, ErrorMsg = "Error json File" };
                }
                
                //Set Rules to Classifier service 
                _classifier.ClassificationRules = rules;

                //Classify by path using pathClassifier service that returns the classified domains
                var data = _pathClassifier.GetClassifyPath(filePath);
                if (data == null)
                {
                    return new ClassifierResponse() { IsSuccess = false, ErrorMsg = "Error File" };
                }

                return new ClassifierResponse() { Data = data };

            }
            catch (Exception ex)
            {
                //add log
                throw ex;

            }


        }


    }
}
