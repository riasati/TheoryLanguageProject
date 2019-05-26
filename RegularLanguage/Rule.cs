using System;
using System.Collections.Generic;
using System.Text;

namespace RegularLanguage
{
    public class Rule
    {
        public string avali;
        public string dovomi;
        public string alphabet;
        public Rule(string rule, string alpha)
        {
            string[] seprate = rule.Split(',');
            string[] sepAlpha = alpha.Split(',');
   
            int k = -1;
            for (int i = 0; i < seprate.Length; i++)
            {
                for (int j = 0; j < sepAlpha.Length; j++)
                {
                    if (seprate[i].Equals(sepAlpha[j]))
                    {
                        k = i;
                        alphabet = seprate[i];
                        break;
                    }
                    else if (seprate[i].Equals("_"))
                    {
                        k = i;
                        alphabet = "_";
                        break;
                    }
                } 
            }
            avali = "(";
            for (int i = 0; i < k; i++)
            {
                avali += seprate[i];
            }
            avali += ")";

            dovomi = "(";
            for (int i = k+1; i < seprate.Length; i++)
            {
                dovomi += seprate[i];
            }
            dovomi += ")";
        }
        public string Print()
        {
            return this.avali + ',' + this.alphabet + ',' + this.dovomi;
        }
    }
}
