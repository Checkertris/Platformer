using UnityEngine;
using Wrenji.Constants;

namespace Wrenji.Utils.Particle
{

    public class GroundParticleEffect : SingleSurfaceTriggeredParticle
    {

        public GroundParticleEffect()
        {
            base.triggerSurfaces.Add(SurfaceConstants.GROUND_OBJECT);
        }

    }
    
}