using System.Collections.Generic;

class StringComparer : IComparer<string>
{
    public int Compare(string x, string y)
    {
        return string.Compare(x, y);
    }
}
