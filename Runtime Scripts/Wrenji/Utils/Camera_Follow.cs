using UnityEngine;
using Wrenji.Constants;


namespace Wrenji.Utils
{

    public class Camera_Follow : MonoBehaviour
    {
        //Definitions
        private Transform targetTransform;
        private GameObject target;

      //  protected GameObject GetTarget() => this.target;


        //Parameters, smoothSpeed much be 0<n<1
        public float smoothSpeed;
        public Vector3 Offset = new Vector3(0f, 13.42f, -21f);

        void Start()
        {
            this.target = GameObject.Find(PlayerConstants.PLAYER_OBJECT);
            this.targetTransform = this.target.transform;
            StartLookAtTarget();

        }

        void FixedUpdate()
        {

            SmoothFollow();

        }

  
        private void StartLookAtTarget()
        {
            gameObject.transform.LookAt(targetTransform);
        }

        private void SmoothFollow()
        {
            Vector3 desiredPosition = new Vector3(this.targetTransform.position.x, 0f, this.targetTransform.position.z) + Offset;
            Vector3 smoothPosition = Vector3.Lerp(base.transform.position, desiredPosition, this.smoothSpeed);
            base.transform.position = smoothPosition;
        }
    }

}

//Tutorial used: https://www.youtube.com/watch?v=MFQhpwc6cKEr