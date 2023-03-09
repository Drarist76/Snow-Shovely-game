using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovellingV2 : MonoBehaviour
{
    [Header("Shovel Settings")]
    public int capacity = 3;
    [Range(3f,8f)]
    public float throwPower = 5f;
    private Vector3 offset;

    [Header("Arrays")]
    public GameObject snowBlockGrabbed;
    public GameObject[] snowsHeld;
    public Transform[] holdingPosition;

    [Header("Containers/Parents")]
    [SerializeField] private GameObject SnowGroup;
    [SerializeField] private GameObject ShoveledSnowParent;

    [Header("Follow")]
    public Transform player;
    public Transform cam;

    bool mousePressed, mouseReleased;

    public GameObject snowPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        snowsHeld= new GameObject[capacity];
        player = GetComponent<Transform>();
        offset = new Vector3(1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(cam.GetComponent<Transform>().localRotation.x * Mathf.Rad2Deg - 30f,0,0);

        if (Input.GetMouseButtonDown(0))
        {
            mousePressed = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mousePressed = false;
            mouseReleased = true;
        }

        if (mousePressed == true)
        {
            PickUp(snowsHeld, holdingPosition);
        }

        if (mouseReleased == true)
        {   
            mouseReleased= false;
            for (int i = 0; i < capacity; i++)
            {
                Throw(snowsHeld);
                snowsHeld[i] = null;
            }
        }


    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Snow")
        {
            snowBlockGrabbed = col.gameObject;
            if (mousePressed)
            {
                for (int i = 0; i < capacity; i++)
                {
                    if (snowsHeld[i] == null)
                    {
                        if(snowBlockGrabbed.GetComponent<SnowBlock>().heightLevel <= 5)
                        {
                            snowsHeld[i] = snowBlockGrabbed.transform.gameObject;
                            break;
                        }
                        else
                        {
                            snowsHeld[i] = Instantiate(snowPrefab, transform.position, Quaternion.identity);
                            snowsHeld[i].GetComponent<SnowBlock>().Resize(3);
                            snowBlockGrabbed.GetComponent<SnowBlock>().Resize(-3);
                            break;
                        }
                    }
                }
            }

        }
    }

    void OnTriggerExit(Collider exitCol)
    {
        if (mouseReleased == true)
        {
            for (int i = 0; i < capacity; i++)
            {
                if (exitCol.transform.parent.gameObject == snowsHeld[i])
                {
                    snowsHeld[i] = null;
                }
            }
        }
    }

    public void PickUp(GameObject[] snow, Transform[] held)
    {
        for (int i = 0; i < capacity; i++)
        {
            if (snowsHeld[i] != null)
            {
                snowPickUp(snow[i], held[i]);
            }
        }
    }

    public void Throw(GameObject[] snow)
    {
        for (int i = 0; i < capacity; i++)
        {
            if (snow[i] != null)
            {
                snowThrow(snow[i]);
            }
        }
    }

    void snowPickUp(GameObject assignedSnow, Transform assignedPos)
    {
        assignedSnow.GetComponent<Rigidbody>().useGravity = false;
        assignedSnow.transform.position = assignedPos.position + 
            new Vector3(0,assignedSnow.GetComponent<SnowBlock>().heightLevel * assignedSnow.GetComponent<SnowBlock>().heightIncriment/2,0);
        assignedSnow.GetComponent<Rigidbody>().isKinematic = true;
        assignedSnow.GetComponent<BoxCollider>().enabled = false;
        assignedSnow.GetComponent<SnowBlock>().onGround= false;
    }

    public void snowThrow(GameObject throwingSnow)
    {
        throwingSnow.GetComponent<Rigidbody>().useGravity = true;
        throwingSnow.GetComponent<Rigidbody>().isKinematic = false;
        throwingSnow.GetComponent<Rigidbody>().velocity = transform.forward * throwPower;
        throwingSnow.GetComponent<BoxCollider>().enabled = true;
        throwingSnow.tag = "Snow Throw";
    }
}
