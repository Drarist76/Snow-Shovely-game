using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowHit : MonoBehaviour
{
    public bool hit;
    public float angerTime = 0;
    public Transform snowPos;
    Color32 originalColor;


    // Start is called before the first frame update
    void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hit == true)
        {
            Anger();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Snow Throw"){
            hit = true;
        }

        if (collision.gameObject.CompareTag("Snow"))
        {
            snowPos = collision.gameObject.transform;
        }
    }

    void Anger()
    {
        var colorMad = GetComponent<Renderer>();
        colorMad.material.SetColor("_Color", Color.red);
        angerTime = angerTime + Time.deltaTime;

        if (angerTime > 5)
        {
            colorMad.material.SetColor("_Color", originalColor);
            angerTime = 0;
            hit = false;
        }
    }
}
