using System;
using System.Collections.Generic;
using System.Text;

namespace TextClassifierApp
{
    public interface IJson2Rules
    {
       ClassificationRules GetRules(string path);
    }
}
