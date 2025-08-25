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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;
using NovaBrush;
using NovaBrush._02_BrushSys;
using NovaBrush._02_BrushSys.Tools;
using System.Security.Cryptography;

namespace _NovaBrush
{

    public partial class MainWindow : Window
    {

        private CanvasPanel canvasPanel;
        private WriteableBitmap _canvasBitmap;
        private ITool itool;

        public MainWindow()
        {
            InitializeComponent();
            CreateCanvas();
            Globals.Initialize(this);
        }

        // Places the Canvas
        public void CreateCanvas()
        {
            canvasPanel = new CanvasPanel
            {
                Width = 32,
                Height = 32,

            };

            // Add it to the named parent container from XAML
            MainCanvas.Children.Add(canvasPanel);
            canvasPanel.prepareCanvas();
        }

        // Click Open Button
        private void btn_open_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.FileName = "Image"; // Default file name
            ofd.DefaultExt = ".png"; // Default file extension
            ofd.Filter = ".Image Files (*.png)|*.png"; // Filter files by extension

            bool? result = ofd.ShowDialog();

            if (result == true)
            {
                try
                {
                    string filename = ofd.FileName;

                    BitmapImage bitmapImage = new BitmapImage(new Uri(filename)); // This makes the image support alpha
                    var convertedBitmap = new FormatConvertedBitmap(
                        bitmapImage,
                        PixelFormats.Bgra32,
                        null,
                        0);

                    WriteableBitmap writable = new WriteableBitmap(convertedBitmap);

                    canvasPanel.LoadBitmap(writable);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // Hide/Unhide Function, Reuse it
        private void ToggleVisibility_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement button)
            {
                if (button.Tag is UIElement target)
                {
                    target.Visibility = target.Visibility == Visibility.Visible
                        ? Visibility.Collapsed
                        : Visibility.Visible;
                }
            }
        }

        // If you shift the slider
        private void sdr_brushsize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Globals.CurrentTool == null) return;
            double newValue = e.NewValue;
            Globals.CurrentTool.Size = (int)newValue;
            Globals.BrushSizeLabel.Text = "Size: " + Convert.ToString((int)newValue) + "px";
        }

        // The Anti Aliasing button
        private void btn_AA_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.CurrentTool == null) return;
            Globals.CurrentTool.AA = !Globals.CurrentTool.AA;
            Globals.AntiAliasingLabel.Text = "AA: " + (Globals.CurrentTool.AA ? "ON" : "OFF");
        }

        // File saving prompt
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            var sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Filter = ".Image Files (*.png)|*.png"; // Filter files by extension
            sfd.DefaultExt = ".png"; // Default file extension

            if (sfd.ShowDialog() == true)
            {
                string filename = sfd.FileName;
                SaveBitmapToFile(Globals.BitmapToDraw, filename);
            }
        }

        // Saving attempt
        private static void SaveBitmapToFile(WriteableBitmap bitmap, String filepath)
        {
            try
            {
                if (bitmap == null) throw new ArgumentNullException(nameof(bitmap));

                BitmapEncoder encoder = GetEncoder(filepath);

                //bitmap.Freeze(); //tihs freezes the program, hmm

                encoder.Frames.Add(BitmapFrame.Create(bitmap));

                using (var fileStream = new System.IO.FileStream(filepath, System.IO.FileMode.Create))
                {
                    encoder.Save(fileStream);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save image: {ex.Message}", "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Get extension, and then encode
        private static BitmapEncoder GetEncoder(string filepath)
        {
            string ext = System.IO.Path.GetExtension(filepath).ToLower();

            return ext switch
            {
                ".png" => new PngBitmapEncoder(),
                _ => new PngBitmapEncoder() // more file types soon
            };
        }
    }
}