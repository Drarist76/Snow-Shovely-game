using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBlock : MonoBehaviour
{
    public float size = 0.25f;
    public float height = 0.25f;
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
            gameObject.tag = "Snow";
        }

        if(this.gameObject.tag == "Snow")
        {
            if (collision.gameObject.CompareTag("Snow Throw"))
            {
                transform.position += new Vector3(0, 0.0625f, 0);
                height += 0.0625f;
                UpdateHeight();
                Destroy(collision.gameObject);
            }
        }
        
    }

    private float SnapBlocks(float pos)
    {
        float difference = pos % size;
        Debug.Log(difference);
        pos -= difference;
        return pos;
    }

    private void UpdateHeight()
    {
        transform.localScale = new Vector3(transform.localScale.x, height, transform.localScale.z);
    }
}
