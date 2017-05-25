using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{

    public Point Position { get; set; }
    public int PathLengthFromStart { get; set; }
    public PathNode NodeCameFrom { get; set; }
    public int HeuristicEstimatePathLength { get; set; }
    public int TotalEstimatePathLength
    {
        get
        {
            return this.PathLengthFromStart + this.HeuristicEstimatePathLength;
        }
    }

    public PathNode(Point position, int pathLengthFromStart, PathNode nodeCameFrom, int heuristicEstimatePathLength)
    {
        Position = position;
        NodeCameFrom = nodeCameFrom;
        PathLengthFromStart = pathLengthFromStart;
        HeuristicEstimatePathLength = heuristicEstimatePathLength;
    }
}

