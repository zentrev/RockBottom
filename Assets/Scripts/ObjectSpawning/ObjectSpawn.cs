using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    [SerializeField] List<GameObject> itemsToSpawn = new List<GameObject>();

    void Start()
    {
        spawnGrabbable();
    }

    public void spawnGrabbable()
    {
        Instantiate(itemsToSpawn[Random.Range(0, itemsToSpawn.Count - 1)], transform);
    }
}
