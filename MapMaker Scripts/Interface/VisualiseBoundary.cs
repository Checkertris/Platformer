using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomUtils;

public class VisualiseBoundary : MonoBehaviour
{

    public void OnDrawGizmosSelected()
    {
        MapMaker mapMaker = (MapMaker)FindObjectOfType(typeof(MapMaker));

        mapMaker.GridSizeClamp();

        float mapRange = mapMaker.gridSize[mapMaker.levelIndex];
        Vector3[] vert = new Vector3[4];
        vert = vert.GenSquare(mapRange);
        
        Gizmos.color = Color.green;

        Gizmos.DrawLine(vert[0], vert[1]);
        Gizmos.DrawLine(vert[1], vert[2]);
        Gizmos.DrawLine(vert[2], vert[3]);
        Gizmos.DrawLine(vert[3], vert[0]);
    }
}
