using UnityEngine;
using UnityEngine.Events;


// FIXME: Use namespace
namespace Wrenji.Utils
{
    public class CharacterController2D : MonoBehaviour
    {
        // Amount of force added when the player jumps.
        [SerializeField] private float m_JumpForce;

        // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;

        // How much to smooth out the movement
        [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;

        // Whether or not a player can steer while jumping;
        [SerializeField] private bool m_AirControl = false;

        // A mask determining what is ground to the character  
        [SerializeField] private LayerMask m_WhatIsGround;

        // A position marking where to check if the player is grounded.
        [SerializeField] private Transform m_GroundCheck;

        // A position marking where to check for ceilings 
        [SerializeField] private Transform m_CeilingCheck;

        // A collider that will be disabled when crouching
        [SerializeField] private Collider2D m_CrouchDisableCollider;

        // Whether or not the player is grounded. 
        private bool m_Grounded;

        private Rigidbody m_Rigidbody;
        // For determining which way the player is currently facing.
        private bool m_FacingRight = true;
        private Vector3 m_Velocity = Vector3.zero;

        // Radius of the overlap circle to determine if grounded
        const float k_GroundedRadius = 0.1f;

        // Radius of the overlap circle to determine if the player can stand up
        const float k_CeilingRadius = 0.1f;

        [Header("Events")]
        [Space]

        public UnityEvent OnLandEvent;

        [System.Serializable]
        public class BoolEvent : UnityEvent<bool> { }

        public BoolEvent OnCrouchEvent;
        private bool m_wasCrouching = false;

        //variables i added
        public float fallMultiplier = 2.5f;


        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();


            if (OnLandEvent == null)
                OnLandEvent = new UnityEvent();

            if (OnCrouchEvent == null)
                OnCrouchEvent = new BoolEvent();
        }

        private void FixedUpdate()
        {
            //bool wasGrounded = m_Grounded;

            //i added this to suit 3D

            m_Grounded = Physics.CheckSphere(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

            PerformFallCorrection();

        }

        private void PerformFallCorrection()
        {
            if (m_Rigidbody.velocity.y < 0)
            {
                m_Rigidbody.velocity += (fallMultiplier - 1) * Time.deltaTime * Vector3.up * Physics.gravity.y;
            }
        }

        public void Move(Vector3 move, bool crouch, bool jump)
        {

            // If crouching, check to see if the character can stand up
            if (!crouch)
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                crouch = Physics2D.OverlapCircle(m_CeilingCheck.position, k_GroundedRadius, m_WhatIsGround);
            }
            // If the player should jump...
            if (m_Grounded && jump)
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Rigidbody.AddForce(new Vector3(0f, m_JumpForce, 0f));
            }

            //only control the player if grounded or airControl is turned on
            if (!m_Grounded && !m_AirControl)
            {
                return;
            }


            // Invoke handlers for when a player croucheds
            m_wasCrouching = !crouch;
            OnCrouchEvent.Invoke(!crouch);

            // Enable the collider when not crouching
            if (m_CrouchDisableCollider != null)
            {
                m_CrouchDisableCollider.enabled = !crouch;
            }

            if (crouch)
            {
                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;
            }


            // Move the character by finding the target velocity
            Vector3 targetVelocity = move;
            // And then smoothing it out and applying it to the character
            //smoothing when moving horizontal is changed to making it look more natural

            float movementSmoothing = move.z == 0 ? m_MovementSmoothing * 3 : m_MovementSmoothing;
            m_Rigidbody.velocity = Vector3.SmoothDamp(m_Rigidbody.velocity, targetVelocity, ref m_Velocity, movementSmoothing);



            // If the input is moving the player right and the player is facing left...
            if (move.x > 0 && !m_FacingRight)
            {

            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move.x < 0 && m_FacingRight)
            {

            }

        }

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }

}