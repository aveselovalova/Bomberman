using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GetRoundPosition
{
    public static Point GetPoint(Vector3 position)
    {
        return new Point((int)Mathf.Round(position.x), (int)Mathf.Round(position.z));
    }
    public static Point GetRoundPointFromPoint(Point position)
    {
        return new Point((int)Mathf.Round(position.X), (int)Mathf.Round(position.Y));
    }
    public static Vector3 RoundXZCoordinate(Vector3 vector)
    {
        return new Vector3(Mathf.Round(vector.x), vector.y, Mathf.Round(vector.z));
    }
}