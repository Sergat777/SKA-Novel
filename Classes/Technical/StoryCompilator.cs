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
            {"GoNextFile", GoNextFile},
            {"CreateOptionBlock", CreateOptionBlock}
        };

        public static void GoNextLine()
        {
            if ((LineOfStory + 1) < CurrentStory.Count())
                LineOfStory++;

            if (CurrentStory[LineOfStory][0] == '*')
                CompilateString(CurrentStory[LineOfStory]);

            ControlsManager.MainText.Text = CurrentStory[LineOfStory];
        }

        private static void CompilateString(string codeString)
        {
            string command = codeString.Substring(1, codeString.IndexOf(':') - 1).Trim();

            foreach (string commandName in Commands.Keys)
                if (command == commandName)
                    Commands[command](codeString);

            GoNextLine();
        }

        private static string GetArguments(string codeString)
        {
            return codeString.Substring(
                codeString.IndexOf(':') + 1,
                codeString.Length - (codeString.IndexOf(':') + 1)).Trim();
        }

        public static void SetBackground(string codeString)
        {
            MediaHelper.SetBackground(GetArguments(codeString));
        }

        public static void SetMusic(string codeString)
        {
            MediaHelper.SetGameMusic(GetArguments(codeString));
        }

        public static void GoNextFile(string codeString)
        {
            CurrentStory = MediaHelper.BeatStringToLines(MediaHelper.GetTextFromFile(GetArguments(codeString)));
            LineOfStory = -1;
        }

        public static void CreateOptionBlock(string codeString)
        {
            string[] options = GetArguments(codeString).Split(',');
            AddOptions(options);
            ControlsManager.MainTextPanel.Visibility = System.Windows.Visibility.Hidden;
        }

        private static void AddOptions(string[] optionsFiles)
        {
            for (int i = 0; i < optionsFiles.Count(); i++)
                ControlsManager.OptionPanel.Children.
                    Add(new Game.GameOption(optionsFiles[i].Trim(), ReadOptionsText()));
        }

        private static string ReadOptionsText()
        {
            LineOfStory++;

            if (CurrentStory[LineOfStory].Trim()[0] == '{')
            {
                LineOfStory++;
                return CurrentStory[LineOfStory];
            }
            else
                if (CurrentStory[LineOfStory].Trim()[0] == '}')
            {
                LineOfStory++;
                LineOfStory++;
            }

            return CurrentStory[LineOfStory];
        }
    }
}
