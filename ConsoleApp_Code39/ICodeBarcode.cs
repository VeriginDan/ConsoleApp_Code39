using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ConsoleApp_Code39
{
    internal interface ICodeBarcode
    {
        string Text { get; set; }
        Bitmap Image { get; set; }
        Bitmap GetBarcode(string textToCode);
        string GetText(Bitmap barcodeToDecode);
    }
}
