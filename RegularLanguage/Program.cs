using System;
using System.Collections.Generic;
using System.IO;

namespace RegularLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
            string StrText = "";
            try
            {
                StreamReader sr = new StreamReader("input.txt");
                StrText = sr.ReadToEnd().ToString();
                sr.Close();
            }
            catch(IOException e)
            {
                Console.WriteLine(e.Message);
            }
            string[] StrArray = StrText.Split('\n');
            NFA nFA = new NFA(int.Parse(StrArray[0]));
            nFA.AddAlpha(StrArray[1]);
            for (int i = 2; i < StrArray.Length; i++)
            {
                nFA.SetRule(StrArray[i]);
            }
            nFA.makeRules2();
            LinkedList<Rule> DFARULE = nFA.MakeDFA();
            foreach (var r in DFARULE)
            {
                Console.WriteLine(r.Print());
            }
           
        }
    }
}
