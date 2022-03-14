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
            //создание кодовой таблицы
            Dictionary<Char, String> Code39Table = new Dictionary<char, string>();
            // заполнение кодовой таблицы по двоичному принципу. Широкий штрих = 3 тонких, 1 - черный, 0 - белый
            //digits
            Code39Table.Add('1', "111010001010111");
            Code39Table.Add('2', "101110001010111");
            Code39Table.Add('3', "111011100010101");
            Code39Table.Add('4', "101000111010111");
            Code39Table.Add('5', "111010001110101");
            Code39Table.Add('6', "101110001110101");
            Code39Table.Add('7', "101000101110111");
            Code39Table.Add('8', "111010001011101");
            Code39Table.Add('9', "101110001011101");
            Code39Table.Add('0', "101000111011101");
            // alphabet
            Code39Table.Add('A', "111010100010111");
            Code39Table.Add('B', "101110100010111");
            Code39Table.Add('C', "111011101000101");
            Code39Table.Add('D', "101011100010111");
            Code39Table.Add('E', "111010111000101");
            Code39Table.Add('F', "101110111000101");
            Code39Table.Add('G', "101010001110111");
            Code39Table.Add('H', "111010100011101");
            Code39Table.Add('I', "101110100011101");
            Code39Table.Add('J', "101011100011101");
            Code39Table.Add('K', "111010101000111");
            Code39Table.Add('L', "101110101000111");
            Code39Table.Add('M', "111011101010001");
            Code39Table.Add('N', "101011101000111");
            Code39Table.Add('O', "111010111010001");
            Code39Table.Add('P', "101110111010001");
            Code39Table.Add('Q', "101010111000111");
            Code39Table.Add('R', "111010101110001");
            Code39Table.Add('S', "101110101110001");
            Code39Table.Add('T', "101011101110001");
            Code39Table.Add('U', "111000101010111");
            Code39Table.Add('V', "100011101010111");
            Code39Table.Add('W', "111000111010101");
            Code39Table.Add('X', "100010111010111");
            Code39Table.Add('Y', "111000101110101");
            Code39Table.Add('Z', "100011101110101");
            //специальные символы
            Code39Table.Add(' ', "100011101011101");
            Code39Table.Add('-', "100010101110111");
            Code39Table.Add('$', "100010001000101");
            Code39Table.Add('%', "101000100010001");
            Code39Table.Add('.', "111000101011101");
            Code39Table.Add('/', "100010001010001");
            Code39Table.Add('+', "100010100010001");
            // start end symbols
            Code39Table.Add('*', "100010111011101");

            String CodeString = Console.ReadLine().ToUpper();
            
            // encoding to 01 string
            string EncodedString = Code39Table['*'];
            
            foreach (char item in CodeString)
            {
               EncodedString += '0' + Code39Table[item];
            }

            EncodedString += '0' + Code39Table['*'];

            //задание размеров штирх кода
            int LineWidth = 2;
            // расчет ширины кода
            int StringWidth = CodeString.Length;
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

            buf.Save("qqqqqqqq.png", ImageFormat.Png);
        }
    }
        
}
