using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegularExpressions
{
    public class WorkingWithRegularExpressions
    {
        static Regex myRegex = new Regex(Regular.regular);

        List<string> listName;
        string str;
        int line;
        Match m;

        public string Str { get { return str; } }

        public WorkingWithRegularExpressions(string str)
        {
            listName = new List<string>() {"" };
            this.str = str;
            line = 1;//
        }

        public void Method()
        {
            m = Regex.Match(str, Regular.regular);
            Console.WriteLine("!!!!!" + m.Value.Length);
            if (m.Success == true)
            {
                Parallel.ForEach(m.Value, symbol => { if (symbol.Equals('\n')) line++; });
                Console.WriteLine("str: \"{0}\" \nMatch: \"{1}\"", str, m);
                foreach (Group grp in m.Groups)
                {
                    //if (grp.Value.Contains('\n'))
                    //{
                    //    line++;
                    //}
                    if (grp == m.Groups["varName"])//if(grp == m.Groups["11"] )
                    {
                        DuplicateNames(m.Groups["varName"]);
                    }
                }
                
                if(str.Length == m.Value.Length && Regular.CheckEnd(m.Value))
                    Exit("All right!!!");
                else
                    Exit($"Ошибка в позиции {m.Value.Length - m.Value.LastIndexOf('\n')} !  \n Линия {line}");
                    //Exit($"Ошибка в позиции {m.Value.Length} !  \n Линия {line}");
            }
            else
                Exit("Match failed!");
        }

        void DuplicateNames(Group group)
        {
            string pattern = @"((\s)*,(\s)*)";
            foreach (Capture cap in group.Captures)
            {
                Console.WriteLine("     cap:  {0}, Index :{1}", cap.Value, cap.Index);
                if (cap.Value.Contains(","))
                {
                    string[] result = Regex.Split(cap.Value, pattern, RegexOptions.IgnoreCase);
                    var res = (from s in result
                               where !s.Contains(" ")
                               select s).ToList().ConvertAll(w => $" {w} ");

                    foreach(var name in res)
                    {
                        CheckAndAdd(name, cap);
                    }
                }
                else
                    CheckAndAdd(cap.Value.Insert(cap.Length," "), cap);
            }
        }

        void CheckAndAdd(string name, Capture cap)
        {
            int lenght = m.Value.IndexOf('\n', cap.Index) - m.Value.LastIndexOf('\n', cap.Index); 
            int lenght_name = lenght - cap.Value.Length;
            int error = (lenght - (lenght - lenght_name)) + cap.Value.LastIndexOf(name.Replace(" ", ""));
            string d = m.Value.Remove(cap.Index);
            foreach (var keyWord in Regular.keyWords)
                if (name.Contains(keyWord[0]) || name.Contains(keyWord[1]))
                {
                    line = (m.Value.Remove(cap.Index).Length - m.Value.Remove(cap.Index).Replace("\n", "").Length);
                    Exit($"Переменная с именем {name} не может являться ключевым словом, ошибка в позиции {error} \n Линия {line+1}");
                    //Exit($"Переменная с именем {name} не может являться ключевым словом, ошибка в позиции {cap.Value.LastIndexOf(name.Replace(" ", "")) + cap.Index} \n Линия {line}");
                }

            if (name.Contains('@'))
                name = name.Replace("@", "");

            if (!listName.Contains(name))
                listName.Add(name);
            else
            {
                line = (m.Value.Remove(cap.Index).Length - m.Value.Remove(cap.Index).Replace("\n", "").Length);
                Exit($"Переменная с именем {name} не может являться ключевым словом, ошибка в позиции {(lenght - (lenght - lenght_name)) + cap.Value.LastIndexOf(name.Replace(" ", ""))} \n Линия {line+1}");
            }
        }

        private void Exit(string _out)
        {
            Console.WriteLine(_out);
            Console.Read();
            Environment.Exit(0);
        }
    }
}
