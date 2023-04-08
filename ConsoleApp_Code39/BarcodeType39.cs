﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace ConsoleApp_Code39
{
    internal class BarcodeType39 : Barcode
    {
        static readonly Dictionary<Char, String> Code39Table = new Dictionary<char, string>()
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
        public BarcodeType39(string textToCode) : base(textToCode)
        {
        }

        public BarcodeType39(Bitmap imageToDecode) : base(imageToDecode)
        {
        }

        protected override Bitmap GetBarcode(string textToCode)
        {
            // encoding to 01 string
            textToCode.ToUpper();
            string EncodedString = Code39Table['*'];
            foreach (char item in textToCode) EncodedString += '0' + Code39Table[item]; // add zero as space between encoded symbols
            EncodedString += '0' + Code39Table['*'];
            
            //задание размеров штриха
            int LineWidth = 2;
            // расчет ширины штриха
            int StringWidth = textToCode.Length;
            int gcodeWidth = (StringWidth + 2) * LineWidth * (3 * 3 + 6) + (StringWidth + 1) * LineWidth + 2 * 10 * LineWidth; // ширина кода + межзнак + свободное пространство
            // расчет высоты кода
            int gcodeHeight = gcodeWidth * 15 / 100;
            
            // создание графика
            Graphics gcode;
            Bitmap buf;
            buf = new Bitmap(gcodeWidth, gcodeHeight);
            gcode = Graphics.FromImage(buf);
            // рисование
            gcode.Clear(Color.White);
            Pen blackPen = new Pen(Color.Black, LineWidth);
            // стартовая точка
            int pointx = 10 * LineWidth + 1;
            int pointy = 0;
            // отрисовка линий
            foreach (char item in EncodedString)
            {
                if (item == '1')
                {
                    gcode.DrawLine(blackPen, pointx, pointy, pointx, pointy + gcodeHeight);
                }
                pointx += LineWidth;
                pointy = 0;
            }
            return buf;
        }

        protected override string GetText(Bitmap barcodeToDecode)
        {
            throw new NotImplementedException();
        }

        protected override bool isImage(Bitmap textToCheck)
        {
            throw new NotImplementedException();
        }

        protected override bool isText(string textToCheck)
        {
            textToCheck.ToUpper();
            foreach (char item in textToCheck) 
                if (!Code39Table.ContainsKey(item)) return false; // if text do not contains symbols from dictionary we cannot code it
            return true;
        }
    }
}
