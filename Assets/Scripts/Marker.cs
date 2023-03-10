using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Marker : MonoBehaviour
{
    [SerializeField] List<Collider> list = new List<Collider>();
    [SerializeField] List<int> inside = new List<int>();
    [SerializeField] SnowBlock snow;
    [SerializeField] int total;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        total = inside.Sum();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Snow"))
        {
            list.Add(other);
            snow = other.gameObject.GetComponent<SnowBlock>();
            inside.Add(snow.heightLevel);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (list.Contains(null)) {
            list.RemoveAll(x => x == null);
        }

        for (int i = 0; i <= list.Count; i++)
            {
                if (other == list[i]) {
                    snow = list[i].gameObject.GetComponent<SnowBlock>();
                    total = total - snow.heightLevel;
                    list.Remove(list[i]);
                }
            }
    }
}
