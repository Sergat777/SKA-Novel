using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

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
            {"CreateOptionBlock", CreateOptionBlock},
            {"AddHero",  AddHero},
            {"ClearHero", ClearHero}
        };


        public static void GoNextLine()
        {
            if (ControlsManager.Timer == null || !ControlsManager.Timer.IsTyping)
            {
                if ((LineOfStory + 1) < CurrentStory.Count())
                    LineOfStory++;

                if (CurrentStory[LineOfStory].Trim()[0] == '(')
                    UpdateSpeaker(CurrentStory[LineOfStory].Trim());

                if (CurrentStory[LineOfStory].Trim()[0] == '*')
                    CompilateString(CurrentStory[LineOfStory]);

                new TypingTimer(ControlsManager.MainText, CurrentStory[LineOfStory]);
            }
            else
                if (ControlsManager.Timer != null)
                ControlsManager.Timer.FinishTyping();
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

        public static void AddHero(string codeString)
        {
            string[] arguments = GetArguments(codeString).Split(',');
            byte position = Convert.ToByte(arguments[2].Trim());

            Game.Character character = new Game.Character(arguments[0]);
            Game.CharacterView characterView = new Game.CharacterView(character, arguments[1].Trim());
            characterView.SetPosition(position);
        }

        public static void ClearHero(string codeString)
        {
            byte position = Convert.ToByte(GetArguments(codeString));

            ControlsManager.HeroPositions[--position].Children.Clear();
        }

        public static void UpdateSpeaker(string speakerString)
        {
            string shortName = speakerString.Substring(1, speakerString.Trim().Length - 1).ToUpper();

            if (shortName == "MND")
            {
                ControlsManager.SpeakerName.Text = "СОЗНАНИЕ";
                ControlsManager.MainText.Foreground = Brushes.Yellow;
                foreach (DockPanel heroPosition in ControlsManager.HeroPositions)
                    if (heroPosition.Children.Count > 0)
                        foreach (Game.CharacterView character in heroPosition.Children)
                            character.SetBlackout();
            }
            else
            {
                foreach (DockPanel heroPosition in ControlsManager.HeroPositions)
                    if (heroPosition.Children.Count > 0)
                        foreach (Game.CharacterView character in heroPosition.Children)
                            if (character.Character.ShortName == shortName)
                            {
                                ControlsManager.MainText.Foreground = character.CharacterColor;
                                ControlsManager.SpeakerName.Text = character.Character.FullName.
                                    Substring(0, character.Character.FullName.IndexOf('_')).ToUpper();
                                character.TakeOffBlackout();
                            }
                            else
                            {
                                character.SetBlackout();
                            }
            }
            LineOfStory++;
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
            {
                string option = optionsFiles[i];
                string karma = option.Substring(option.IndexOf('(') + 1, option.IndexOf(')') - (option.IndexOf('(') + 1));
                ControlsManager.OptionPanel.Children.
                    Add(new Game.GameOption(
                        option.Substring(0, option.IndexOf('(')).Trim(),
                        Convert.ToByte(karma),
                        ReadOptionsText()));
            }
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
