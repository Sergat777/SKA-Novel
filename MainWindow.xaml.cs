using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SKA_Novel.Classes.Technical;



namespace SKA_Novel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int line = -1;
        public static string[] CurrentStory;

        public MainWindow()
        {
            InitializeComponent();
            ControlsManager.AppMainWindow = this;
            MediaHelper.SetBackground("outside");
            MediaHelper.SetGameMusic("standartMusic");
            CurrentStory = MediaHelper.BeatStringToLines(MediaHelper.GetTextFromFile("Test"));
        }

        private void btBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void brdMainText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (line + 1 < CurrentStory.Count())
            {
                line++;
                txtMainText.Text = CurrentStory[line];
            }
        }
        //public static readonly string[] CurrentStory;
    }
}
