using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapMaker))]
public class MapMakerCustomInspector : Editor
{

    public override void OnInspectorGUI()
    {

        //Initialise
        MapMaker mapMaker = (MapMaker)target;
        GUIStyle selected = new GUIStyle(EditorStyles.textField);


        //Interface

         if (GUILayout.Button("Reset/Initialise"))
        {
            mapMaker.Initialise();
        

        }


       // DrawDefaultInspector();

        EditorGUILayout.Space();
        for (int i = 0; i < mapMaker.levelCount; i++)
        {

              EditorGUILayout.BeginHorizontal();

                            EditorGUILayout.LabelField("LEVEL " + i,EditorStyles.boldLabel);

    
                if (GUILayout.Button("New Grid"))
        {
            mapMaker.AddGridToLevel(i);

        }
        if (mapMaker.levelCount>1)
        {
            if (GUILayout.Button("Delete"))
        {
            mapMaker.RemoveLevel(i);
        }

        }

        EditorGUILayout.EndHorizontal();


               EditorGUILayout.BeginHorizontal();
            
             EditorGUILayout.LabelField("Grid Size");

             mapMaker.gridSize[i] = EditorGUILayout.IntSlider(mapMaker.gridSize[i],2,50);

               if(mapMaker.gridSize[i]%2==1)
                 {
               }

                   EditorGUILayout.EndHorizontal();
                   

        

                    for (int j = 0; j < mapMaker.gridCount[i]; j++)
        {
                   EditorGUILayout.BeginHorizontal();

                   if (i == mapMaker.levelIndex && j == mapMaker.gridIndex)
                   {
                       EditorGUILayout.LabelField("Grid "+j, selected);
                   }

                   else
                   {
                   EditorGUILayout.LabelField("Grid "+j);
                   }
                     


     if (GUILayout.Button("Select"))
        {            
            mapMaker.SaveMap(mapMaker.levelIndex,mapMaker.gridIndex);

            mapMaker.levelIndex = i;
            mapMaker.gridIndex = j;
           
            mapMaker.ClearMap();
            mapMaker.LoadMap(i,j);

 

        }

      if (mapMaker.gridCount[i]>1)
      {
        if (GUILayout.Button("Delete"))
        {
      mapMaker.RemoveGrid(i,j);

        }

      }
  
                EditorGUILayout.EndHorizontal();

        }

            EditorGUILayout.Space(30);
  
        }
 

        if (GUILayout.Button("New Level"))
        {
            mapMaker.AddLevel();

        }



        


    }
    
}
