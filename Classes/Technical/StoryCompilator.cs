using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKA_Novel.Classes.Technical
{
    internal class StoryCompilator
    {
        public static int LineOfStory { get; set; } = -1;
        public static string[] CurrentStory { get; set; }

        public static string GetNextLine()
        {
            if ((LineOfStory + 1) < CurrentStory.Count())
                LineOfStory++;

            if (CurrentStory[LineOfStory][0] == '*')
                CompilateString(CurrentStory[LineOfStory]);

            return CurrentStory[LineOfStory];
        }

        private static void CompilateString(string codeString)
        {
            string command = codeString.Substring(1, codeString.IndexOf(':') - 1);

            if (command == "ChangeBackground")
                ChangeBackground(codeString);

            GetNextLine();
        }

        private static void ChangeBackground(string codeString)
        {
            string backgroundName = codeString.Substring(
                codeString.IndexOf(':') + 1,
                codeString.Length - codeString.IndexOf(':') - 1);
            MediaHelper.SetBackground(backgroundName);
        }
    }
}
