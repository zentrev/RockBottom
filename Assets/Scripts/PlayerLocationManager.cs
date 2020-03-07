using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocationManager : MonoBehaviour
{

    [SerializeField] GameObject playerStartLocation = null;
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = playerStartLocation.transform.position;
    }
}
