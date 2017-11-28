using System;
using System.Windows;
using System.Windows.Input;

namespace VisualMetronome
{
    public class VisualMetronomeViewModel : ViewModelBase
    {
        private double _winWidth;
        public double PixelWidth
        {
            get { return _winWidth; }
            set
            {
                _winWidth = value;
                base.RaisePropertyChanged(() => PixelWidth);
            }
        }

        private double _pixelHeight;
        public double PixelHeight
        {
            get { return _pixelHeight; }
            set
            {
                _pixelHeight = value;
                base.RaisePropertyChanged(() => PixelHeight);
            }
        }

        private double _physicalWidth;
        public double PhysicalWidth
        {
            get { return _physicalWidth; }
            set
            {
                _physicalWidth = value;
                Properties.Settings.Default.PhysicalWidth = _physicalWidth;
                Properties.Settings.Default.Save();
                base.RaisePropertyChanged(() => PhysicalWidth);
                base.RaisePropertyChanged(() => AnimeDuration);
            }
        }

        private double _mmPerSec;
        public double MilliPerSec
        {
            get { return _mmPerSec; }
            set { _mmPerSec = value;

                Properties.Settings.Default.MilliPerSec = _mmPerSec;
                Properties.Settings.Default.Save();

                // calculate duration from speed
                base.RaisePropertyChanged(() => MilliPerSec);
                base.RaisePropertyChanged(() => AnimeDuration);
            }
        }

        public Duration AnimeDuration
        {
            get { return new Duration(TimeSpan.FromMilliseconds(PhysicalWidth / MilliPerSec * 1000)); }
            set { base.RaisePropertyChanged(() => AnimeDuration); }
        }

        public VisualMetronomeViewModel()
        {
            PhysicalWidth = Properties.Settings.Default.PhysicalWidth;
            MilliPerSec = Properties.Settings.Default.MilliPerSec;
            PixelWidth = SystemParameters.WorkArea.Width;
            PixelHeight = SystemParameters.WorkArea.Height;

            // コマンドライン引数からパラメータ取得！
            if (App.CommandLineArgs != null)
            {
                int index = 0;
                foreach(string arg in App.CommandLineArgs)
                {
                    switch (index)
                    {
                        case 0:
                            PhysicalWidth = Double.Parse(arg);
                            break;
                        case 1:
                            MilliPerSec = Double.Parse(arg);
                            break;
                        default:
                            break;
                    }
                    index++;
                }
            }
        }

        private ICommand _showVersionCmd;
        public ICommand ShowVersionCmd
        {
            get
            {
                if(_showVersionCmd == null)
                {
                    _showVersionCmd = new RelayCommand(
                        param => showVersionCmdExecute());
                }
                return _showVersionCmd;
            }
        }
        private void showVersionCmdExecute()
        {
            MessageBox.Show("Visual Metronome v0.02\n2017/11/28", "Version");
        }
    }
}
