using System;
using System.Collections.Generic;
using System.Text;

namespace TextClassifierApp
{
  public  class ClassifierResponse
    {
        public bool IsSuccess { get; set; }
        public string ErrorMsg { get; set; }
        public HashSet<string> Data { get; set; }
        public ClassifierResponse()
        {
            IsSuccess = true;
        }
      
    }
}
