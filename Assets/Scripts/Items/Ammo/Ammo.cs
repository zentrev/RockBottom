using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Ammo : Item<AmmoData>
{
    [SerializeField]
    private Rigidbody rb;
    private float damage = 100.0f;

    public void Start()
    {
        if (rb == null)
        {
            if (!TryGetComponent(out rb))
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
            //Calculate damage based on velocity
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

        Collider collider = this.GetComponent<Collider>();

        Debug.Log("Collision " + collider.transform.name);

        Vector3 raycastOrigin = collider.bounds.center;
        Vector3 raycastDirection = (other.transform.position - raycastOrigin).normalized;
        Physics.Raycast(raycastOrigin, raycastDirection, out RaycastHit hit, 0.5f);

        //Stick into target
        if (hit.collider != null)
        {
            //Stop velocity
            this.rb.velocity = Vector3.zero;
            this.rb.isKinematic = true;

            //Set new parent, position, and rotation
            this.transform.SetParent(other.transform);
            this.transform.position = hit.point;
            this.transform.LookAt(hit.point);

            Debug.Log("Ammo Stuck");
        }
    }
}
