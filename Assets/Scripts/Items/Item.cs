using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item<T> : MonoBehaviour where T : ItemData
{
    //This is the base object for interactable objects
    public Item()
    {

    }

    public T data;
}
