using System;
using System.Collections;
using System.Collections.Generic;
using Wrenji.Assets.Tools.Scripts;

namespace Wrenji.Assets.Scripts.Tools
{
    [Serializable]
    public class PhaseData
    {
        public int currentPhase;
        public TileState tileState;

        public PhaseData(int currentPhase, TileState tileState)
        {
            this.currentPhase = currentPhase;
            this.tileState = tileState;
        }

    }

}