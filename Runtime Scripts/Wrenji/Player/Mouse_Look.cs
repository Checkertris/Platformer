using UnityEngine;

namespace Wrenji.Player
{

    public class Mouse_Look : MonoBehaviour
    {

        private float mouseSensitivity = 1000f;
        private float xRotation = 0f;

        public Transform playerBody;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * this.mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * this.mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;

            base.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            this.playerBody.Rotate(Vector3.up * mouseX);
        }
    }

}

