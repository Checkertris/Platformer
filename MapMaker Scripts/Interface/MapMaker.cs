using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using Wrenji.Assets.Tools.Scripts;
using UnityEditor;
using UnityEngine.TextCore.LowLevel;
using System.Collections;
using System.Collections.Generic;
using CustomUtils;

//CURRENT ISSUES:
// multiple layers for enemy, structure where certain prefabs are disabled for certain layers
// MUST reorder layers by x and z position
// some way to save data as file 
// remove tiles out of range and fill empty tiles with floor (for structure layer), probably in the save data file method
//transfer information

[ExecuteInEditMode]
public class MapMaker : MonoBehaviour
{ 
    public int levelIndex;
    public int gridIndex;
    public int levelCount;
    public int[] gridCount;
    public int[] gridSize;

    //x = level index, y = grid index
    public LevelData[] savedLevels;

    void Update()
    {

        if (!Application.isPlaying)
        {
                   SaveMap(levelIndex, gridIndex);
                // PrintSavedLevels();

            

        }

       
    }

    //INTERFACE
    public void AddLevel()
    {
        levelCount += 1;
       gridCount = gridCount.Add<int>(1);
       gridSize = gridCount.Add<int>(2);
       savedLevels = savedLevels.Add<LevelData>();
    }

    public void RemoveLevel(int index)
    {
        levelCount -= 1;
      gridCount = gridCount.IndexRemove<int>(index);
      gridSize = gridSize.IndexRemove<int>(index);
      savedLevels = savedLevels.IndexRemove<LevelData>(index);

    }

    public void AddGridToLevel(int index)
    {
        gridCount[index] += 1;
        savedLevels[index].grids = savedLevels[index].grids.Add<GridData>();
    }

    public void RemoveGrid(int level, int grid)
    {
        gridCount[level] -= 1;
        savedLevels[level].grids = savedLevels[level].grids.IndexRemove<GridData>(grid);

    }

//OTHERS
public void Initialise()
{
    levelIndex = 0;
    gridIndex = 0;
    levelCount = 1;
    gridCount = new int[1];
    gridCount[0] = 1;
    gridSize = new int[1];
    gridSize[0] = 2;

    savedLevels = new LevelData[1];
    savedLevels[0] = new LevelData();

    ClearMap();
    SaveMap(0,0);

}

    public void GridSizeClamp()
    {
        for (int i = 0; i <gridSize.Length; i++)
        {
            if(gridSize[i]%2 != 0){
            gridSize[i] += 1;

        }

        }
     
    }

        void PrintSavedLevels()
{
    foreach (LevelData level in savedLevels)
    {
        Debug.Log("level:");
        foreach (GridData grid in level.grids)
        {
            Debug.Log("grid, " + grid.cells);
            
        }

    }
}


//SAVE AND LOAD

    public void SaveMap(int level, int grid)
    {

        Transform[] cellTransform = new Transform[0];

        foreach (Transform transform in gameObject.transform)
        {
            cellTransform = cellTransform.Add<Transform>(transform);
        }

        cellTransform = cellTransform.SortTransforms();

        CellData[] newCells = new CellData[cellTransform.Length];

      for (int i = 0; i < cellTransform.Length; i++)
        {
            GameObject obj = cellTransform[i].gameObject;

            Vector3 pos = cellTransform[i].position;
            string prefabName = obj.name;
            TileState state = obj.GetComponent<MapmakerTile>().state;

            newCells[i] = new CellData(pos,prefabName,state);  

        }

        GridData Grid = savedLevels[levelIndex].grids[gridIndex];

       Grid.cells = newCells;


    

    }

    public void ClearMap()
    {
        Transform[] cellTransform = gameObject.transform.GetComponentsInChildren<Transform>();
        cellTransform = cellTransform.IndexRemove<Transform>(0);

        foreach (Transform child in cellTransform) {
            if (child != null)
            {
                GameObject.DestroyImmediate(child.gameObject);
            }
         
 }
        


    }

    public void LoadMap(int level,int grid)
    {
        GridData loadGrid = savedLevels[levelIndex].grids[gridIndex];
        CellData[] loadCells = loadGrid.cells;
        
        if (loadCells != null)
        {

                    for (int i = 0; i < loadCells.Length; i++)
        {
            string name = loadCells[i].prefabName;
            Instantiate(Resources.Load("MapMaker/"+name,typeof(GameObject)),loadCells[i].worldPosition,Quaternion.Euler(new Vector3(0,0,0)), gameObject.transform);
        }
        }

        else
        {
            Debug.Log("empty map, nothing loaded");
        }
    


    }

    }



