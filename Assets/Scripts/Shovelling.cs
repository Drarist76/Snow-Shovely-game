using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovelling : MonoBehaviour
{
    public GameObject snowHeld, snowHeld1, snowHeld2, snowHeld3;
    public Transform Holding1, Holding2, Holding3;
    bool mousePressed, mouseReleased;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            mousePressed = true; 
        }

        if (Input.GetMouseButtonUp(0))
        {
            mousePressed = false;
            mouseReleased = true;
        }

        if (mousePressed == true)
        {
            PickUp(snowHeld1, snowHeld2, snowHeld3, Holding1, Holding2, Holding3);
        }

        if (mouseReleased == true)
        {
            Throw(snowHeld1, snowHeld2, snowHeld3);
            mouseReleased = false;
            snowHeld1 = null;
            snowHeld2 = null;
            snowHeld3 = null;
        }
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Snow")
        {
            snowHeld = col.gameObject;
            if (mousePressed) {
                if (snowHeld1 == null) {
                    snowHeld1 = snowHeld.transform.gameObject;
                } else if (snowHeld2 == null)
                {
                    snowHeld2 = snowHeld.transform.gameObject;
                }
                else if (snowHeld3 == null)
                {
                    snowHeld3 = snowHeld.transform.gameObject;
                }
            }

        }
    }

    void OnTriggerExit(Collider exitCol)
    {
        if (mouseReleased == true)
        {
            if (exitCol.transform.parent.gameObject == snowHeld1)
            {
                snowHeld1 = null;
            }

            if (exitCol.transform.parent.gameObject == snowHeld2)
            {
                snowHeld2 = null;
            }

            if (exitCol.transform.parent.gameObject == snowHeld3)
            {
                snowHeld3 = null;
            }
        }
    }

    public void PickUp(GameObject snow1, GameObject snow2, GameObject snow3, Transform held1, Transform held2, Transform held3){
        if (snowHeld1 != null)
        {
            snowPickUp(snow1, held1);
        }
        
        if(snowHeld2 != null)
        {
            snowPickUp(snow2, held2);
        }
        
        if(snowHeld3 != null)
        {
            snowPickUp(snow3, held3);
        }
    }

    public void Throw(GameObject snow1, GameObject snow2, GameObject snow3)
    {
        if (snow1 != null)
        {
            snowThrow(snow1);
        }
        if (snow2 != null)
        {
            snowThrow(snow2);
        }
        if (snow3 != null)
        {
            snowThrow(snow3);
        }
    }

    void snowPickUp(GameObject assignedSnow, Transform assignedPos)
    {
        assignedSnow.GetComponent<Rigidbody>().useGravity = false;
        assignedSnow.transform.position = assignedPos.position;
        assignedSnow.GetComponent<Rigidbody>().isKinematic = true;
        assignedSnow.GetComponent<BoxCollider>().enabled = false;
        assignedSnow.GetComponent<SnowBlock>().onGround = false;
    }

    public void snowThrow(GameObject throwingSnow)
    {
        throwingSnow.GetComponent<Rigidbody>().useGravity = true;
        throwingSnow.GetComponent<Rigidbody>().isKinematic = false;
        throwingSnow.GetComponent<Rigidbody>().velocity = transform.forward * 3;
        throwingSnow.GetComponent<BoxCollider>().enabled = true;
        throwingSnow.tag = "Snow Throw";
    }
}
