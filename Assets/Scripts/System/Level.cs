using System.Collections;
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
                return LevelName;
            }
            set
            {
                levelName = value;
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