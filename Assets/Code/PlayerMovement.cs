using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public AudioClip WalkingOnSnow;
    public AudioClip WalkingOnGround;
    public SlowPlayerDown playerFeet;

    public AudioSource walkingSounds;
   // public AudioSource walkingSounds2;
    public Transform groundCheck;
    public float groundDistance = 0.05f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    private bool onSnowSound;

    void Start()
    {
        walkingSounds.clip = WalkingOnGround;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


        if (SlowPlayerDown.isOnSnow == true) 
        {
                if(onSnowSound == false)
                {
                walkingSounds.Pause();
                walkingSounds.clip = WalkingOnSnow;
                Debug.Log("snow");
                walkingSounds.Play();
                onSnowSound = true;
                }
                
            
        }

        if (SlowPlayerDown.isOnSnow == false)
        {
  
                if(onSnowSound == true)
                {
                walkingSounds.Pause();
                walkingSounds.clip = WalkingOnGround;
                Debug.Log("ground");
                walkingSounds.Play();
                onSnowSound = false;
                }

            
        }


        if (isGrounded && velocity.y < 0)
        {
         
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * playerFeet.GetComponent<SlowPlayerDown>().slowMultipliyer * Time.deltaTime);


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //sounds
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) && walkingSounds.isPlaying == false)
        {
            walkingSounds.Play();
        }
        else if (walkingSounds.isPlaying == true && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) == false)
            {
                walkingSounds.Pause();
            } 

    }
}
