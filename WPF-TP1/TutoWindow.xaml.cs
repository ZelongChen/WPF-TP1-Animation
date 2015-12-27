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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_TP1
{
    /// <summary>
    /// TutoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TutoWindow : Window
    {
        public TutoWindow()
        {
            InitializeComponent();
            string currentProjectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            Uri uri = new Uri(currentProjectPath + @"\Video\TutoVideo.mp4");
            this.TutoVideo.Source = uri;

            uri = new Uri(currentProjectPath + @"\Icon\video.png");
            this.Icon = new BitmapImage(uri);
        }


        private void PlayVideo_Click(object sender, RoutedEventArgs e)
        {
            if (PlayVideo.Content.ToString() == "Play")
            {
                PlayVideo.Content = "Pause";
                TutoVideo.LoadedBehavior = MediaState.Manual;
                TutoVideo.Play();
            } else
            {
                PlayVideo.Content = "Play";
                TutoVideo.LoadedBehavior = MediaState.Manual;
                TutoVideo.Pause();
            }

        }
    }
}
