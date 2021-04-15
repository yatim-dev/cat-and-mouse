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
            var huy = LoadLevels();
        }
        private static IEnumerable<Map> LoadLevels()
        {
            var file = new StreamReader("Levels\\Level1.txt");//
            yield return Map.FromText(file.ReadToEnd());
            //yield return Map.FromText(Properties.Resources.Level2);
            //yield return Map.FromText(Properties.Resources.Level3);
            
            
        }
    }
}