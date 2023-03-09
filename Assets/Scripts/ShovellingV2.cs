using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovellingV2 : MonoBehaviour
{
    public int capacity = 3;

    [Range(3f,8f)]
    public float throwPower = 5f;
    //public GameObject snowHeld, snowHeld1, snowHeld2, snowHeld3;
    public GameObject[] snowsHeld;
    public GameObject snowBlockGrabbed;
    //public Transform Holding1, Holding2, Holding3;
    public Transform[] holdingPosition;
    [SerializeField] private GameObject SnowGroup;
    [SerializeField] private GameObject ShoveledSnowParent;
    bool mousePressed, mouseReleased;

    public AudioClip shovelDownSound;
    public AudioClip shovelUpSound;
    public AudioSource shovelSounds;
    public AudioSource shovelSounds2;

    // Start is called before the first frame update
    void Awake()
    {
     //   shovelDownSound = GetComponent<AudioSource>();
        snowsHeld = new GameObject[capacity];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePressed = true;
           //&& if the player is walking ---    
            shovelSounds.clip = shovelDownSound;
            shovelSounds.Play();
        }

        if (Input.GetMouseButtonUp(0))
        {
            mousePressed = false;
            mouseReleased = true;
            shovelSounds.Stop();
            shovelSounds2.clip = shovelUpSound;
            shovelSounds2.Play();
        }

        if (mousePressed == true)
        {
            PickUp(snowsHeld, holdingPosition);
        }

        if (mouseReleased == true)
        {

            mouseReleased = false;
            for (int i = 0; i < capacity; i++)
            {
                Throw(snowsHeld);
                snowsHeld[i] = null;
                Debug.Log("test");
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
                        snowsHeld[i] = snowBlockGrabbed.transform.gameObject;
                        break;
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
