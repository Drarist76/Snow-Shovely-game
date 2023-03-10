using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlayerDown : MonoBehaviour
{
    public PlayerMovement player;

    public float slowMultipliyer = 1f;

    [Range(0.01f, 0.2f)]
    public float slownessIncrement = 0.05f;

    [Range(0.1f, 1f)]
    public float maxSlowness = 0.5f;
    public BoxCollider feet;

    private void Start()
    {
        feet = GetComponent<BoxCollider>();
        player = player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Snow"))
        {
            slowMultipliyer =  1 - (other.GetComponent<SnowBlock>().heightLevel * slownessIncrement);
            if(slowMultipliyer < maxSlowness)
            {
                slowMultipliyer = maxSlowness;
                player.isOnSnow = true;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Snow"))
        {
            slowMultipliyer = 1f;
            player.isOnSnow = false;
        }
    }
}
