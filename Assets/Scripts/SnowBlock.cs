using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBlock : MonoBehaviour
{
    public float maxHeightLevel = 15f;
    public float size = 0.125f;
    public float heightIncriment = 0.0625f;
    public int heightLevel = 1;
    public bool onGround = false;
    public bool hasTouchedGround = false;
    public GameObject snowImpactPrefab;

    private void OnValidate()
    {
        /*
                if(transform.parent != null)
                {
                    float parentY = transform.parent.position.y;
                    transform.localScale = new Vector3(transform.localScale.x, size +
                        ((heightLevel - 1) * heightIncriment), transform.localScale.z);
                    transform.position = new Vector3(transform.position.x, parentY + (heightLevel - 1) * heightIncriment / 2, transform.position.z);

                }*/

    }
    void Start()
    {

    }

    // Update is called once per frame

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
                Instantiate(snowImpactPrefab, transform.position, Quaternion.Euler(-90f, 0, 0));
            transform.position = new Vector3(SnapBlocks(transform.position.x), transform.position.y, SnapBlocks(transform.position.z));
            transform.localRotation = Quaternion.identity;
            onGround= true;
            GetComponent<Rigidbody>().isKinematic= true;
            gameObject.tag = "Snow";
            hasTouchedGround = true;
        }

        else if(this.gameObject.tag == "Snow")
        {
            if (collision.gameObject.CompareTag("Snow Throw"))
            {
                if(heightLevel < maxHeightLevel)
                {
                    Instantiate(snowImpactPrefab, transform.position, Quaternion.Euler(-90f, 0, 0));
                    Resize(collision.gameObject.GetComponent<SnowBlock>().heightLevel);
                    heightLevel += collision.gameObject.GetComponent<SnowBlock>().heightLevel;
                    //UpdateHeight();
                    Destroy(collision.gameObject);
                }

            }
        }
        
    }

    private float SnapBlocks(float pos)
    {
        float difference = pos % size;
        pos -= difference;
        return pos;
    }

    public void Resize(int amount)
    {
        Vector3 direction = new Vector3(0, 1, 0);
        transform.position += direction * amount * heightIncriment / 2; // Move the object in the direction of scaling, so that the corner on ther side stays in place
        transform.localScale += direction * amount * heightIncriment; // Scale object in the specified direction
        heightLevel += amount;
    }
}
