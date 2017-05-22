using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilites.Level
{
    public class Level : MonoBehaviour
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

        private List<GameObject> levelObjects;
        public List<GameObject> LevelObjects
        {
            get
            {
                return LevelObjects;
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