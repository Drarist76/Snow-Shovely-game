using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBlock : MonoBehaviour
{
    public float size = 0.125f;
    public float heightIncriment = 0.0625f;

    [Range(1,10)]
    public int heightLevel = 1;
    public bool onGround = false;

    private void OnValidate()
    {
        if(transform.parent != null)
        {
            float parentY = transform.parent.position.y;
            transform.localScale = new Vector3(transform.localScale.x, size +
                ((heightLevel - 1) * heightIncriment), transform.localScale.z);
            transform.position = new Vector3(transform.position.x, parentY + (heightLevel - 1) * heightIncriment / 2, transform.position.z);
        }
        
    }
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

                Resize(collision.gameObject.GetComponent<SnowBlock>().heightLevel * heightIncriment, new Vector3(0, 1, 0));
                heightLevel += collision.gameObject.GetComponent<SnowBlock>().heightLevel;
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
