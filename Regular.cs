using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegularExpressions
{
    public abstract class Regular
    {
        //string s2 = String.Format("...{0}...", String.Join("|", keyWordsNotRef));

        static List<string> keyWordsNotRef = new List<string>() { "int", "float", "double", "byte", };
        static List<string> keyWordsNotRefSys = new List<string>() { "Int16", "Int32", "Int64", "Decimal", "Double"};
        static List<string> keyWordsRef = new List<string>() { "string", "object" };
        static List<string> keyWordsRefSys = new List<string>() { "String", "Object" };
        
        static string System { get; } = @"(?<System>(?<=\A|\;)\s*System\s*\.)|";
        static string varTypeWithSystem { get; } = $@"(?<varTypeWithSystem>(?<=\.){String.Join("|", join(keyWordsRefSys))}|{String.Join("|", join(keyWordsNotRefSys))})|";
        static string varTypeWithoutSystem { get; } = $@"(?<varTypeWithoutSystem>(?<=\A|\;){String.Join("|", join(keyWordsRef))}|{String.Join("|", join(keyWordsRefSys))}|{String.Join("|", join(keyWordsNotRefSys))}|{String.Join("|", join(keyWordsNotRef))})|";
        static string massOpen = @"(?<massOpen>(?<=[a-z0-9]|\]|\?)\s*(?:\[))|";
        static string commaMass = @"(?<commaMass>(?<=\[|\,)\s*(?:\,))|";
        static string massClose = @"(?<massClose>(?<=\[|\[\,|\,)\s*(?:\]))|";
        static string question = $@"(?<question>(?<={String.Join("|", keyWordsNotRef)}|{String.Join("|", keyWordsNotRefSys)})\s*(?:\?))|";
        static string varName = @"(?<varName>(?<=\?|\]|\,|[a-zA-Z0-9_])(\s*[\@]{0,1}[a-zA-Z_][a-zA-Z0-9_]*|[a-zA-Z_][a-zA-Z0-9_]*)(\s*,\s*[\@]{0,1}[a-zA-Z_][a-zA-Z0-9_]*|[a-zA-Z_][a-zA-Z0-9_]*)+)|";
        static string semicolon = @"(?<semicolon>(?<=[a-zA-Z0-9_])\s*(?:;))";

        static List<string> join(List<string> list)
        {
            List<string> joinList = list.ToList();

            for(int i = 0; i < joinList.Count; i++)
            {
                joinList[i] = String.Concat(@"\s*", joinList[i]);
            }
            return joinList;
        }

        public static string regular = "(" + System + varTypeWithSystem + varTypeWithoutSystem + massOpen +
            commaMass + massClose + question + varName + semicolon + ")*";

        public static bool CheckEnd(string m)
        {
            if (m.EndsWith(";"))
                return true;
            else return false;
        }

        public static List<List<string>> keyWords = new List<List<string>>() { keyWordsNotRef.ConvertAll(w => $" {w} "), keyWordsRef.ConvertAll(w => $" {w} ") };
    }
}
