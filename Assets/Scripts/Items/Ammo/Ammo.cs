using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision))]
public class Ammo : Item<AmmoData>
{
    [SerializeField]
    private Rigidbody rb;
    private float damage = 100.0f;

    public void Start()
    {
        if (rb == null)
        {
            rb = this.transform.GetComponent<Rigidbody>();

            if (rb == null)
            {
                Debug.Log("Ammo Components not properly set or found.");
            }
        }
    }

    public void SetDamage(float weaponBaseDamage)
    {
        this.damage = weaponBaseDamage * this.data.damageModifier;
    }

    private float GetVelocityDamage()
    {
        float damage = 1.0f;

        if (this.rb)
        {
            //Calculate damage based on
        }

        return damage;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ammo Collision");
        Damagable damagable = other.transform.GetComponent<Damagable>();

        if (damagable)
        {
            damagable.ChangeHealth(-this.damage);
        }
    }
}
