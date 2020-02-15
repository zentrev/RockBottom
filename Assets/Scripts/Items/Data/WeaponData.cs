using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ItemData")]
public class WeaponData : ItemData
{
    public float damage;
    public float fireRate;
    public AmmoData.AmmoType compatableAmmoTypes;
}
