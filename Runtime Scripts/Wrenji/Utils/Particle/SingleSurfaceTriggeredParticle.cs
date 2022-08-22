using System.Collections.Generic;
using UnityEngine;

namespace Wrenji.Utils.Particle
{
    public class SingleSurfaceTriggeredParticle : MonoBehaviour
    {
        protected HashSet<string> triggerSurfaces = new HashSet<string>();

        public GameObject particlePrefab;

        private GameObject aParticle;

        void OnCollisionEnter(Collision collision)
        {
            if (!ShouldPerformEffect(collision))
            {
                return;
            }
            // assert collisionPoint != null;
            if (this.aParticle == null)
            {
                this.aParticle = InstantiateParticle();
            }
            RenderEffect();
        }

        protected bool ShouldPerformEffect(Collision collision) => triggerSurfaces.Contains(collision.gameObject.name);

        protected GameObject InstantiateParticle()
        {
            return Instantiate(this.particlePrefab, base.transform);
        }

        protected void RenderEffect()
        {
            this.aParticle.transform.position = base.transform.position;
            this.aParticle.GetComponent<ParticleSystem>().Play();
        }

    }
}