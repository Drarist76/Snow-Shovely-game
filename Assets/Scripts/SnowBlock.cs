using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBlock : MonoBehaviour
{
    public float size = 0.25f;
    public float heightIncriment = 0.0625f;
    public bool onGround = false;

    void Start()
    {
       //transform.position = new Vector3(SnapBlocks(transform.position.x), transform.position.y, SnapBlocks(transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            transform.position = new Vector3(SnapBlocks(transform.position.x), transform.position.y, SnapBlocks(transform.position.z));
            transform.localRotation = Quaternion.identity;
            onGround= true;
            GetComponent<Rigidbody>().isKinematic= true;
            gameObject.tag = "Snow";
        }

        if(this.gameObject.tag == "Snow")
        {
            if (collision.gameObject.CompareTag("Snow Throw"))
            {
                Resize(heightIncriment, new Vector3(0, 1, 0));
                //UpdateHeight();
                Destroy(collision.gameObject);
            }
        }
        
    }

    private float SnapBlocks(float pos)
    {
        float difference = pos % size;
        pos -= difference;
        return pos;
    }

    public void Resize(float amount, Vector3 direction)
    {
        transform.position += direction * amount / 2; // Move the object in the direction of scaling, so that the corner on ther side stays in place
        transform.localScale += direction * amount; // Scale object in the specified direction
    }
}
