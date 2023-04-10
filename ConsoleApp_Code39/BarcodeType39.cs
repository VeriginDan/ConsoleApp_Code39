using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Security;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp_Code39
{
    internal class BarcodeType39 : Barcode
    {
        static readonly Dictionary<Char, String> CODE39TABLE = new Dictionary<char, string>()
        {//заполнение кодовой таблицы по двоичному принципу. Широкий штрих = 3 тонких, 1 - черный, 0 - белый
            {'1', "111010001010111"},// digits
            {'2', "101110001010111"},
            {'3', "111011100010101"},
            {'4', "101000111010111"},
            {'5', "111010001110101"},
            {'6', "101110001110101"},
            {'7', "101000101110111"},
            {'8', "111010001011101"},
            {'9', "101110001011101"},
            {'0', "101000111011101"},
            {'A', "111010100010111"}, //alphabet. Code39 works with capital letters
            {'B', "101110100010111"},
            {'C', "111011101000101"},
            {'D', "101011100010111"},
            {'E', "111010111000101"},
            {'F', "101110111000101"},
            {'G', "101010001110111"},
            {'H', "111010100011101"},
            {'I', "101110100011101"},
            {'J', "101011100011101"},
            {'K', "111010101000111"},
            {'L', "101110101000111"},
            {'M', "111011101010001"},
            {'N', "101011101000111"},
            {'O', "111010111010001"},
            {'P', "101110111010001"},
            {'Q', "101010111000111"},
            {'R', "111010101110001"},
            {'S', "101110101110001"},
            {'T', "101011101110001"},
            {'U', "111000101010111"},
            {'V', "100011101010111"},
            {'W', "111000111010101"},
            {'X', "100010111010111"},
            {'Y', "111000101110101"},
            {'Z', "100011101110101"},
            {' ', "100011101011101"}, //special symbols
            {'-', "100010101110111"},
            {'$', "100010001000101"},
            {'%', "101000100010001"},
            {'.', "111000101011101"},
            {'/', "100010001010001"},
            {'+', "100010100010001"},
            {'*', "100010111011101" } // start end symbols
        };
        static readonly Dictionary<String, Char> DECODE39TABLE = new Dictionary<string, char>()
        {//заполнение кодовой таблицы по двоичному принципу. Широкий штрих = 3 тонких, 1 - черный, 0 - белый
            {"111010001010111", '1'},// digits
            {"101110001010111", '2'},
            {"111011100010101", '3'},
            { "101000111010111", '4' },
            { "111010001110101", '5' },
            { "101110001110101", '6' },
            { "101000101110111", '7' },
            { "111010001011101", '8' },
            { "101110001011101", '9' },
            { "101000111011101", '0' },
            { "111010100010111", 'A' }, //alphabet. Code39 works with capital letters
            {"101110100010111" , 'B'},
            {"111011101000101" , 'C'},
            {"101011100010111" , 'D'},
            {"111010111000101" , 'E'},
            {"101110111000101" , 'F'},
            {"101010001110111" , 'G'},   
            {"111010100011101" , 'H'},
            {"101110100011101" , 'I'},
            {"101011100011101" , 'J'},
            {"111010101000111" , 'K'},
            {"101110101000111" , 'L'},
            {"111011101010001" , 'M'},
            {"101011101000111" , 'N'},
            {"111010111010001" , 'O'},
            {"101110111010001" , 'P'},
            {"101010111000111" , 'Q'},
            {"111010101110001" , 'R'},
            {"101110101110001" , 'S'},
            {"101011101110001" , 'T'},
            {"111000101010111" , 'U'},
            {"100011101010111" , 'V'},
            {"111000111010101" , 'W'},
            {"100010111010111" , 'X'},
            {"111000101110101" , 'Y'},
            {"100011101110101" , 'Z'},
            {"100011101011101" , ' '}, //special symbols
            {"100010101110111" , '-'},
            {"100010001000101" , '$'},
            {"101000100010001" , '%'},
            {"111000101011101" , '.'},
            {"100010001010001" , '/'},
            {"100010100010001" , '+'},
            {"100010111011101" , '*' } // start end symbols
        };
        public BarcodeType39(string textToCode) : base(textToCode)
        {
        }

        public BarcodeType39(Bitmap imageToDecode) : base(imageToDecode)
        {
        }

        override public string Text
        {
            get => _text;
            set => _text = isValidText(value.ToUpper()) ? value.ToUpper() : "";//Console.WriteLine("Text is not supported to make barcode");
            // check possibility to code text in barcode. Need to implement Event of Exception here
        }

        protected override Bitmap GetImage(string textToCode)
        {
            // encoding to 01 string
            string encodedString = CODE39TABLE['*'];
            foreach (char item in textToCode) encodedString += '0' + CODE39TABLE[item]; // add zero to convert from int code of char to char
            encodedString += '0' + CODE39TABLE['*'];
            
            //задание размеров штриха
            int lineWidth = 2; // need revision to dependence on dpi of output device
            // расчет ширины штриха
            int stringWidth = textToCode.Length;
            int gcodeWidth = (stringWidth + 2) * lineWidth * (3 * 3 + 6) + (stringWidth + 1) * lineWidth + 2 * 10 * lineWidth; // ширина кода + межзнак + свободное пространство
            // расчет высоты кода
            int gcodeHeight = gcodeWidth * 15 / 100;
            
            // создание графика
            Graphics gcode;
            Bitmap buf;
            buf = new Bitmap(gcodeWidth, gcodeHeight);
            gcode = Graphics.FromImage(buf);
            // рисование
            gcode.Clear(Color.White);
            Pen blackPen = new Pen(Color.Black, lineWidth);
            // стартовая точка
            int pointX = 10 * lineWidth + 1;
            int pointY = 0;
            // отрисовка линий
            foreach (char item in encodedString)
            {
                if (item == '1')
                {
                    gcode.DrawLine(blackPen, pointX, pointY, pointX, pointY + gcodeHeight);
                }
                pointX += lineWidth;
                pointY = 0;
            }
            return buf;
        }

        protected override string GetText(Bitmap barcodeToDecode) // simple realization to decode barcodes generated by this programm
        {
            int lineWidth = 2; // in this realization should be the same as in getImage
            int pointX = 0;
            int pointY = barcodeToDecode.Height / 2;
            //while (barcodeToDecode.GetPixel(pointX, pointY).ToArgb() != Color.Black.ToArgb() && pointX < barcodeToDecode.Width) pointX++; //search first line
            
            List <char> decodedString = new List<char>();
            while (pointX < barcodeToDecode.Width)
            {
                while (barcodeToDecode.GetPixel(pointX, pointY).ToArgb() != Color.Black.ToArgb() && pointX < barcodeToDecode.Width) pointX++; //search first line
                if (DECODE39TABLE.TryGetValue(
                                                GetSymbolCoded(barcodeToDecode, ref pointX, ref pointY, lineWidth), 
                                                out char value))
                    decodedString.Add(value);
                if (decodedString.FindAll(x => x == '*').Count == 2) break;// if we have both start and stop symbols 
            }

            decodedString.RemoveAt(decodedString.LastIndexOf('*'));
            decodedString.RemoveAt(decodedString.IndexOf('*'));
            return new String(decodedString.ToArray());
        }
        private string GetSymbolCoded(Bitmap barcodeToDecode, ref int pointX, ref int pointY, int lineWidth)//method to read series of lines
        {
            char[] symbolCoded = new char[15];
            for (int iter = 0; iter < 15; iter++)
            {
                symbolCoded[iter] = (barcodeToDecode.GetPixel(pointX, pointY).ToArgb() == Color.Black.ToArgb()) ? '1' : '0';
                pointX += lineWidth;
            }
            pointX++; //put in next position of coded symbol in barcode
            return new String(symbolCoded);    
        }
        protected override bool isValidImage(Bitmap imageToCheck) // there should be check that image contain barcode. For easy way we assume we get ideal scan of barcode
        {
            return imageToCheck is Bitmap;
        }

        protected override bool isValidText(string textToCheck)
        {
            string tempString = textToCheck.ToUpper();
            foreach (char item in tempString) 
                if (!CODE39TABLE.ContainsKey(item)) return false; // if text do not contains symbols from dictionary we cannot code it
            return true;
        }
    }
}
