using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    bool doneMovement = true;
    float maxRange = 3;
    float speed;
    float timeCount = 3;
    Vector3 location;


    // Start is called before the first frame update
    void Start()
    {
        speed = 0.2f * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (doneMovement == false)
        {
            MoveToLocation();
        }else if(timeCount>=3)
        {
            timeCount = 0;
            FindLocation();
        }

        timeCount = timeCount + Time.deltaTime;
    }

    void FindLocation()
    {
        location = new Vector3(Random.Range(-maxRange, maxRange), 0.0f, Random.Range(-maxRange, maxRange));
        doneMovement = false;
    }

    void MoveToLocation()
    {
        transform.position = Vector3.MoveTowards(transform.position, location, speed);
        Rotation();

        if (Vector3.Distance(transform.position, location)<0.5f)
        {
            doneMovement = true;
        }
    }

    void Rotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(location), timeCount);
    }
}
