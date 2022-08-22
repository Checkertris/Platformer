using UnityEngine;
using Wrenji.Constants;

namespace Wrenji.Utils
{

    public class Billboard : MonoBehaviour
    {
        private Transform camTransform;
        private GameObject externalCamera;

        void Start()
        {
            this.externalCamera = GameObject.Find(CameraConstants.CAMERA_OBJECT);
            this.camTransform = this.externalCamera.transform;
        }

        void Update()
        {
            this.transform.rotation = this.camTransform.rotation;
        }
    }
}
//Script makes this object face whatever game object is called "Camera"