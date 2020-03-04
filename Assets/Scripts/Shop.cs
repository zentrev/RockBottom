using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject ItemHolder = null;
    public int activeItemNum = 0;

    public void ShowNextItem()
    {
        activeItemNum++;
        SetActiveItem(activeItemNum);
    }

    public void ShowPrevItem()
    {
        activeItemNum--;
        SetActiveItem(activeItemNum);
    }

    public void SetActiveItem(int num)
    {
        if(activeItemNum > ItemHolder.transform.childCount-1)
        {
            activeItemNum = 0;
        }

        if(activeItemNum < 0)
        {
            activeItemNum = ItemHolder.transform.childCount - 1;
        }

        for (int i = 0; i < ItemHolder.transform.childCount; i++)
        {
            if (i == num)
            {
                ItemHolder.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                ItemHolder.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
