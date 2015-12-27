using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF_TP1
{

    public partial class ColorWindow : Window
    {
        MainWindow main = (MainWindow)Application.Current.MainWindow;

        public ColorWindow()
        {
            InitializeComponent();
            string currentProjectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            Uri uri = new Uri(currentProjectPath + @"\Icon\color.png");
            this.Icon = new BitmapImage(uri);
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            SolidColorBrush colorSelected = new SolidColorBrush(ColorPicker.SelectedColor.Value);
            main.CircleItem.Fill = colorSelected;
            main.RectangleItem.Fill = colorSelected;

            this.ColorSelectedText.Foreground = colorSelected;
        }
    }
}
