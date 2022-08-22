using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapLoader))]
public class MapLoaderCustomInspector : Editor
{
     public override void OnInspectorGUI()
    {

        //Initialise
        MapLoader mapLoader = (MapLoader)target;

      if (GUILayout.Button("Reset/Initialise"))
        {
            mapLoader.Initialise();
        

        }


DrawDefaultInspector();


    }

}
