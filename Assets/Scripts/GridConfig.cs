using UnityEngine;
using UnityEngine.UIElements;
using System;

[CreateAssetMenuAttribute(fileName = "New GridConfig", menuName = "Grid Config", order = 2)]
public class GridConfig : ScriptableObject
{
    public int width, height, cellsize, margin;

    public Vector3 GetCorrection()
    {
        return GetHalfGridSize()
            - Vector3.one * cellsize
            + ( new Vector3(width-1, height-1, 0) * margin /2f );
    }

    public Vector3 GetHalfGridSize()
    {
        return new Vector3(width+1, height+1, 0) * cellsize /2f;
    }

    public int GetCellCount()
    {
        return width * height;
    }
}
