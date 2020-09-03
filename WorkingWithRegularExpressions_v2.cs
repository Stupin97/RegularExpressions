using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace RegularExpressions
{
    class WorkingWithRegularExpressions_v2
    {
        //Тип
        static Regex varType = new Regex(@Regular_v2.VarType);
        //Имя переменной
        static Regex varName = new Regex(Regular_v2.VarName);
        //Точка с запятой
        static Regex semicolon = new Regex(Regular_v2.Semicolon);

        List<Regex> regices = new List<Regex>() { varType, varName, semicolon };

        List<string> listName;
        string str;
        int index, SelectRegex, line;

        public string Str { get { return str; } }

        public WorkingWithRegularExpressions_v2(string str)
        {
            listName = new List<string>() { "" };
            this.str = str;
            index = 0;
            SelectRegex = 0;
            line = 0;//
        }

        Regex GetNextRegex(int i)
        {
            if (regices.Count <= i)
                SelectRegex = 0;
            return regices[SelectRegex];
        }

        public void Method()
        {
            while (index != str.Length)
            {
                Match m = regices[SelectRegex].Match(str, index);
                while (m.Success)
                {
                    if (m.Index == index)
                    {
                        if (SelectRegex == 2) //Если используется регулярное выражение разбора имен
                            DuplicateNames(m.Index, m.Value.Length);
                        index += m.Value.Length;
                    }
                    else Exit($"Ошибка в позиции :{m.Value.Length + index}");
                    break;
                }
                GetNextRegex(++SelectRegex);
            }
        }

        void DuplicateNames(int startIndex, int count)
        {
            string strName = str.Substring(startIndex, count);
            string pattern = "((\\s)*,(\\s)*)";
            string[] result = Regex.Split(strName, pattern,
                                          RegexOptions.IgnoreCase);
            var res = (from s in result
                       where !s.Contains(" ")
                       select s).ToArray();

            for (int ctr = 0; ctr < res.Length; ctr++)
            {
                //Console.WriteLine("'{0}'", res[ctr]);
                foreach (var keyWord in Regular_v2.keyWords)
                    if (res[ctr].Contains(keyWord))
                        Exit($"Переменная с именем {res[ctr]} не может являться ключевым словом, ошибка в позиции {strName.LastIndexOf(res[ctr]) + index}");

                if (res[ctr].Contains('@'))
                    res[ctr] = res[ctr].Remove(0, 1);

                if (!listName.Contains(res[ctr]))
                    listName.Add(res[ctr]);
                else Exit($"Переменная с именем {res[ctr]} уже существует, ошибка в позиции {strName.LastIndexOf(res[ctr]) + index}");
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
