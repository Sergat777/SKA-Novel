using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SKA_Novel.Classes.Game
{
    internal class CharacterView : System.Windows.Controls.Image
    {
        public Character Character { get; }
        public SolidColorBrush CharacterColor;

        public CharacterView(Character character, string characterColor = "#FFF")
        {
            Character = character;
            if (string.IsNullOrWhiteSpace(characterColor))
                CharacterColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF");
            else
                CharacterColor = (SolidColorBrush)new BrushConverter().ConvertFrom(characterColor);
            Source = new BitmapImage(new Uri(Technical.MediaHelper.ImagesDirectory + character.FullName + ".png"));
        }

        public void SetPosition(byte positionNumber)
        {
            Technical.ControlsManager.HeroPositions[--positionNumber].Children.Add(this);
        }

        public void SetBlackout()
        {
            Source = new BitmapImage(new Uri(Technical.MediaHelper.ImagesDirectory + Character.FullName + "_затемнение.png"));
        }

        public void TakeOffBlackout()
        {
            Source = new BitmapImage(new Uri(Technical.MediaHelper.ImagesDirectory + Character.FullName + ".png"));
        }
    }
}
