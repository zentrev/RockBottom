using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void EndGame()
    {
        
    }
}
