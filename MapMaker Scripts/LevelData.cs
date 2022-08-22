using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wrenji.Assets.Tools.Scripts;

namespace Wrenji.Assets.Tools.Scripts
{
  
    public class LevelData
    {
        public GridData[] grids;

        public float transitionTime;
        public float idleTime;

        public LevelData()
        {
            grids = new GridData[1];
            grids[0] = new GridData();
        }

    
    }
    

}
