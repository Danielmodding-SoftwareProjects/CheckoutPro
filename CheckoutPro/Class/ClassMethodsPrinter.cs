﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace CheckoutPro.Class
{
    internal class ClassMethodsPrinter
    {
        public static string Anzahl;
        public static string Produkt;
        public static string Preis;
        public static bool PrintPriceonLabel;
        public static bool Print1xonLabel;

        public void Print(string lAnzahl, string lProdukt, string lPreis, bool lPrintPriceonLabel, bool lPrintSeperatLabels, bool lprint1x)
        {
            Print1xonLabel = lprint1x;


            int anzahl;
            string numberPart = Regex.Replace(lAnzahl, @"[^\d]", ""); // Alle nicht-numerischen Zeichen entfernen
            if (!int.TryParse(numberPart, out anzahl) || anzahl <= 0)
            {
                throw new ArgumentException("Die Anzahl muss eine gültige positive Ganzzahl sein.");
            }

            

            if (!lPrintSeperatLabels)
            {
                Anzahl = "1x";
                Produkt = lProdukt;
                Preis = lPreis;
                PrintPriceonLabel = lPrintPriceonLabel;

                for (int i = 0; i < anzahl; i++)
                {
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += new PrintPageEventHandler(PrintBon);
                    pd.Print();
                }
            }
            else
            {

                Anzahl = lAnzahl;
                Produkt = lProdukt;
                Preis = lPreis;
                PrintPriceonLabel = lPrintPriceonLabel;

                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(PrintBon);
                pd.Print();
            }
        }

        private static void PrintBon(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            float pageWidth = e.PageSettings.PrintableArea.Width;
            float pageHeight = e.PageSettings.PrintableArea.Height;

            Font font = new Font("Consolas", 12);
            Font largeFont = new Font("Consolas", 16);
            Font boldFont = new Font("Consolas", 18, FontStyle.Bold);

            SizeF anzahlSize = g.MeasureString(Anzahl, boldFont);
            SizeF produktSize = g.MeasureString(Produkt, largeFont);
            SizeF preisSize = g.MeasureString(Preis, boldFont);

            float anzahlX = (pageWidth - anzahlSize.Width) / 2;
            float produktX = (pageWidth - produktSize.Width) / 2;
            float preisX = (pageWidth - preisSize.Width) / 2;

            float lineHeight = font.GetHeight();

            float totalHeight = produktSize.Height + preisSize.Height + 2 * lineHeight;

            float startY = (pageHeight - totalHeight) * 0.05f;

            if (Print1xonLabel || Anzahl != "1x")
            {
                g.DrawString(Anzahl, boldFont, System.Drawing.Brushes.Black, anzahlX, startY);
                g.DrawString(Produkt, largeFont, System.Drawing.Brushes.Black, produktX, startY + anzahlSize.Height + lineHeight);
            }
            else
            {
                g.DrawString(Produkt, largeFont, System.Drawing.Brushes.Black, produktX, startY + (Anzahl != "1x" ? anzahlSize.Height + lineHeight : 0));
            }

            if (PrintPriceonLabel)
            {
                g.DrawString(Preis, boldFont, System.Drawing.Brushes.Black, preisX, startY + (Anzahl != "1x" ? anzahlSize.Height : 0) + produktSize.Height + 2 * lineHeight);
            }

            string currentDateAndTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            SizeF dateAndTimeSize = g.MeasureString(currentDateAndTime, font);
            float dateAndTimeX = (pageWidth - dateAndTimeSize.Width) / 2;
            g.DrawString(currentDateAndTime, font, System.Drawing.Brushes.Black, dateAndTimeX, startY + (Anzahl != "1x" ? anzahlSize.Height : 0) + produktSize.Height + preisSize.Height + 3 * lineHeight);

        }


    }
}
