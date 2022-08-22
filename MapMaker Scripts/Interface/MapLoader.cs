using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wrenji.Assets.Tools.Scripts;
using CustomUtils;

public class MapLoader : MonoBehaviour
{

    //Initial
    public int levelIndex;
    public GameObject[] structureEntities;
    public LevelData level;
    public int gridCount;
    public GridData[] grids;


    //Animation/timing
    public float transitionTime;
    public float idleTime;

    public float wallHeight;
    public float floorHeight;
    public int gridSize;


    //Dynamic
    public LevelPhase phase;
    public int gridIndex;
    public float t;


//MAIN METHODS
    void Update()
    {
        UpdateTime();

        if (phase == LevelPhase.TRANSITION)
        {
            UpdateTransition();
        }

    

    }


//INNITIALISATION
    public void Initialise()
    {
        MapMaker mapMaker = FindObjectOfType<MapMaker>();
        level = mapMaker.savedLevels[levelIndex];
        grids = level.grids;

        phase = LevelPhase.PAUSE;
        gridIndex = 0;
        

        //transitionTime = level.transitionTime;
        //idleTime = level.idleTime;
        transitionTime = 0.5f;
        idleTime = 2f;
        gridCount = level.grids.Length;
        wallHeight = 1.5f;
        floorHeight = -1.5f;

        gameObject.ClearChildren();
        CreateEntities();

        StartCoroutine(Idle());
    
    }

    void CreateEntities()
    {

       
        CreatePlatform();
        CreateStructureEntities();





    }

    void CreatePlatform()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        //gen vectors
        float length = gridSize * 0.5f; 
        Vector3[] vert = new Vector3[8];
        vert[0] = new Vector3(length, 0, length);
        vert[1] = new Vector3(length, 0, -length);
        vert[2] = new Vector3(-length, 0, -length);
        vert[3] = new Vector3(-length, 0, length);
        vert[4] = new Vector3(length, -25, length);
        vert[5] = new Vector3(length, -25, -length);
        vert[6] = new Vector3(-length, -25, -length);
        vert[7] = new Vector3(-length, -25, length);

        

 
    }

    void CreateStructureEntities()
    {

        CellData[] firstCells = level.grids[0].cells;

         structureEntities = new GameObject[firstCells.Length];
        Debug.Log(firstCells.Length);

        for (int i = 0; i < firstCells.Length; i++)
        {
            CellData cell = firstCells[i];
            Vector3 pos = firstCells[i].worldPosition;

            //assign height
            float h;
            if (firstCells[i].state == TileState.FLOOR)
            {
                h = floorHeight;
            }
            else if (firstCells[i].state == TileState.WALL)
            {
                h = wallHeight;
            }
            else
            {
                h = 0f;
            }

            pos = new Vector3(pos.x,h,pos.z);

            structureEntities[i] = Instantiate(Resources.Load("MapLoader/Wall-Floor",typeof(GameObject)),pos,Quaternion.Euler(new Vector3(0,0,0)), gameObject.transform)as GameObject;
            

        }

    }

    //PHASE METHODS
    IEnumerator Idle()
    {
        phase = LevelPhase.IDLE;

        yield return new WaitForSeconds(idleTime);
        StartCoroutine(Transition());

    }

   IEnumerator Transition()
    {
        UpdatePhase();
        t = 0;

        //assign phase last since this would call the update transition method
        phase = LevelPhase.TRANSITION;

        yield return new WaitForSeconds(transitionTime);
        StartCoroutine(Idle());

    }
    void UpdatePhase()
    {
        gridIndex += 1;

        if (gridIndex >= gridCount)
        {
            gridIndex = 0;
        }

    }

//ANIMATION
void UpdateTransition()
{
        //for each entity
        //get grid state for incoming state
        //apply either a fuction for wall or floor 
        //should we load this in structure entity instead?

        GridData nextGrid = grids[gridIndex];
        CellData[] nextCells = nextGrid.cells;

        for (int i = 0; i < structureEntities.Length; i++)
        {
            TileState state = nextCells[i].state;
            Transform entityTransform = structureEntities[i].transform;

            if (state == TileState.WALL)
            {

             VerticalTransition(entityTransform,t,wallHeight);

            }

            else if (state == TileState.FLOOR)
            {
                VerticalTransition(entityTransform,t,floorHeight);
                
            }

        }

}

//SIMPLE/UTILITY METHODS
    void UpdateTime()
    {
         t += Time.deltaTime/transitionTime;

    }

 void VerticalTransition(Transform entityTransform, float time, float height)
    {
        Vector3 initPos = entityTransform.position;
        float h = Mathf.Lerp(initPos.y,height,time);

        Vector3 newPos = new Vector3(initPos.x,h,initPos.z);
        entityTransform.position = newPos;
    }


    

}
