using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovellingV2 : MonoBehaviour
{
    [Header("Shovel Settings")]
    public int capacity = 3;

    [Range(1f,8f)]
    public float throwPower = 5f;

    [Range(0.5f, 5f)]
    public float yPower = 3f;
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

    private bool canShovel = true;

    bool mousePressed, mouseReleased;


    public GameObject snowPrefab;

    public AudioClip shovelDownSound;
    public AudioClip shovelUpSound;
    public AudioSource shovelSounds;
    public AudioSource shovelSounds2;


    // Start is called before the first frame update
    void Awake()
    {

        snowsHeld= new GameObject[capacity];
        player = GetComponent<Transform>();
        offset = new Vector3(1f, 1f, 1f);
     //   shovelDownSound = GetComponent<AudioSource>();
        snowsHeld = new GameObject[capacity];
        shovelSounds.clip = shovelDownSound;
        shovelSounds2.clip = shovelUpSound;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(cam.GetComponent<Transform>().localRotation.x * Mathf.Rad2Deg - 10f,0,0);

        if (Input.GetMouseButtonDown(0))
        {
            if(canShovel == true)
            {
                mousePressed = true;
                canShovel= false;
                StartCoroutine(ShovelCooldown());
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            mousePressed = false;
            mouseReleased = true;
            shovelSounds.Stop();
            for (int i = 0; i < capacity; i++)
            {
                if(snowsHeld[i] != null)
                {
                    shovelSounds2.Play();
                    break;
                }
            }
        }

        if (mousePressed == true)
        {
            PickUp(snowsHeld, holdingPosition);

            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && shovelSounds.isPlaying == false)
            {
                shovelSounds.Play();
            }
            else if (shovelSounds.isPlaying == true && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) == false)
            {
                shovelSounds.Pause();
            }
        }

        if (mouseReleased == true)
        {

            mouseReleased = false;
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
        throwingSnow.GetComponent<Rigidbody>().velocity = transform.forward * throwPower + new Vector3(0,yPower,0);
        throwingSnow.GetComponent<BoxCollider>().enabled = true;
        throwingSnow.tag = "Snow Throw";
    }

    private IEnumerator ShovelCooldown()
    {
        yield return new WaitForSeconds(0.75f);
        canShovel = true;
        Debug.Log("test");
    }
}
