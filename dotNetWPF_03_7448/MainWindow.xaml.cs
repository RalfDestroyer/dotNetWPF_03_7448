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

namespace dotNetWPF_03_7448
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyUserControl CourentPrinter;
        Queue<MyUserControl> queue;

        public MainWindow()
        {
            InitializeComponent();

            queue = new Queue<MyUserControl>();
            foreach (Control item in printersGrid.Children)
            {
                if (item is MyUserControl)
                {
                    MyUserControl printer = item as MyUserControl;
                    printer.InkEmpty += new EventHandler<PrinterEventArgs>(UseDoInkEmpty);
                    printer.PageMissing += new EventHandler<PrinterEventArgs>(UseDoPageMissing);
                    queue.Enqueue(printer);
                }
            }
            CourentPrinter = queue.Dequeue();
            CourentPrinter.BorderThickness = new Thickness(4, 4, 4, 4);
            CourentPrinter.BorderBrush = new SolidColorBrush(Color.FromRgb(0x00, 0xCE, 0x01));
            return;
        }

        private void UseDoPageMissing(object sender, PrinterEventArgs e)
        {
            MessageBox.Show(DateTime.Now.ToString() + e.message + e.name, MessageBoxButton.OK.ToString());
            if (e.isCritical == true)
                ChangePrinter();
            return;
        }

        private void UseDoInkEmpty(object sender, PrinterEventArgs e)
        {
            MessageBox.Show(DateTime.Now.ToString() + e.message + e.name, MessageBoxButton.OK.ToString());
            if (e.isCritical == true)
                ChangePrinter();
            return;
        }

        private void ChangePrinter()
        {
            CourentPrinter.BorderThickness = new Thickness(1,1,1,1);
            CourentPrinter.BorderBrush = new SolidColorBrush(Color.FromRgb(0x11, 0x11, 0x11));
            queue.Enqueue(CourentPrinter);


            CourentPrinter = queue.Dequeue();
            CourentPrinter.BorderThickness = new Thickness(4, 4, 4, 4);
            CourentPrinter.BorderBrush = new SolidColorBrush(Color.FromRgb(0x00, 0xCE, 0x01));
        }

        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            CourentPrinter.Print();
            return;
        }
    }
}
