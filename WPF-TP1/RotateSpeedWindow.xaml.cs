using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace WPF_TP1
{
    /// <summary>
    /// SpeedWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SpeedWindow : Window
    {
        MainWindow main;

        Storyboard sbRectangleRotateTransform;
        Storyboard sbCircleRotateTransform;
        Storyboard sbImageRotateTransform;

        bool isStartRotation;
        bool isStartScale;

        public bool IsStartRotation
        {
            get
            {
                return isStartRotation;
            }

            set
            {
                isStartRotation = value;
            }
        }

        public bool IsStartScale
        {
            get
            {
                return isStartScale;
            }

            set
            {
                isStartScale = value;
            }
        }

        public SpeedWindow(bool isRotation)
        {
            InitializeComponent();

            this.isStartRotation = isRotation;

            main = (MainWindow)Application.Current.MainWindow;

            sbRectangleRotateTransform = (Storyboard)main.RectangleItem.FindResource("RectangleRotateTransformStoryBoard");
            sbCircleRotateTransform = (Storyboard)main.CircleItem.FindResource("CircleRotateTransformStoryBoard");
            sbImageRotateTransform = (Storyboard)main.ImageItem.FindResource("ImageRotateTransformStoryBoard");

            string currentProjectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            Uri uri = new Uri(currentProjectPath + @"\Icon\speed.png");
            this.Icon = new BitmapImage(uri);
        }

        private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.speedSlider.Value == 0)
            {
                if (isStartRotation)
                {
                    sbCircleRotateTransform.Pause();
                    sbImageRotateTransform.Pause();
                    sbRectangleRotateTransform.Pause();
                }

            }
            else
            {
                int speedRatio = (int)this.speedSlider.Value;

                if (isStartRotation)
                {
                    sbCircleRotateTransform.Resume();
                    sbImageRotateTransform.Resume();
                    sbRectangleRotateTransform.Resume();

                    sbImageRotateTransform.SetSpeedRatio(speedRatio);
                    sbRectangleRotateTransform.SetSpeedRatio(speedRatio);
                    sbCircleRotateTransform.SetSpeedRatio(speedRatio);
                }

            }
        }
    }
}
