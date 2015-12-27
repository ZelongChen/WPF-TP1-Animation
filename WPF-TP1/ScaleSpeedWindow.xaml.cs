using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_TP1
{
    /// <summary>
    /// ScaleSpeedWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ScaleSpeedWindow : Window
    {
        MainWindow main;
        Storyboard sbRectangleScaleTransform;
        Storyboard sbCircleScaleTransform;
        Storyboard sbImageScaleTransform;

        bool isStartScale;

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

        public ScaleSpeedWindow(bool isScale)
        {
            InitializeComponent();
            this.isStartScale = isScale;

            main = (MainWindow)Application.Current.MainWindow;

            sbRectangleScaleTransform = (Storyboard)main.RectangleItem.FindResource("RectangleScaleTransformStoryboard");
            sbCircleScaleTransform = (Storyboard)main.CircleItem.FindResource("CircleScaleTransformStoryboard");
            sbImageScaleTransform = (Storyboard)main.ImageItem.FindResource("ImageScaleTransformStoryboard");

            string currentProjectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            Uri uri = new Uri(currentProjectPath + @"\Icon\speed.png");
            this.Icon = new BitmapImage(uri);
        }

        private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.speedSlider.Value == 0)
            {

                if (isStartScale)
                {
                    sbRectangleScaleTransform.Pause();
                    sbCircleScaleTransform.Pause();
                    sbImageScaleTransform.Pause();
                }

            }
            else
            {
                int speedRatio = (int)this.speedSlider.Value;

                if (isStartScale)
                {
                    sbRectangleScaleTransform.Resume();
                    sbCircleScaleTransform.Resume();
                    sbImageScaleTransform.Resume();

                    sbRectangleScaleTransform.SetSpeedRatio(speedRatio);
                    sbCircleScaleTransform.SetSpeedRatio(speedRatio);
                    sbImageScaleTransform.SetSpeedRatio(speedRatio);
                }

            }
        }
    }
}
