using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TextClassifierApp
{
    public class Json2Rules : IJson2Rules
    {
        public ClassificationRules GetRules(string path)
        {
            try
            {

                var ext = System.IO.Path.GetExtension(path).Replace(".", "");
                if (ext != "json")
                {
                    //log error: invalid input file type
                    return null;
                }
                using (StreamReader json = new StreamReader(path))
                {
                    
                    JsonSerializer d = new JsonSerializer();
                    ClassificationRules c = (ClassificationRules)d.Deserialize(json, typeof(ClassificationRules));

                    return c;


                }
            }
            catch (Exception ex)
            {
                //log
                throw ex;
            }
        }
    }
}

