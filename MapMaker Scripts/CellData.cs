using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wrenji.Assets.Tools.Scripts;

namespace Wrenji.Assets.Tools.Scripts
{
  
    public class CellData
    {
        
        //Input Values        
        public TileState state;
        public Vector3 worldPosition;
        public string prefabName;

        public CellData(Vector3 pos, string prefab, TileState saveState)
        {
            string name = prefab;

            if (name.Contains("(Clone)"))
            {
                for (int i = 0; i < 7; i++)
                {
                    name = name.Remove(name.Length-1);
                }
                
            }

            prefabName = name;
            worldPosition = pos;
            state = saveState;
            

        }

        public CellData()
        {
            
        }
    }

}
