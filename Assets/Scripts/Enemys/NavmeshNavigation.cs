using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshNavigation : MonoBehaviour
{
    #region Serialize Fields
    [Header("Movement")]
    [SerializeField] private float m_maxSpeed = 10.0f;
    [SerializeField] private float m_accleration = 1.0f;
    [SerializeField] private float m_rotationAccleration = 10.0f;

    [SerializeField] private Transform m_target = null;
    [SerializeField] private float m_pathGenRate = 1.0f;
    [SerializeField] [Range(0.0f, 10.0f)] float m_nodeTolorance = 2.0f;

    [Header("Ragdoll")]
    [SerializeField] Collider m_navCollider = null;
    [SerializeField] Rigidbody m_holdingPin = null;

    [Header("Gameplay")]
    [SerializeField] int gold = 100;
    [SerializeField] float damage = 5;
    [SerializeField] float fireRate = 1;
    float ticker = 0;
    #endregion

    #region Private Varibles

    private Rigidbody m_rb = null;

    private NavMeshPath m_navPath = null;
    private int m_navCorner = 0;

    private Vector3 m_direction = Vector3.zero;
    private Vector3 m_velocity = Vector3.zero;

    private float m_pathGenTicker = 0.0f;

    public Transform Target { get => m_target; set => m_target = value; }
    #endregion

    void Start()
    {
        TryGetComponent(out m_rb);

        if (m_holdingPin == null) Debug.LogError($"{gameObject.name} NavmeshNavigation is missing holding pin");
        else m_holdingPin.isKinematic = true;
    }

    void Update()
    {
        GeneratePath();

        m_direction = transform.forward;
        if (m_navPath != null)
        {
            if (m_navPath.status == NavMeshPathStatus.PathInvalid)
            {
                m_direction = Vector3.zero;
            }
            else if ((transform.position - m_navPath.corners[m_navCorner]).magnitude < m_nodeTolorance)
            {
                if (m_navPath.corners.Length > m_navCorner) m_navCorner++;
                if (m_navCorner == m_navPath.corners.Length)
                {
                    m_direction = Vector3.zero;
                    m_navCorner = m_navPath.corners.Length - 1;
                }
            }

            UpdateLook();

            UpdateMovment();
        }
    }

    private void UpdateLook()
    {
        if (m_navPath.status != NavMeshPathStatus.PathInvalid)
        {
            Quaternion newRot = Quaternion.LookRotation(new Vector3(m_navPath.corners[m_navCorner].x, transform.position.y, m_navPath.corners[m_navCorner].z) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRot, m_rotationAccleration);
        }
    }

    private void UpdateMovment()
    {
        Vector3 target = m_direction * m_maxSpeed * Time.deltaTime;

        m_velocity = Vector3.Lerp(m_velocity, target, m_accleration * Time.deltaTime);
        transform.position += m_velocity;
    }

    private void GeneratePath()
    {
        m_pathGenTicker += Time.deltaTime;
        if (m_pathGenTicker >= m_pathGenRate)
        {
            m_pathGenTicker = 0;
            m_navPath = new NavMeshPath();

            m_navCorner = 0;
            if (NavMesh.CalculatePath(transform.position, Target.position, NavMesh.AllAreas, m_navPath))
            {
//                Debug.Log("Worked");
            }
            else
            {
                //Debug.Log(m_navPath.status);
                if(TryGetComponent(out Damagable d))
                {
                    d.Annihilate();
                }
            }
            //Debug.Log(m_navPath.status);

//            Debug.Log(m_navPath.corners.Length);


            for (int i = 0; i < m_navPath.corners.Length - 1; i++)
            {
                Debug.DrawLine(m_navPath.corners[i], m_navPath.corners[i + 1], Color.red, 10.0f);
                //Debug.Log(m_navPath.corners[i] + " : " + m_navPath.corners[i + 1]);
            }
        }
    }

    public void ActivateRagdoll()
    {
        m_navCollider.enabled = false;
        m_holdingPin.isKinematic = false;
        Destroy(gameObject, 10.0f);
    }

    public void AddGold()
    {
        GameManager.Instance.gold = gold;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "door")
        {
            if(other.TryGetComponent(out Damagable d))
            {
                ticker += Time.deltaTime;
                if(ticker >= fireRate)
                {
                    ticker = 0;
                    d.ChangeHealth(-damage);
                }
            }
        }
    }
}
