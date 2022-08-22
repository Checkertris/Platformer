using UnityEngine;

namespace Wrenji
{

    public class CharacterClass : MonoBehaviour
    {

        private Rigidbody rb;

        protected Vector3 movement;
        protected float speed;

        void Start()
        {
            this.rb = this.GetComponent<Rigidbody>();

        }

        void FixedUpdate()
        {
            this.Movement();
        }


        protected void Movement()
        {
            this.rb.MovePosition(this.transform.position + (Time.deltaTime * speed * movement));

        }
    }
}

//Tutorial Used: https://www.youtube.com/watch?v=ixM2W2tPn6c