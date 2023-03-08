using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody Player;
    float speed = 1f;

    void Start()
    {
        Player = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A)){
            Player.velocity = new Vector3(-speed, 0, 0);
            rotatePlayer();
        }

        if(Input.GetKey(KeyCode.S)){
            Player.velocity = new Vector3(0, 0, -speed);
            rotatePlayer();
        }

        if(Input.GetKey(KeyCode.D)){
            Player.velocity = new Vector3(speed, 0, 0);
            rotatePlayer();
        }

        if(Input.GetKey(KeyCode.W)){
            Player.velocity = new Vector3(0, 0, speed);
            rotatePlayer();
        }
    }
    void rotatePlayer()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.velocity), 3*Time.deltaTime);
        transform.Translate(Player.velocity * speed * Time.deltaTime, Space.World);
    }
}
