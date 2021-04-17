using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.PropertyGridInternal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace cat_and_mouse.Domain
{
    public class GameForm : Form
    {
        public GameForm()
        {
            var levels = LoadLevels();
        }
        private static IEnumerable<Map> LoadLevels()
        {
            const string pathForLevel1 = @"C:\Users\warsd\RiderProjects\cat-and-mouse\cat and mouse\Levels\Level1.txt";
            var text = new StreamReader(pathForLevel1);
            yield return Map.FromText(text.ReadToEnd());
            
        }
    }
}