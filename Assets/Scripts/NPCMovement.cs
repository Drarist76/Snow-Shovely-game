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
    public Transform location;
    public SnowHit npcHit;
    public Transform player;
    [SerializeField] LayerMask mask;
    public Collider[] hitColliders;
    RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {
        speed = 0.8f * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(new Vector3(transform.position.x, 9.1f, transform.position.z), location.localPosition);
        if (npcHit.hit == true)
        {
            Running();
            speed = 1.5f * Time.deltaTime;
        }
        else
        {
            speed = 0.8f * Time.deltaTime;
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
        location.position = new Vector3(Random.Range(point4, point3), 9.1f, Random.Range(point1, point2));
        doneMovement = false;
    }

    void MoveToLocation()
    {
        Rotation();
        transform.position = Vector3.MoveTowards(transform.position, location.position, speed);
        AvoidSnow();

        if (Vector3.Distance(transform.position, location.position) <0.5f)
        {
            doneMovement = true;
        }
    }

    void Rotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(location.position.x, 0, location.position.z)), 10* Time.deltaTime);
    }

    void Running()
    {

        location.position = transform.position - player.position;
        location.Translate(location.position.x, 9.3f, location.position.z);

        if (npcHit.angerTime <= 5)
        {
            MoveToLocation();
        }
    }

    void AvoidSnow()
    {
        if (Physics.SphereCast(new Vector3(transform.position.x, 9.1f, transform.position.z), 0.3f, location.localPosition, out hit, 2f, mask))
        {
            Debug.Log("hit" + hit.collider.gameObject);
            FindLocation();
        }
    }
}
