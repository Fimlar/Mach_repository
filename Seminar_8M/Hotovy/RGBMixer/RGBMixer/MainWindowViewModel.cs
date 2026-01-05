using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace RGBMixer
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null!)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public SolidColorBrush Color =>
        new SolidColorBrush(System.Windows.Media.Color.FromRgb(
            (byte)R, (byte)G, (byte)B));

        private int r;
        private int g;
        private int b;

        public int R
        {
            get => r;
            set
            {
                if (value < 0 || value > 255)
                {
                    MessageBox.Show("Zadej hodnotu v platném rozsahu");
                    return;
                }
                r = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Color));
                OnPropertyChanged(nameof(Hex));
            }

        }

        public int G
        {
            get => g;
            set
            {
                g = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Color));
                OnPropertyChanged(nameof(Hex));
            }
        }

        public int B
        {
            get => b;
            set
            {
                b = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Color));
                OnPropertyChanged(nameof(Hex));
            }
        }

        public string Hex => $"#{R:X2}{G:X2}{B:X2}";
    }
}
