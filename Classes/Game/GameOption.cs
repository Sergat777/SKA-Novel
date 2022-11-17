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

        public GameOption(string fileName, string optionText)
        {
            TargetFile = fileName;
            Text = optionText;
            MouseDown += GameOption_MouseDown;

            
            FontSize = 32;
            Foreground = Brushes.White;
            Margin = new Thickness(10);
        }

        private void GameOption_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Technical.StoryCompilator.GoNextFile(TargetFile);
            Technical.ControlsManager.OptionPanel.Children.Clear();
            Technical.ControlsManager.MainTextPanel.Visibility = Visibility.Visible;
            Technical.StoryCompilator.GoNextLine();
        }
    }
}
