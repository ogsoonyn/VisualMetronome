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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnimationTest
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            mStoryboard = new Storyboard();
            mAnim = new DoubleAnimation();
        }

        Storyboard mStoryboard;
        DoubleAnimation mAnim;

        private void StartAnimation()
        {

            Storyboard.SetTarget(mAnim, rect);
            Storyboard.SetTargetProperty(mAnim, new PropertyPath("(Canvas.Left)"));
            mAnim.From = 0;
            mAnim.To = canvas.ActualWidth;
            mAnim.Duration = TimeSpan.FromMilliseconds(500);
            mAnim.RepeatBehavior = RepeatBehavior.Forever;
            mAnim.AutoReverse = true;

            mStoryboard.Children.Add(mAnim);
            mStoryboard.Begin();
        }

        private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            mAnim.To = canvas.ActualWidth;
            mStoryboard.Begin();
            rect.Height = canvas.ActualHeight;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartAnimation();
        }
    }

    public class MainWindowModel : ViewModelBase
    {
        private double _interaval;
        public double Interval
        {
            get { return _interaval; }
            set
            {
                _interaval = value;
                this.RaisePropertyChanged(() => Interval);
            }
        }

    }
}
