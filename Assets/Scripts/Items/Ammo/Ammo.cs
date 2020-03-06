using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Ammo : Item<AmmoData>
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Transform parent;
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

        if(parent == null)
        {
            parent = transform.parent;
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
        Damagable damagable = other.transform.GetComponentInParent<Damagable>();

        if (damagable && !damagable.IsDead)
        {
            damagable.ChangeHealth(-this.damage, this.transform.position);
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
            parent.transform.SetParent(other.transform);
            this.transform.LookAt(hit.point);
            this.transform.position = hit.point;

            Collider[] colliders = parent.GetComponentsInChildren<Collider>();
            foreach(Collider col in colliders)
            {
                col.enabled = false;
            }

            if(hit.collider.TryGetComponent<Rigidbody>(out Rigidbody otherRB))
            {
                otherRB.AddForceAtPosition(Vector3.back * 100, hit.point, ForceMode.Impulse);
            }

            Debug.Log("Ammo Stuck");
        }
    }
}
