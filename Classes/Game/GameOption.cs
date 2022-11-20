﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SKA_Novel.Classes.Game
{
    internal class GameOption : System.Windows.Controls.TextBlock
    {
        public readonly string TargetFile;
        public readonly int KarmaWeight;

        public GameOption(string fileName, int karmaWeight, string optionText)
        {
            TargetFile = fileName;
            KarmaWeight = karmaWeight;
            Text = optionText;
            MouseDown += GameOption_MouseDown;

            TextAlignment = TextAlignment.Center;
            TextWrapping = TextWrapping.Wrap;
            FontSize = 42;
            FontWeight = FontWeights.Bold;
            FontStyle = FontStyles.Italic;
            Foreground = Brushes.White;
            Margin = new Thickness(20);
        }

        private void GameOption_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Technical.ControlsManager.KarmaLevel += KarmaWeight;
            Technical.StoryCompilator.GoNextFile(TargetFile);
            Technical.ControlsManager.OptionPanel.Children.Clear();
            Technical.ControlsManager.MainTextPanel.Visibility = Visibility.Visible;
            Technical.StoryCompilator.GoNextLine();
        }
    }
}
