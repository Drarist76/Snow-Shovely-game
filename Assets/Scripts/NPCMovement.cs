using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    bool doneMovement = true;
    float point1 = -18;
    float point2 = -4.5f;
    float point3 = -17;
    float point4 = -29;
    float speed;
    float timeCount = 3;
    Vector3 location;
    public SnowHit npcHit;
    public Transform player;
    [SerializeField] LayerMask mask;
    public Collider[] hitColliders;


    // Start is called before the first frame update
    void Start()
    {
        speed = 0.8f * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (npcHit.hit == true)
        {
            Running();
        }else
        {
            if (doneMovement == false)
            {
                MoveToLocation();
            }
            else if (timeCount >= 3)
            {
                timeCount = 0;
                FindLocation();
            }
        }

        timeCount = timeCount + Time.deltaTime;
    }

    void FindLocation()
    {
        location = new Vector3(Random.Range(point1, point2), 9.3f, Random.Range(point4, point3));
        doneMovement = false;
    }

    void MoveToLocation()
    {
        transform.position = Vector3.MoveTowards(transform.position, location, speed);
        Rotation();
        AvoidSnow();

        if (Vector3.Distance(transform.position, location)<0.5f)
        {
            doneMovement = true;
        }
    }

    void Rotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(location.x, 0, location.z)), timeCount);
    }

    void Running()
    {
        if (!Physics.Raycast(new Vector3(transform.position.x, 9.06f, transform.position.y), Vector3.forward, 2f, mask))
        {
            location = transform.position - player.position;
            location.y = 0.17f;
        }

        if (npcHit.angerTime <= 5)
        {
            MoveToLocation();
        }
    }

    void AvoidSnow()
    {
        if(Physics.Raycast(new Vector3(transform.position.x, 9.06f, transform.position.y), Vector3.forward, 2f, mask))
        {
            FindLocation();
        }
    }
}
