using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomUtils;

namespace CustomUtils
{

public static class GeometryUtils
{

    public static Vector3[] GenPrisim(this Vector3[] surface, float height)
    {
        int len = surface.Length;

          for (int i = 0; i < len; i++)
        {
            Vector3 v = surface[i] + new Vector3(0,-height,0);
            surface = surface.Add<Vector3>(v);
        }

        return surface;
    }

        public static Vector3[] GenSquare(this Vector3[] array, float size)
    {
        float len = size * 0.5f;
        Vector3[] verts = new Vector3[4];
        verts[0] = new Vector3(len, 0, len);
        verts[1] = new Vector3(len, 0, -len);
        verts[2] = new Vector3(-len, 0, -len);
        verts[3] = new Vector3(-len, 0, len);

        return verts;

    }


    public static Vector3[] Displace(this Vector3[] array, Vector3 offset)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] += offset;
        }

        return array;
    }

  
}
}