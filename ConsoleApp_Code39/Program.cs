using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ConsoleApp_Code39
{
    class Program
    {
        static void Main(string[] args)
        {
            Barcode barcode = new BarcodeType39("I am barcode".ToUpper());
            Bitmap buf = barcode.Image;
            buf.Save("d:/qqqqq.png", System.Drawing.Imaging.ImageFormat.Png);
            Barcode barcode1 = new BarcodeType39(barcode.Image);
            Console.WriteLine(barcode1.Text);
            Console.ReadKey();
        }
    }
        
}
