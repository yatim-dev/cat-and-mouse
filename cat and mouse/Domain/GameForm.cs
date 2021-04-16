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
            LoadLevels();
        }
        private static void LoadLevels()
        {
           // var file = new StreamReader("Levels\\Level1.txt");//
            DirectoryInfo list;
           // yield return Map.FromText(file.ReadToEnd());
           var levelsDirectory = new DirectoryInfo("Levels");
            foreach (var e in levelsDirectory.GetFiles("*.txt"))
                list = levelsDirectory;
            var file = "";
            //yield return Map.FromText(Properties.Resources.Level2);
            //yield return Map.FromText(Properties.Resources.Level3);


        }
    }
}