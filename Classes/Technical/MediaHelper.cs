﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace SKA_Novel.Classes.Technical
{
    public class MediaHelper
    {
        public static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory + "Resources\\";
        public static readonly string ImagesDirectory = BaseDirectory + "Images\\";
        public static readonly string BackgroundsDirectory = ImagesDirectory + "Backgrounds\\";
        public static readonly string FilesDirectory = BaseDirectory + "Files\\";
        public static readonly string MusicDirectory = BaseDirectory + "Music\\";

        public static MediaPlayer MainMusicPlayer = new MediaPlayer();

        public MediaHelper()
        {
            MainMusicPlayer.MediaEnded += MusicFinish;
        }

        public static string GetTextFromFile(string fileName)
        {
            StreamReader reader = new StreamReader(FilesDirectory + fileName + ".txt");
            string result = reader.ReadToEnd();
            return result;
        }

        public static string[] BeatStringToLines(string targetText, string separator = "\r\n")
        {
            return targetText.Split(new string[] {separator}, StringSplitOptions.None);
        }

        public static void SetBackground(string backgroundName)
        {
            ControlsManager.AppMainWindow.Background = new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(BackgroundsDirectory + backgroundName + ".jpg"))
            };
        }

        public static void SetGameMusic(string musicName)
        {
            MainMusicPlayer.Stop();
            MainMusicPlayer.Open(new Uri(MusicDirectory + musicName + ".wav"));

            MainMusicPlayer.Play();
        }

        private static void MusicFinish(object sender, EventArgs e)
        {
            MainMusicPlayer.Position = TimeSpan.Zero;
            MainMusicPlayer.Play();
        }
    }
}
