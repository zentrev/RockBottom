using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] List<GameObject> shopItems = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject interactable in shopItems)
        {
            interactable.TryGetComponent<Rigidbody>(out Rigidbody rigidbody);
            interactable.TryGetComponent<BoxCollider>(out BoxCollider boxy);
            if(interactable.TryGetComponent<Purchasable>(out Purchasable item))
            {
                if(GameManager.Instance.gold >= item.price)
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
    }

    public void PurchaseItem(Purchasable item)
    {
        GameManager.Instance.gold -= item.price;
        item.price = 0;
    }
}
