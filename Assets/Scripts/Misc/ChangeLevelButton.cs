using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//[RequireComponent(typeof(Button))]
public class ChangeLevelButton : MonoBehaviour
{
    [SerializeField] string scenename = "MapleLeafSyrupTower";
    //[SerializeField] int m_nextLevel = 0;
    //private Button m_button = null;

    void Start()
    {
        //m_button = GetComponent<Button>();
        //m_button.onClick.AddListener(TaskOnClick);
    }

    //void TaskOnClick()
    //{
    //    GameManager.Instance.LoadLevel(m_nextLevel);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        //Layer 9 = Ammo
        if(collision.gameObject.layer == 9)
        {
            if (scenename != "Quit")
            {
                SceneManager.LoadScene(scenename);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
