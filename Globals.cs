using NovaBrush._02_BrushSys;
using NovaBrush._02_BrushSys.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace _NovaBrush{
    public class Globals {
        public static TextBlock MousePositionLabel { get; set; }
        public static TextBlock BrushSizeLabel { get; set; }
        public static TextBlock AntiAliasingLabel { get; set; }
        public static Canvas CanvasPanelLabel { get; set; }
        public static WriteableBitmap BitmapToDraw { get; set; }
        public static ITool CurrentTool { get; set; }

        public static Color curColor { get; set; }

        public static readonly Dictionary<string, ITool> Tools = new()
        {
            { "Pencil", new bsh_pencil() },
            { "Eraser", new bsh_eraser() },
            { "Brush", new bsh_brush() },
        };

        public static void InitializeTools()
        {
            CurrentTool = Tools["Pencil"];
            CurrentTool = Tools["Eraser"];
            CurrentTool = Tools["Brush"];
        }

        public static void Initialize(MainWindow mainWindow){
            MousePositionLabel = mainWindow.lbl_MousePos;
            CanvasPanelLabel = mainWindow.MainCanvas;
            BrushSizeLabel = mainWindow.lbl_brushsize;
            AntiAliasingLabel = mainWindow.lbl_aa;
            Globals.curColor = Color.FromArgb(12, 0, 0, 0);
        }
    }
}
