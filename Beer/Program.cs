using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ANN ann = new ANN(5, 2, 1, new int[] { 2 }, null, true);

            List<double> res = ann.Run(new double[] { 1, 0.5, 0.2, 0.1, 0.5 });

            foreach (double d in res)
                Console.WriteLine("res: " + d);
            List<double> res1 = ann.Run(new double[] { 1, 0.5, 0.2, 0.1, 0.5 });

            foreach (double d in res1)
                Console.WriteLine("res1: " + d);


            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());*/
        }
    }
}
