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
        public MainWindow()
        {
            InitializeComponent();

            ControlsManager.AppMainWindow = this;
            ControlsManager.OptionPanel = stckPnlOptions;
            ControlsManager.MainTextPanel = brdMainText;
            ControlsManager.MainText = txtMainText;
            ControlsManager.HeroPosition1 = imgHeroPosition1;
            ControlsManager.HeroPosition2 = imgHeroPosition2;

            StoryCompilator.CurrentStory = MediaHelper.BeatStringToLines(MediaHelper.GetTextFromFile("Test"));
            StoryCompilator.GoNextLine();
        }

        private void btBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void brdMainText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StoryCompilator.GoNextLine();
        }
    }
}
