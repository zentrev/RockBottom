using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class RadiusDamage : MonoBehaviour
{
    [SerializeField] private float tickDamage = 1.0f;
    private SphereCollider bounds;

    private void Start()
    {
        this.bounds = this.GetComponent<SphereCollider>();
        this.bounds.isTrigger = true;
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(bounds.transform.position, bounds.radius);

        foreach (Collider collider in colliders)
        {
            Damagable damagable = collider.GetComponent<Damagable>();

            if (damagable)
            {
                damagable.ChangeHealth(this.tickDamage);
            }
        }
    }
}
