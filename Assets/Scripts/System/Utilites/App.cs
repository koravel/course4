using System.Collections.Generic;
using System.IO;
using Utilites.Serialiazer;

public class App
{
    public static void UsersSave()
    {
        if (GlobalData.user != null)
        {
            if(GlobalData.user.Name != "")
            {
                List<User> usersToSave = new List<User>();
                string path = GlobalData.dataDir + "\\" + GlobalData.usersListFileName;
                Serialiazer.DeserialiazitionFromXml(ref usersToSave, path);
                File.Delete(path);
                usersToSave.Remove(usersToSave.Find(u => u.Name == GlobalData.user.Name));
                usersToSave.Add(GlobalData.user);
                Serialiazer.SerialiazeToXml(ref usersToSave, path);
            }
        }
    }
}
