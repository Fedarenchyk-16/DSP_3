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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public HarmonicViewModel HarmonicViewModel { get; } = new();
        public PolyHarmonicViewModel PolyHarmonicViewModel { get; } = new();
        public FFTViewModel FFTViewModel { get; } = new();
        public FilterViewModel FilterViewModel { get; } = new();
        public CommonViewModel CommonViewModel { get; } = new();
        
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }
    }
}
