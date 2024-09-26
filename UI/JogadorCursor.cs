using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace UI
{
    //Class para trocar o cursor dependendo do jogador
    public static class JogadorCursor
    {
        
        public static readonly Cursor CursorW = LoadCursor("Images/CursorW.cur");

        public static readonly Cursor CursorB = LoadCursor("Images/CursorB.cur");


        private static Cursor LoadCursor(string filePath)
        {
            Stream stream = Application.GetResourceStream(new Uri(filePath, UriKind.Relative)).Stream;
            return new Cursor(stream, true);
        }
    }
}
