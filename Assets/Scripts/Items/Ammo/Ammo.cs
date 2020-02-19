using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision), typeof(Rigidbody))]
public class Ammo : Item<AmmoData>
{
    private Collision collision;
    private Rigidbody rb;
    private float damage = 100.0f;

    public void Start()
    {
        if (collision == null)
        {
            collision = this.transform.GetComponent<Collision>();
        }

        if (rb == null)
        {
            rb = this.transform.GetComponent<Rigidbody>();
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

    public void OnCollisionEnter(Collision other)
    {
        Debug.Log("Ammo Collision");
        Damagable damagable = other.transform.GetComponent<Damagable>();

        if (damagable)
        {
            damagable.ChangeHealth(-this.damage);
        }
    }
}
