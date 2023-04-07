using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ConsoleApp_Code39
{
    internal class Barcode : ICodeBarcode
    {
        private string _text;
        public string Text 
        { 
            get => _text; 
            set => throw new NotImplementedException(); 
        }
        private Bitmap _image;
        public Bitmap Image 
        { 
            get => _image; 
            set => throw new NotImplementedException(); 
        }

        public Barcode(string textTocode)
        {
            Text = textTocode;
            Image = GetBarcode(Text);
        }
        public Barcode(Bitmap imageToDecode) 
        {
            Image = imageToDecode;
            Text = GetText(Image);
        }

        public Bitmap GetBarcode(string textToCode)
        {
            throw new NotImplementedException();
        }

        public string GetText(Bitmap barcodeToDecode)
        {
            throw new NotImplementedException();
        }
    }
}
