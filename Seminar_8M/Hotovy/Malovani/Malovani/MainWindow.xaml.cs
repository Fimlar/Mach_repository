using System.Drawing;
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

namespace Malovani
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // proměnné pro canvas
        private bool _isDrawing = false;

        private double _circleRadius = 10;

        private System.Windows.Media.Color _color = Colors.Black;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void colorBtn_Click(object sender, RoutedEventArgs e)
        {
            RGBColorPicker _RGBColorPicker = new RGBColorPicker();
            _RGBColorPicker.ShowDialog();   // Modální okno

            if (_RGBColorPicker.Success == true)
            {
                colorBtn.Background = new SolidColorBrush(_RGBColorPicker.Color);
                _color = _RGBColorPicker.Color;
            }
        }

        private void openBtn_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".jpeg"; // Default file extension
            dialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dialog.FileName;
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".jpeg"; // Default file extension
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result.Value)
            {
                // Save document
                string filename = dlg.FileName;
            }
        }

        private void drawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDrawing)
            {
                // Získání aktuální pozice myši
                System.Windows.Point currentPoint = e.GetPosition(drawingCanvas);

                // Vytvoření kruhu (Ellipse)
                Ellipse circle = new Ellipse
                {
                    Width = _circleRadius,
                    Height = _circleRadius,
                    Fill = new SolidColorBrush(_color) // Barva kruhu
                };

                // Nastavení pozice kruhu
                Canvas.SetLeft(circle, currentPoint.X - _circleRadius);
                Canvas.SetTop(circle, currentPoint.Y - _circleRadius);

                // Přidání kruhu na Canvas
                drawingCanvas.Children.Add(circle);
            }
        }

        private void drawingCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isDrawing = true;
        }

        private void drawingCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isDrawing = false;
        }

        private void widthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _circleRadius = (double)e.NewValue;
        }
    }
}