using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ConsoleApp_Code39
{
    abstract internal class Barcode : ICodeBarcode
    {
        
        private string _text;
        public string Text 
        { 
            get => _text;
            set => _text = isText(value) ? value : "";//Console.WriteLine("Text is not supported to make barcode");
            // check possibility to code text in barcode. Need to implement Event of Exception here
        }
        
        abstract protected bool isText(string textToCheck); // realization may be different for different code systems
        
        private Bitmap _image;
        public Bitmap Image 
        { 
            get => _image;
            set => _image = isImage(value) ? value : null; //Console.WriteLine("File is not supported to decode");
            // check possibility to decode text in barcode. Need to implement Event of Exception here
            // need revision here - where and how to check
        }
        abstract protected bool isImage(Bitmap textToCheck); // realization may be different for different code systems
        public Barcode(string textToCode) //We make new object of class based on text and simultaneously obtain barcode from text
        {
            Text = textToCode;
            Image = GetImage(Text);
        }
        public Barcode(Bitmap imageToDecode) //We make new object of class based on text and simultaneously obtain barcode from text
        {
            Image = imageToDecode;
            Text = GetText(Image);
        }
        protected abstract Bitmap GetImage(string textToCode);// realization may be different for different code systems
        protected abstract string GetText(Bitmap barcodeToDecode);   // realization may be different for different code systems   
    }
}
