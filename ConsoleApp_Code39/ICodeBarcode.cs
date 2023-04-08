using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ConsoleApp_Code39
{
    internal interface ICodeBarcode // all barcodes must provide text and image
    {
        string Text { get; set; }
        Bitmap Image { get; set; }
    }
}
