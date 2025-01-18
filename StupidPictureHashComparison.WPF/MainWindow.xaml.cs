using System.IO;
using System.Security.Cryptography;
using System.Text;
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

using StupidPictureHashComparison.WPF.Controls;

using Path = System.IO.Path;

namespace StupidPictureHashComparison.WPF;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
        {
            dialog.Description = "Select the folder containing images";
            dialog.ShowNewFolderButton = false;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folderPath = dialog.SelectedPath;
                var imageHashes = await ProcessImagesInFolder(folderPath);
                DisplayMatchingImages(imageHashes);
            }
        }
    }

    private async Task<List<ImageHash>> ProcessImagesInFolder(string folderPath)
    {
        var imageFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories)
            .Where(file => new[] { ".jpeg", ".jpg", ".png", ".webp" }.Contains(Path.GetExtension(file).ToLower()))
            .ToList();

        var imageHashes = new List<ImageHash>();
        var tasks = new List<Task>();

        foreach (var file in imageFiles)
        {
            tasks.Add(Task.Run(() =>
            {
                var hash = ComputeSha256Hash(file);
                imageHashes.Add(new ImageHash(file, hash));
            }));

            if (tasks.Count >= 30)
            {
                await Task.WhenAll(tasks);
                tasks.Clear();
                GC.Collect();
            }
        }

        if (tasks.Any())
        {
            await Task.WhenAll(tasks);
        }

        return imageHashes;
    }

    private string ComputeSha256Hash(string filePath)
    {
        using (FileStream stream = File.OpenRead(filePath))
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(stream);
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }

    private void DisplayMatchingImages(List<ImageHash> imageHashes)
    {
        var groupedHashes = imageHashes.GroupBy(ih => ih.Hash).Where(g => g.Count() > 1).ToList();

        foreach (var group in groupedHashes)
        {
            var matches = group.ToList();
            for (int i = 0; i < matches.Count; i++)
            {
                for (int j = i + 1; j < matches.Count; j++)
                {
                    var imageMatching = new ImageMatching(matches[i], matches[j]);
                    stackPanel.Children.Add(imageMatching);
                }
            }
        }
    }
}

