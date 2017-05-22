using System.Collections.Generic;
using System;

public class User
{
    private string name;
    private List<bool> levelsAccess;
    private List<int> levelsScore;

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }
    public List<bool> LevelsAccess
    {
        get
        {
            return levelsAccess;
        }
        set
        {
            levelsAccess = value;
        }
    }
    public List<int> LevelsScore
    {
        get
        {
            return levelsScore;
        }
        set
        {
            levelsScore = value;
        }
    }
}
