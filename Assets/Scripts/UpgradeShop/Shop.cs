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
        SetActiveItem();
    }

    public void ShowPrevItem()
    {
        activeItemNum--;
        SetActiveItem();
    }

    public void SetActiveItem()
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
            if (i == activeItemNum)
            {
                GameObject child = ItemHolder.transform.GetChild(i).gameObject;
                child.SetActive(true);
                child.TryGetComponent(out Rigidbody rigidbody);
                child.TryGetComponent(out BoxCollider boxy);
                if (child.TryGetComponent(out Purchasable item))
                {
                    if (GameManager.Instance.gold >= item.price)
                    {
                        rigidbody.isKinematic = false;
                        boxy.enabled = true;
                    }
                    else
                    {
                        rigidbody.isKinematic = true;
                        boxy.enabled = false;
                    }
                }
            }
            else
            {
                ItemHolder.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void PurchaseItem(Purchasable item)
    {
        GameManager.Instance.gold -= item.price;
        item.price = 0;
    }
}
