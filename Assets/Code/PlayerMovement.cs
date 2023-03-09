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

    public AudioSource walkingSounds;
    public Transform groundCheck;
    public float groundDistance = 0.05f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

       

        if (isGrounded && velocity.y < 0)
        {
         
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //sounds
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) && walkingSounds.isPlaying == false)
        {
            walkingSounds.clip = WalkingOnSnow;
            walkingSounds.Play();
        }
           if (Input.GetKeyUp(KeyCode.W) && Input.GetKeyUp(KeyCode.A) && Input.GetKeyUp(KeyCode.S) && Input.GetKeyUp(KeyCode.D) && walkingSounds.isPlaying == true)
         {
              walkingSounds.clip = WalkingOnSnow;
              walkingSounds.Pause(); 
          }   

    }
}
