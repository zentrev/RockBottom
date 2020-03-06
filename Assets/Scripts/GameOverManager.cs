using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : Singleton<GameOverManager>
{
    public GameObject GameOverCanvas = null;
    public float CanvasYOffset = 10;

    GameManager gameManager = null;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void TurnOnGameOver()
    {
        Time.timeScale = 0;

        Vector3 tempPos = gameManager.Face.transform.position;

        tempPos.y -= CanvasYOffset;

        GameOverCanvas.transform.position = tempPos;

        GameOverCanvas.SetActive(true);
    }
}
