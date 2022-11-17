using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SKA_Novel.Classes.Technical
{
    internal class ControlsManager
    {
        public static MainWindow AppMainWindow { get; set; }
        public static StackPanel OptionPanel { get; set; }
        public static Border MainTextPanel { get; set; }
        public static TextBlock MainText { get; set; }
        public static Image HeroPosition1 { get; set; }
        public static Image HeroPosition2 { get; set; }
        public static Image HeroPosition3 { get; set; }
    }
}
