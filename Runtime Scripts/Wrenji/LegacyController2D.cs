using UnityEngine;
using UnityEngine.Events;
using Wrenji.Constants;

// FIXME: Use namespace
public class LegacyController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpSpeed;                          // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

    const float k_GroundedRadius = 0.1f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = 0.1f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector2 m_Velocity = Vector2.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

    //variables i added
    public float fallMultiplier;
    public float jumpMultiplier;
    public float walkSpeed;
    public Transform spriteTransform;
    private bool Landing;


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        spriteTransform = GameObject.Find(PlayerConstants.PLAYER_SPRITE).transform;


        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {
        //bool wasGrounded = m_Grounded;

        //i added this to suit 3D

        m_Grounded = Physics2D.OverlapCircle(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

        LandingParticleEffect();
         JumpCorrection();



    }

    void LandingParticleEffect()
    {
        if (Landing && m_Grounded)
        {
            ParticleSystem ps = GameObject.Find("GroundPS").GetComponent<ParticleSystem>();
            ps.Play();

            Landing = false;
        }

        if (!m_Grounded)
        {
            Landing = true;
        }


    }


    private void JumpCorrection()
    {

        if (m_Rigidbody.velocity.y < 0)
        {
            m_Rigidbody.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        }

        else if (m_Rigidbody.velocity.y > 0)
        {
            m_Rigidbody.velocity += Vector2.up * Physics.gravity.y * (jumpMultiplier - 1) * Time.deltaTime;

        }

    }



    public void Move(Vector2 move, bool crouch, bool jump)
    {
        move = move * walkSpeed;

        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_GroundedRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Move the character by finding the target velocity
            Vector2 targetVelocity = move;
            // And then smoothing it out and applying it to the character

            m_Rigidbody.velocity = Vector2.SmoothDamp(m_Rigidbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);



            // If the input is moving the player right and the player is facing left...
            if (move.x > 0 && !m_FacingRight)
            {
                m_FacingRight = !m_FacingRight;
                spriteTransform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move.x < 0 && m_FacingRight)
            {
                m_FacingRight = !m_FacingRight;
                spriteTransform.localRotation = Quaternion.Euler(0, 0, 0);
            }

        }
        // If the player should jump, then turn off jump
        if (m_Grounded && jump)
        {

            m_Rigidbody.velocity += Vector2.up * m_JumpSpeed;
            m_Grounded = false;
      
        }

    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        spriteTransform.localRotation = Quaternion.Euler(0, 180, 0);
    }
}
