using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoData", menuName = "ItemData/AmmoData")]
public class AmmoData : ItemData
{
    [System.Flags]
    public enum AmmoType
    {
        NONE         = 0,
        ARROW        = 1 << 0,
        BULLET       = 1 << 1,
        BALLISTA     = 1 << 2,
        ALL          = ~0
    }

    public AmmoType ammoType;
    public float damageModifier;
    public float speed;
    public bool isInstant;
}
