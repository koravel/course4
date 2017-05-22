using System.Collections.Generic;
using UnityEngine;

namespace Utilites.Level
{
    public class Level
    {
        private string levelName;
        public string LevelName
        {
            get
            {
                return levelName;
            }
            set
            {
                levelName = value;
            }
        }

        private List<GameObject> levelObjects;
        public List<GameObject> LevelObjects
        {
            get
            {
                return levelObjects;
            }
            set
            {
                levelObjects = value;
            }
        }
    }

}