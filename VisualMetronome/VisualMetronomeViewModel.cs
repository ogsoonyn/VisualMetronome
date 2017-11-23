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
                base.RaisePropertyChanged(() => PhysicalWidth);
                base.RaisePropertyChanged(() => AnimeDuration);
            }
        }

        private double _mmPerSec;
        public double MilliPerSec
        {
            get { return _mmPerSec; }
            set { _mmPerSec = value;

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
            PhysicalWidth = 550; // mm
            MilliPerSec = 40;
            PixelWidth = SystemParameters.WorkArea.Width;
            PixelHeight = SystemParameters.WorkArea.Height;
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
            MessageBox.Show("Visual Metronome v0.01\n2017/11/23", "Version");
        }
    }
}
