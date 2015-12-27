using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_TP1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Storyboard sbRectangleRotateTransform;
        Storyboard sbCircleRotateTransform;
        Storyboard sbImageRotateTransform;
        Storyboard sbRectangleScaleTransform;
        Storyboard sbCircleScaleTransform;
        Storyboard sbImageScaleTransform;
        Storyboard sbCircleMoveTransform;
        Storyboard sbRectangleMoveTransform;
        Storyboard sbImageMoveTransform;

        bool isStartRotation;
        bool isStartScale;

        ColorWindow colorWindow;
        SpeedWindow rotateSpeedWindow;
        ScaleSpeedWindow scaleSpeedWindow;

        PathFigure pathFigure = new PathFigure();
        PathFigureCollection pathFigureCollection;

        public MainWindow()
        {
            InitializeComponent();

            sbRectangleRotateTransform = (Storyboard)RectangleItem.FindResource("RectangleRotateTransformStoryBoard");
            sbCircleRotateTransform = (Storyboard)CircleItem.FindResource("CircleRotateTransformStoryBoard");
            sbImageRotateTransform = (Storyboard)ImageItem.FindResource("ImageRotateTransformStoryBoard");
            sbRectangleScaleTransform = (Storyboard)RectangleItem.FindResource("RectangleScaleTransformStoryboard");
            sbCircleScaleTransform = (Storyboard)CircleItem.FindResource("CircleScaleTransformStoryboard");
            sbImageScaleTransform = (Storyboard)ImageItem.FindResource("ImageScaleTransformStoryboard");
            sbCircleMoveTransform = (Storyboard)CircleItem.FindResource("CircleMoveTransformStoryBoard");
            sbRectangleMoveTransform = (Storyboard)RectangleItem.FindResource("RectangleMoveTransformStoryBoard");
            sbImageMoveTransform = (Storyboard)ImageItem.FindResource("ImageMoveTransformStoryBoard");

            pathFigureCollection = (PathFigureCollection)FindResource("PathFigure");
            pathFigureCollection.Add(pathFigure);

        }

        private void CircleMenu_Click(object sender, RoutedEventArgs e)
        {
            this.CircleItem.Visibility = Visibility.Visible;
            this.RectangleItem.Visibility = Visibility.Collapsed;
            this.ImageItem.Visibility = Visibility.Collapsed;
        }

        private void RectangleMenu_Click(object sender, RoutedEventArgs e)
        {
            this.CircleItem.Visibility = Visibility.Collapsed;
            this.RectangleItem.Visibility = Visibility.Visible;
            this.ImageItem.Visibility = Visibility.Collapsed;
        }

        private void ImageMenu_Click(object sender, RoutedEventArgs e)
        {
            this.CircleItem.Visibility = Visibility.Collapsed;
            this.RectangleItem.Visibility = Visibility.Collapsed;
            this.ImageItem.Visibility = Visibility.Visible;

            Microsoft.Win32.OpenFileDialog openfile = new Microsoft.Win32.OpenFileDialog();
            openfile.Filter = " PNG Files(*.png) | *.png| JPEG Files(*.jpeg) | *.jpeg| JPG Files(*.jpg) | *.jpg| GIF Files(*.gif) | *.gif| All Files| *.*";
            Nullable<bool> result = openfile.ShowDialog();
            if (result == true)
            {
                ImageItem.Source = new BitmapImage(new Uri(openfile.FileName));
            }
        }

        private void ChooseColorMenu_Click(object sender, RoutedEventArgs e)
        {
            colorWindow = new ColorWindow();
            colorWindow.Show();
        }

        private void RotateSpeedMenu_Click(object sender, RoutedEventArgs e)
        {
            rotateSpeedWindow = new SpeedWindow(isStartRotation);
            rotateSpeedWindow.Show();

        }


        private void ScaleSpeedMenu_Click(object sender, RoutedEventArgs e)
        {
            scaleSpeedWindow = new ScaleSpeedWindow(isStartScale);
            scaleSpeedWindow.Show();
        }

        private void StartRotationMenu_Click(object sender, RoutedEventArgs e)
        {
            isStartRotation = true;
            sbRectangleRotateTransform.Begin();
            sbCircleRotateTransform.Begin();
            sbImageRotateTransform.Begin();

            updateRotateSpeedWindow();
        }

        private void StopRotationMenu_Click(object sender, RoutedEventArgs e)
        {
            isStartRotation = false;
            sbRectangleRotateTransform.Stop();
            sbCircleRotateTransform.Stop();
            sbImageRotateTransform.Stop();

            updateRotateSpeedWindow();
        }

        private void StartScaleMenu_Click(object sender, RoutedEventArgs e)
        {
            isStartScale = true;
            sbRectangleScaleTransform.Begin();
            sbCircleScaleTransform.Begin();
            sbImageScaleTransform.Begin();

            updateScaleSpeedWindow();
        }

        private void StopScaleMenu_Click(object sender, RoutedEventArgs e)
        {
            isStartScale = false;
            sbRectangleScaleTransform.Stop();
            sbCircleScaleTransform.Stop();
            sbImageScaleTransform.Stop();

            updateScaleSpeedWindow();
        }


        private void StartDrawMenu_Click(object sender, RoutedEventArgs e)
        {
            string message = "1. Click your mouse and use it to draw a path on the panel@2. Click menu Draw->StartMove to start the movement@3. Click menu Draw->StopMove to stop the movement@@Do you want to see an exemple?";
            message = message.Replace("@", System.Environment.NewLine);
            MessageBoxResult result = MessageBox.Show(message, "Guide", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                TutoWindow tutoWindow = new TutoWindow();
                tutoWindow.Show();
            }
            this.DrawCanvas.Visibility = Visibility.Visible;
            this.DrawIndication.Visibility = Visibility.Visible;
        }

        private void StopDrawMenu_Click(object sender, RoutedEventArgs e)
        {
            pathFigure.Segments.Clear();

            this.DrawCanvas.Visibility = Visibility.Collapsed;
            this.DrawIndication.Visibility = Visibility.Collapsed;

            this.CircleItem.HorizontalAlignment = HorizontalAlignment.Center;
            this.CircleItem.VerticalAlignment = VerticalAlignment.Center;
            this.RectangleItem.HorizontalAlignment = HorizontalAlignment.Center;
            this.RectangleItem.VerticalAlignment = VerticalAlignment.Center;
            this.ImageItem.HorizontalAlignment = HorizontalAlignment.Center;
            this.ImageItem.VerticalAlignment = VerticalAlignment.Center;
            sbCircleMoveTransform.Stop();
            sbImageMoveTransform.Stop();
            sbRectangleMoveTransform.Stop();
        }

        private void DrawCanvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            pathFigure.Segments.Clear();
            if (e.ButtonState == MouseButtonState.Pressed)
                pathFigure.StartPoint = e.GetPosition(this);
        }

        private void DrawCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                LineSegment segment = new LineSegment();
                segment.Point = e.GetPosition(this);
                pathFigure.Segments.Add(segment);
            }

        }

        private void StartMoveMenu_Click(object sender, RoutedEventArgs e)
        {
            this.CircleItem.HorizontalAlignment = HorizontalAlignment.Left;
            this.CircleItem.VerticalAlignment = VerticalAlignment.Top;
            this.RectangleItem.HorizontalAlignment = HorizontalAlignment.Left;
            this.RectangleItem.VerticalAlignment = VerticalAlignment.Top;
            this.ImageItem.HorizontalAlignment = HorizontalAlignment.Left;
            this.ImageItem.VerticalAlignment = VerticalAlignment.Top;
            sbCircleMoveTransform.Begin();
            sbRectangleMoveTransform.Begin();
            sbImageMoveTransform.Begin();
        }

        private void AboutMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This application is created with passion by Zelong CHEN.", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void updateRotateSpeedWindow()
        {
            if (rotateSpeedWindow != null)
            {
                rotateSpeedWindow.IsStartRotation = isStartRotation;
            }
        }

        private void updateScaleSpeedWindow()
        {
            if (scaleSpeedWindow != null)
            {
                scaleSpeedWindow.IsStartScale = isStartScale;
            }
        }

    }
}
