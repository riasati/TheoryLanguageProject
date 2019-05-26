using System;
using System.Collections.Generic;
using System.Text;

namespace RegularLanguage
{
    public class NFA
    {
        int NumberOfState;
        string Alphabet;
        LinkedList<string> Rules = new LinkedList<string>();
        LinkedList<string> States;
        LinkedList<Rule> Rules2 = new LinkedList<Rule>();
        string FirstState;
        LinkedList<string> FinalState = new LinkedList<string>();
        public NFA(int number)
        {
            NumberOfState = number;
            States = new LinkedList<string>();
        }
        public void AddAlpha(string alpha)
        {
            Alphabet = alpha;
        }
        public void SetRule(string rule)
        {
            if (rule[0].Equals('-'))
            {
                FirstState = rule.Substring(2, 2);
                rule = rule.Substring(2, rule.Length - 2);
            }
            if (ruleHaveFinal(rule))
            {
                rule = changeRule(rule);
            }
            Rules.AddLast(rule);

            string[] sepState = rule.Split(',');
            for (int i = 0; i < sepState.Length; i++)
            {
                if (i == 1)
                {
                    continue;
                }
                if (!sameState(sepState[i]))
                {
                    States.AddLast(sepState[i]);
                }
            }
        }
        protected string changeRule(string rule)
        {
            if (rule[0].Equals('*'))
            {
                if (rule[3].Equals(','))
                {
                    string finalState = rule.Substring(1, 2);
                    FinalState.AddLast(finalState);
                }
                else
                {
                    string finalState = rule.Substring(1, 3);
                    FinalState.AddLast(finalState);
                }
                
                string str = rule.Substring(1, rule.Length - 1);
                return str;
            }
            for (int i = 0; i < rule.Length; i++)
            {
                if (!rule[i].Equals('*'))
                {
                    continue;
                }

                string finalState = rule.Substring(i + 1, 2);
                FinalState.AddLast(finalState);
               

                string rule1 = rule.Substring(0, i);
                string rule2 = rule.Substring(i + 1, rule.Length - i - 1);
                string str = rule1 + rule2;
                return str;
            }
            return null;
        }
        protected bool ruleHaveFinal(string rule)
        {
            for (int i = 0; i < rule.Length; i++)
            {
                if (rule[i].Equals('*'))
                {
                    return true;
                }
            }
            return false;
        }
        protected bool sameState(string state)
        {
            foreach (var i in States)
            {
                if (i == state)
                {
                    return true;
                }
            }
            return false;
        }
        public void makeRules2()
        {
            foreach (var rule in Rules)
            {
                Rule newRule = new Rule(rule, Alphabet);
                Rules2.AddLast(newRule);
            }
        }

        public Rule MakeDFArule(Rule NFArule)
        {
            LinkedList<Rule> neededRule = new LinkedList<Rule>();
            foreach (Rule rule1 in Rules2)
            {
                if (rule1.avali == NFArule.avali & rule1.alphabet == NFArule.alphabet)
                {
                    neededRule.AddLast(rule1);
                }
            }
            LinkedList<Rule> neededRule2 = new LinkedList<Rule>();
            foreach (Rule rule1 in neededRule)
            {
                foreach (Rule rule2 in Rules2)
                {
                    if (rule1.dovomi == rule2.avali && rule2.alphabet=="_")
                    {
                        neededRule2.AddLast(rule2);
                    }
                }
            }
            LinkedList<Rule> neededRule3 = new LinkedList<Rule>();
            foreach (Rule rule1 in neededRule2)
            {
                foreach (Rule rule2 in Rules2)
                {
                    if (rule1.dovomi == rule2.avali && rule2.alphabet == "_")
                    {
                        neededRule3.AddLast(rule2);
                    }
                }
            }

            foreach (Rule rule1 in neededRule2)
            {
                neededRule.AddLast(rule1);
            }
            foreach (Rule rule1 in neededRule3)
            {
                neededRule.AddLast(rule1);
            }

            
            string avali = NFArule.avali.Substring(1, NFArule.avali.Length-2);
            string alphabet = NFArule.alphabet;
            string dovomi = "";
            foreach (Rule rule1 in neededRule)
            {
                dovomi += rule1.dovomi.Substring(1, rule1.dovomi.Length-2) + ",";
            }
            dovomi = dovomi.Substring(0, dovomi.Length - 1);
            Rule newRule = new Rule(avali + "," + alphabet + "," + dovomi, Alphabet);
            return newRule;

        }
        public LinkedList<Rule> MakeDFA()
        {
            LinkedList<Rule> returnList = new LinkedList<Rule>();
            foreach (var r in this.Rules2)
            {
                Rule rule = MakeDFArule(r);
                returnList.AddLast(rule);
            }
            return returnList;
        }
    }
}
