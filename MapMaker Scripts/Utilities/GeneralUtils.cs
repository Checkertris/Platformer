using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomUtils
{

public static class GeneralUtils 
{

     public static void ClearChildren(this GameObject obj)
    {

        foreach (Transform child in obj.transform) {
            
                GameObject.Destroy(child.gameObject);

        }
        
    }
}

}