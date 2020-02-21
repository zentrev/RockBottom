using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlyForwardArrow : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 100.0f)] float m_force = 5.0f;
    private Rigidbody m_rb = null;

    public bool trigger = false;

    void Start()
    {
        TryGetComponent(out m_rb);
        m_rb.isKinematic = true;
    }

    private void Update()
    {
        if(trigger)
        {
            trigger = false;
            m_rb.isKinematic = false;
            m_rb.AddForce(transform.forward * m_force, ForceMode.Impulse);

        }
    }


}
