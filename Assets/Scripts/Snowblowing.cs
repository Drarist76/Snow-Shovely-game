using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowblowing : MonoBehaviour
{
    public Shovelling snowMove;
    GameObject blowingSnow, blownSnow1, blownSnow2, blownSnow3;
    Transform holding1, holding2, holding3;
    bool snowblowerOn;

    // Start is called before the first frame update
    void Start()
    {
        holding1 = snowMove.Holding1;
        holding2 = snowMove.Holding2;
        holding3 = snowMove.Holding3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            snowblowerOn = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            snowblowerOn = false;
        }

        if (snowblowerOn == true)
        {
            if (blownSnow1 != null)
            {
                snowMove.snowThrow(blownSnow1);
            }

            if (blownSnow2 != null)
            {
                snowMove.snowThrow(blownSnow2);
            }

            if (blownSnow3 != null)
            {
                snowMove.snowThrow(blownSnow3);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Snow")
        {
            blowingSnow = col.gameObject;
                if (blownSnow1 == null)
                {
                    blownSnow1 = blowingSnow.transform.gameObject;
                }
                else if (blownSnow2 == null)
                {
                    blownSnow2 = blowingSnow.transform.gameObject;
                }
                else if (blownSnow3 == null)
                {
                    blownSnow3 = blowingSnow.transform.gameObject;
                }
            }

        }

    void OnTriggerExit(Collider exitCol)
    {
        if (exitCol.transform.gameObject == blownSnow1)
        {
            blownSnow1 = null;
        }

        if (exitCol.transform.gameObject == blownSnow2)
        {
            blownSnow2 = null;
        }

        if (exitCol.transform.gameObject == blownSnow3)
        {
            blownSnow3 = null;
        }
    }
}
