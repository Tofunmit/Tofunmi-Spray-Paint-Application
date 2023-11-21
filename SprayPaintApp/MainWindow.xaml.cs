using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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

namespace SprayPaintApp
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Track if mouse is down for painting
        private bool isPainting = false;

        // Previous mouse point for painting
        private Point previousPoint;

        // Track if eraser mode is on
        private bool isErasing = false;

        public MainWindow()
        {
            InitializeComponent();
            // Set max height to screen height
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

        }

        // Method to handle dragging the window by clicking on the title bar
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        // Minimize window on icon click
        private void imgmin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // Toggle maximize/restore on icon click
        private void imgmax_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                this.WindowState = WindowState.Normal;

            }
        }

        // Close app on icon click
        private void imgclose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        // Method to handle loading an image
        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            // Open a dialog to select an image file
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".png",
                Filter = "Image files (*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Load the selected image and set it as the background of the canvas
                var image = new BitmapImage(new Uri(openFileDialog.FileName));
                PaintCanvas.Background = new ImageBrush(image);
            }
        }

        // Method to handle saving changes to the canvas
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            // Render the canvas to a bitmap
            var renderTargetBitmap = new RenderTargetBitmap((int)PaintCanvas.ActualWidth, (int)PaintCanvas.ActualHeight, 96, 96, PixelFormats.Default);
            renderTargetBitmap.Render(PaintCanvas);

            // Create a PNG encoder and add the bitmap frame
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            // Open a dialog to select the save location and save the image
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                DefaultExt = ".png",
                Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (var fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    encoder.Save(fileStream);
                }
            }
        }

        // Method to handle mouse movement on the canvas
        private void PaintCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            // Paint if mouse down
            if (PaintCanvas != null && PaintCanvas.Background is ImageBrush && isPainting)
            {
                // If painting, simulate spray paint on the canvas
                var currentPosition = e.GetPosition(PaintCanvas);
                SprayPaint(previousPoint, currentPosition);
                previousPoint = currentPosition;
            }
            else if (isErasing)
            {
                // If erasing, remove spray elements from the canvas
                var currentPosition = e.GetPosition(PaintCanvas);
                EraseSpray(currentPosition);
            }

        }

        // Turn on eraser when checkbox checked
        private void EraserCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            isErasing = true;
        }

        // Turn off eraser when checkbox unchecked
        private void EraserCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            isErasing = false;
        }

        // Methods to handle mouse down and up events on the canvas
        private void PaintCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // When mouse is down, start painting and set cursor to crosshair
            isPainting = true;
            PaintCanvas.Cursor = Cursors.Cross;
            previousPoint = e.GetPosition(PaintCanvas);
        }

        // Stop painting on mouse up
        private void PaintCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // When mouse is up, stop painting and set cursor to arrow
            isPainting = false;
            PaintCanvas.Cursor = Cursors.Arrow;
        }

        // Method to simulate spray painting etween two points on the canvas
        private void SprayPaint(Point startPoint, Point endPoint)
        {
            // Code to generate spray paint effect
            var density = DensitySlider.Value;
            var distance = Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2));
            var stepSize = 1 / density;

            // Iterate through points and create ellipses to simulate spray
            for (double t = 0; t < 1; t += stepSize)
            {
                var x = (1 - t) * startPoint.X + t * endPoint.X;
                var y = (1 - t) * startPoint.Y + t * endPoint.Y;

                var spray = new Ellipse
                {
                    Width = SizeSlider.Value,
                    Height = SizeSlider.Value,
                    Fill = new SolidColorBrush(ColorPicker.SelectedColor.Value)
                    {
                        Opacity = OpacitySlider.Value / 100
                    },
                };
                Canvas.SetLeft(spray, x - spray.Width / 2);
                Canvas.SetTop(spray, y - spray.Height / 2);

                PaintCanvas.Children.Add(spray);
            }
        }

        // Method to check if a point is within a UIElement
        private bool IsPointWithinElement(Point point, UIElement element)
        {
            // Collision detection code
            var rect = new Rect(Canvas.GetLeft(element), Canvas.GetTop(element), element.RenderSize.Width, element.RenderSize.Height);
            return rect.Contains(point);
        }

        // Erase paint under mouse position
        private void EraseSpray(Point position)
        {
            var sprayToRemove = new List<UIElement>();

            // Identify spray elements within the specified position and remove them
            foreach (UIElement element in PaintCanvas.Children)
            {
                if (element is Ellipse spray && IsPointWithinElement(position, spray))
                {
                    sprayToRemove.Add(spray);
                }
            }

            foreach (var spray in sprayToRemove)
            {
                PaintCanvas.Children.Remove(spray);
            }
        }
    }
}