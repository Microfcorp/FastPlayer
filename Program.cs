﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FastPlayer
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if(args.Length < 1)
                Application.Run(new Controller());
            else
                Application.Run(new Controller(args));
        }
    }
}
