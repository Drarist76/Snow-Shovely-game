using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBlock : MonoBehaviour
{
    public float size = 0.25f;
    public bool onGround = true;

    void Start()
    {
       transform.position = new Vector3(SnapBlocks(transform.position.x), transform.position.y, SnapBlocks(transform.position.z));
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
            Debug.Log("touch");
            transform.localRotation = Quaternion.identity;
        }
        
    }

    private float SnapBlocks(float pos)
    {
        float difference = pos % size;
        pos -= difference;
        return pos;
    }
}
