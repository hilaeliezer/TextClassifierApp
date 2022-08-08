using System;
using System.Collections.Generic;
using System.Text;

namespace TextClassifierApp
{
   public interface ITextClassifier
    {

        ClassifierResponse Classify(string [] args);
    }
}
