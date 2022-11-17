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

        public delegate void Command(string codeString);

        //{"ChangeBackground", "Изменение заднего фона, принимает имя фона"},
        //{"ChangeMusic", "Изменение музыки, принимает имя музыки (.wav)"},
        //{ "GoNextFile", "Переход к новому файлу, принимает имя файла"}

        public static Dictionary<String, Command> Commands { get; } = new Dictionary<string, Command>()
        {
            {"SetBackground", SetBackground},
            {"SetMusic", SetMusic},
            {"GoNextFile", GoNextFile}
        };

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

            foreach (string commandName in Commands.Keys)
                if (command == commandName)
                    Commands[command](codeString);

            GetNextLine();
        }

        private static string GetArguments(string codeString)
        {
            return codeString.Substring(
                codeString.IndexOf(':') + 1,
                codeString.Length - (codeString.IndexOf(':') + 1));
        }

        private static void SetBackground(string codeString)
        {
            MediaHelper.SetBackground(GetArguments(codeString));
        }

        private static void SetMusic(string codeString)
        {

        }

        private static void GoNextFile(string codeString)
        {
            CurrentStory = MediaHelper.BeatStringToLines(MediaHelper.GetTextFromFile(GetArguments(codeString)));
            LineOfStory = -1;
        }
    }
}
