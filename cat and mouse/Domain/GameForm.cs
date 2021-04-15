﻿using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.PropertyGridInternal;

namespace cat_and_mouse.Domain
{
    public class GameForm : Form
    {
        private static IEnumerable<Map> LoadLevels()
        {
            yield return Map.FromText(Properties.Resources.Level1);
            //yield return Map.FromText(Properties.Resources.Level2);
            //yield return Map.FromText(Properties.Resources.Level3);
        }
    }
}