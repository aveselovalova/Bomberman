using UnityEngine;

public static class ResourceLoader
{
    public static GameObject LoadItem(string path)
    {
        return Resources.Load(path, typeof(GameObject)) as GameObject;
    }
}
