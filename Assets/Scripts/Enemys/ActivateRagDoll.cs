using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRagDoll : MonoBehaviour
{
    #region Serialize Feilds
    [SerializeField] Collider m_navCollider = null;
    [SerializeField] Rigidbody m_holdingPin = null;
    #endregion

    // Remove HidInInspector to allow for editor triggering
    [HideInInspector] public bool trigger = false;

    private void Start()
    {
        if (m_holdingPin == null) Debug.LogError($"{gameObject.name} ActivateRagDoll is missing holding pin");
    }

    private void Update()
    {
        if(trigger)
        {
            trigger = false;
            ActivateRagdoll();
        }
    }

    public void ActivateRagdoll()
    {
        m_navCollider.enabled = false;
        m_holdingPin.isKinematic = false;
    }
}
