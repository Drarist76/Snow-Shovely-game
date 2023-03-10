using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Marker : MonoBehaviour
{
    [SerializeField] List<GameObject> list = new List<GameObject>();
    [SerializeField] SnowBlock snow;
    public GameObject snowman;
    public int total = 12;


    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (list.Count>=total)
        {
            CreateSnowman(list);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Snow"))
        {
            list.Add(other.gameObject);
            snow = other.gameObject.GetComponent<SnowBlock>();
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        list.RemoveAll(x => x == null);

        list.Remove(other.gameObject);
    }

    void CreateSnowman(List<GameObject> snows)
    {
        foreach (var item in snows) 
        {
            Destroy(item);
        }

        snowman.SetActive(true);
        this.enabled = false;
    }
}
