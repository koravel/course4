public class GlobalData
{
    public static User user;
    public static string levelName = "";
    public static string prefabMelee
    {
        get
        {
            return "Prefabs\\Units\\EnemyMelee";
        }
    }
    public static string prefabRange
    {
        get
        {
            return "Prefabs\\Units\\EnemyRange";
        }
    }
    public static string prefabPlayer
    {
        get
        {
            return "Prefabs\\Units\\Player";
        }
    }
    public static string prefabWall
    {
        get
        {
            return "Prefabs\\Units\\Wall";
        }
    }
    public static string prefabBulletLazer
    {
        get
        {
            return "Prefabs\\Units\\BulletLazer";
        }
    }
    public static string textureGun
    {
        get
        {
            return "Textures\\gun64";
        }
    }
    public static string soundGun
    {
        get
        {
            return "Sounds\\weapon_enemy";
        }
    }
    public static string materialGun
    {
        get
        {
            return "PhysicsMaterials\\EnemyBounce";
        }
    }
    public static string usersDir
    {
        get
        {
            return "Users";
        }
    }
    public static string usersListFileName
    {
        get
        {
            return "users.xml";
        }
    }
    public static string dataDir
    {
        get
        {
            return "Data";
        }
    }
    public static string levelsDir
    {
        get
        {
            return "Levels";
        }
    }
    public static string font
    {
        get
        {
            return "Fonts\\JazzCreateBubble";
        }
    }
}
