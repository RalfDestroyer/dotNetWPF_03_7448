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
    /// Interaction logic for MyUserControl.xaml
    /// </summary>
    public partial class MyUserControl : UserControl
    {
        #region fields
        public string PrinterName;
        public double InkCount;
        public int PageCount;
        #endregion

        #region CONST
        int MAX_INK = 100;
        int MIN_ADD_INK = 50;
        int MAX_PRINT_INK = 15;

        int MAX_PAGES = 400;
        int MIN_ADD_PAGES = 200;
        int MAX_PRINT_PAGES = 80;
        #endregion

        #region events
        public event EventHandler<PrinterEventArgs> PageMissing;
        public event EventHandler<PrinterEventArgs> InkEmpty;
        #endregion


        public static int PrinterNumber = 1;
        public static Random rnd;

        public MyUserControl()
        {
            InitializeComponent();
            rnd = new Random();

            this.PrinterName = "Printer " + PrinterNumber.ToString();
            this.label_pName.Content = "Printer " + PrinterNumber.ToString();
            this.InkCount = rnd.Next(MIN_ADD_INK, MAX_INK);
            this.inkCountProgressBar.Value = InkCount;
            this.PageCount = rnd.Next(MIN_ADD_PAGES, MAX_PAGES);
            this.pageCountSlider.Value = PageCount;


            PrinterNumber++;
        }

        #region METHODS
        public void Print()
        {
            
           


            double ink = rnd.Next(1, MAX_PRINT_INK + 1);
            int pages = rnd.Next(1, MAX_PRINT_PAGES + 1);
            //int MissingPages = -1 * (this.PageCount - pages);
            this.PageCount -= pages;
            this.InkCount -= ink;

            this.inkCountProgressBar.Value = InkCount;
            this.pageCountSlider.Value = PageCount;

            float percentInk = ((float)InkCount / (float)MAX_INK) * 100;

            if (this.PageCount < 0)
            {
                this.label_pages.Foreground = Brushes.Red;
                PageMissing(this, 
                    new PrinterEventArgs(true, " To finish print task, needed " + (-1 * this.PageCount) + " pages in ", this.PrinterName));
            }
            else if ((percentInk > 10) && (percentInk <=15))
            {
                this.label_Ink.Foreground = Brushes.Yellow;
                PageMissing(this,
                    new PrinterEventArgs(false, " Low ink level, only: " + percentInk + "% in ", this.PrinterName));
            }
            else if ((percentInk > 1) && (percentInk <= 10))
            {
                this.label_Ink.Foreground = Brushes.Orange;
                PageMissing(this,
                    new PrinterEventArgs(false, " Low ink level, only: " + percentInk + "% in ", this.PrinterName));
            }
            else if (percentInk <= 1)
            {
                this.label_Ink.Foreground = Brushes.Red;
                PageMissing(this,
                    new PrinterEventArgs(true, " No ink in ", this.PrinterName));
            }

            if (PageCount < 0 || percentInk < 1)
            {
                this.InkCount = rnd.Next(MIN_ADD_INK, MAX_INK);
                this.inkCountProgressBar.Value = InkCount;
                this.label_Ink.Foreground = Brushes.Black;


                this.PageCount = rnd.Next(MIN_ADD_PAGES, MAX_PAGES);
                this.pageCountSlider.Value = PageCount;
                this.label_pages.Foreground = Brushes.Black;
            }

            return;
        }

        private void AddInk()
        {
            int ink = rnd.Next(MIN_ADD_INK, MAX_INK);
            this.PageCount += ink;
            return;
        }

        private void AddPages()
        {
            int pages = rnd.Next(MIN_ADD_PAGES, MAX_PAGES);
            this.PageCount += pages;
            return;
        }
        #endregion

        #region bonus Mouse and ToolTip
        private void label_pName_MouseEnter(object sender, MouseEventArgs e)
        {
            label_pName.FontSize = 40;
            return;
        }

        private void label_pName_MouseLeave(object sender, MouseEventArgs e)
        {
            label_pName.FontSize = 20;
            return;
        }

        private void pageCountSlider_MouseEnter(object sender, MouseEventArgs e)
        {
            pageCountSlider.ToolTip = PageCount.ToString();
            return;
        }
        #endregion

        private void inkCountProgressBar_MouseEnter(object sender, MouseEventArgs e)
        {
            inkCountProgressBar.ToolTip = (((float)InkCount / (float)MAX_INK) * 100) + "%";
            return;
        }
    }
}
