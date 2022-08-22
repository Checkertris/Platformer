using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wrenji.Constants;

public enum PlayerState {Idle, Jump, Walk, Crouch, Block, Attack};

public class PlayerController : MonoBehaviour
{

    //Constants
    private Animator animator;
    private LegacyController2D movementController;

    //Bools
    private bool jump;
    private bool crouch;
    private bool block;
    private PlayerState _state;

    //General Movement
    private Vector2 movement = new Vector2(0f, 0f);
    private float magnitude;
    float speed;

    //Character Specific
    public string[] attackControl;

    void Start()
    {

        Settings();

    }

    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(movement.x));
    }

    private void FixedUpdate()
    {
        this.movementController.Move(this.movement, crouch, jump);

    }

    void Settings()
    {
        speed = 300;
        jump = false;
        crouch = false;
        block = false;

        //Constants
        this.animator = GameObject.Find(PlayerConstants.PLAYER_SPRITE).GetComponent<Animator>();
        this.movementController = this.GetComponent<LegacyController2D>();
    }

   
    void MovementInput(string blockKey, string crouchKey)
    { 

        this.block = Input.GetKey(blockKey);

        if (block)
        {
            this.movement = new Vector2(0f, 0f);
            this.crouch = false;
            this.jump = false;
        }

        else

        {
            this.crouch = Input.GetKey(crouchKey);

            this.movement = new Vector2(Input.GetAxis("Horizontal")* this.speed * Time.fixedDeltaTime, 0f);

            if (crouch)
            {
                this.jump = false;
            }

            else
            {
                this.jump = Input.GetAxis("Jump") > 0;
            }
        }

        //ANIMATIONS
        animator.SetBool("Jump", jump);
        animator.SetBool("Crouch", crouch);
        animator.SetBool("Block", block);
    }

    void PlayParticleSystem(string objectName)
    {
        ParticleSystem ps = GameObject.Find(objectName).GetComponent<ParticleSystem>();
        ps.Play();

    }
}

//Tutorials Used: https://www.youtube.com/watch?v=ixM2W2tPn6cˆ, https://www.youtube.com/watch?v=sPiVz1k-fEs
