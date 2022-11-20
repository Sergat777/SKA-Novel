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
        bool IsShowedMenu = true;
        public MainWindow()
        {
            InitializeComponent();

            ControlsManager.AppMainWindow = this;
            ControlsManager.OptionPanel = stckPnlOptions;
            ControlsManager.MainTextPanel = brdMainText;
            ControlsManager.MainText = txtMainText;
            ControlsManager.SpeakerName = txtCurrentCharacter;
            ControlsManager.HeroPositions[0] = HeroPosition1;
            ControlsManager.HeroPositions[1] = HeroPosition2;
            ControlsManager.HeroPositions[2] = HeroPosition3;
        }

        private void brdMainText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StoryCompilator.GoNextLine();
        }

        private void btVolume_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MediaHelper.MainMusicPlayer.Volume > 0)
            {
                MediaHelper.MainMusicPlayer.Volume = 0;
                imgVolume.Source = new BitmapImage(new Uri(MediaHelper.ImagesDirectory + "mute.png"));
            }
            else
            {
                MediaHelper.MainMusicPlayer.Volume = 0.5;
                imgVolume.Source = new BitmapImage(new Uri(MediaHelper.ImagesDirectory + "volume.png"));
            }
        }

        private void btClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void btSave_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new ModalWindows.SaveSuccessWindow().ShowDialog();
        }

        private void btLoadGame_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("ВСЕ_ГО_ХО_РО_ШЕ_ГО");
        }

        private void btStartGame_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dckPnlMainMenu.Visibility = Visibility.Collapsed;
            IsShowedMenu = false;

            StoryCompilator.GoNextFile("NewTest");
            StoryCompilator.GoNextLine();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsShowedMenu)
            {
                if (e.Key == Key.Space || e.Key == Key.Enter)
                    StoryCompilator.GoNextLine();
                else
                    if (e.Key == Key.Escape)
                {
                    dckPnlMainMenu.Visibility = Visibility.Visible;
                    IsShowedMenu = true;
                }
            }
        }
    }
}
