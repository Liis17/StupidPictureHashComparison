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

using StupidPictureHashComparison.WPF.Common;

using UserControl = System.Windows.Controls.UserControl;

namespace StupidPictureHashComparison.WPF.Controls
{
    /// <summary>
    /// Логика взаимодействия для ImageMatching.xaml
    /// </summary>
    public partial class ImageMatching : UserControl
    {
        public ImageMatching(List<ImageHash> imageHashes)
        {
            InitializeComponent();
            foreach (var imageHash in imageHashes)
            {
                var imageElement = new ImageElement(imageHash);
                ImageList.Children.Add(imageElement);
            }
        }
    }
}
