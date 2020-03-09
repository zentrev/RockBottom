using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject ItemHolder = null;
    public int activeItemNum = 0;

    [Header("UI")]
    public TextMeshProUGUI MoneyText = null;

    private GameManager gameManager = null;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        MoneyText.text = gameManager.gold.ToString();
    }

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
        //Debug.Log("Active Item Prev: " + activeItemNum);

        if(activeItemNum >= ItemHolder.transform.childCount)
        {
            activeItemNum = 0;
        }

        if(activeItemNum < 0)
        {
            activeItemNum = ItemHolder.transform.childCount - 1;
        }

        //Debug.Log("Active Item After Clamp: " + activeItemNum);

        foreach(Transform child in ItemHolder.transform)
        {
            child.gameObject.SetActive(false);  
        }

        for (int i = 0; i < ItemHolder.transform.childCount; i++)
        {
            GameObject child = ItemHolder.transform.GetChild(i).gameObject;
            if (i == activeItemNum)
            {
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
                child.SetActive(false);
            }
        }
    }

    public void PurchaseItem(Purchasable item)
    {
        if(GameManager.Instance.gold - item.price >= 0)
        {
            GameManager.Instance.gold -= item.price;
            
            item.price = 0;
        }
    }
}
