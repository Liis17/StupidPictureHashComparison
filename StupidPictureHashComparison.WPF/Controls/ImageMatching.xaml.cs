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
        public ImageMatching(ImageHash img1, ImageHash img2)
        {
            InitializeComponent();
            var path1 = img1.Path;
            var path2 = img2.Path;
            var hash1 = img1.Hash;
            var hash2 = img2.Hash;
            TextPath1.Text = path1;
            TextPath2.Text = path2;
            TextHash1.Text = hash1;
            TextHash2.Text = hash2;

            Image1.Source = new BitmapImage(new Uri(path1));
            Image2.Source = new BitmapImage(new Uri(path2));
        }
    }
}
