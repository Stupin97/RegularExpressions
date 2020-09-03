using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegularExpressions
{
    class Regular_v2
    {
        int _1;
        int __;

        public static List<string> keyWords1 = new List<string>() { "int", "float", "double", "byte", };
        string s = $"...({String.Join("|", keyWords1)})...";
        string s2 = String.Format("...{0}...", String.Join("|", keyWords1));
        public static List<string> s3 = keyWords1.ConvertAll(w => $" {w} ");

        public static string VarType { get; } = @"((\s)*(string|(int(((\s)*\\?)|(\s)*))|(System(\s)*\.(\s)*(String|(Int32(((\s)*\?)|(\s)*)))))(\s)*(\[((\s)*|(\,)*)*\](\s)*)*)";
        public static string VarName { get; } = "((@(\\s)*)|((\\s)*))(((_)*([a-z]+)(([a-zA-Z0-9])(_)*)*)|(([a-z]+)(([a-zA-Z0-9])(_)*)*))((\\s)*,(\\s)*(((@(\\s)*)|((\\s)*))(((_)*([a-z]+)(([a-zA-Z0-9])(_)*)*)|(([a-z]+)(([a-zA-Z0-9])(_)*)*)))+)*";
        public static string Semicolon { get; } = "(\\s)*(\\;)";

        public static bool MarkerName { get; set; }

        public static List<string> keyWords = new List<string>() { " int ", " string ", " float ", " double ", " byte ", " object " };
    }
}
