using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRagDoll : MonoBehaviour
{
    #region Serialize Feilds
    [SerializeField] Collider m_navCollider = null;
    [SerializeField] Rigidbody m_holdingPin = null;
    #endregion

    public bool trigger = false;

    private void Update()
    {
        if(trigger)
        {
            trigger = false;
            OnRagdoll();
        }
    }

    void OnRagdoll()
    {
        m_navCollider.enabled = false;
        m_holdingPin.isKinematic = false;
    }
}
