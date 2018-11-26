using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetWPF_03_7448
{
    public class PrinterEventArgs: EventArgs
    {
        // fields
        public bool isCritical;
        public DateTime date;
        public string message;
        public string name;

        //ctor
        public PrinterEventArgs(bool isCritical, string message, string name)
        {
            this.isCritical = isCritical;
            this.date = DateTime.Now;
            this.message = message;
            this.name = name;
        }
    }
}
