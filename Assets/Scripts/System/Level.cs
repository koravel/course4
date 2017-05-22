using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        public void Load()
        {
            SceneManager.LoadScene(levelName);
        }

        public void UserLoad()
        {

        }

        public void Close()
        {

        }

        public void Save()
        {

        }
    }

}