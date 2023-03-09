using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    private float shovelDistancewithCam;
    [Range(0.1f, 2f)]
    public float maxShovelDistance = 2f;
    public Transform cam;

    // Update is called once per frame
    void Update () {
        transform.position = player.transform.position + new Vector3(0,0,1);
    }

    private float MoveWithCamera()
    {
        float currenRotation = Mathf.Clamp(cam.transform.rotation.y * Mathf.Rad2Deg, 30f, 90f) - 30f;
        Debug.Log(currenRotation);
        shovelDistancewithCam = maxShovelDistance * (currenRotation / 60);
        return shovelDistancewithCam;
    }
}
