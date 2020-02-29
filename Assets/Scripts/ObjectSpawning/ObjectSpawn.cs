using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    [SerializeField] List<GameObject> itemsToSpawn = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        spawnGrabbable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnGrabbable()
    {
        Instantiate(itemsToSpawn[Random.Range(0, itemsToSpawn.Count - 1)], gameObject.transform);
        //Instantiate(itemsToSpawn[Random.Range(0,itemsToSpawn.Count-1)], gameObject.transform.position, gameObject.transform.rotation);
    }
}
