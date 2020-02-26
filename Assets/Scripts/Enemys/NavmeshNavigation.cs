using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshNavigation : MonoBehaviour
{
    #region Serialize Fields
    [Header("Movment")]
    [SerializeField] private float m_maxSpeed = 10.0f;
    [SerializeField] private float m_accleration = 1.0f;
    [SerializeField] private float m_rotationAccleration = 10.0f;

    [SerializeField] private Transform m_target = null;
    [SerializeField] private float m_pathGenRate = 1.0f;

    #endregion

    #region Private Varibles

    private Rigidbody m_rb = null;

    private NavMeshPath m_navPath = null;
    private int m_navCorner = 0;

    private Vector3 m_direction = Vector3.zero;
    private Vector3 m_velocity = Vector3.zero;

    private float m_pathGenTicker = 0.0f;
    #endregion

    void Start()
    {
        TryGetComponent(out m_rb);
    }

    void Update()
    {
        GeneratePath();


        if ((transform.position - m_navPath.corners[m_navCorner]).magnitude < 2f)
        {
            if (m_navPath.corners.Length > m_navCorner) m_navCorner++;
            if (m_navCorner == m_navPath.corners.Length) m_direction = Vector3.zero;
        }

        UpdateLook();

        UpdateMovment();
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
        m_direction = transform.forward;

        m_direction = m_direction.normalized;

        Vector3 target = m_direction * m_maxSpeed * Time.deltaTime;

        m_velocity = Vector3.Lerp(m_velocity, target, m_accleration * Time.deltaTime);
        Debug.Log(m_velocity);
        transform.position += m_velocity;
    }

    private void GeneratePath()
    {
        m_pathGenTicker += Time.deltaTime;
        if (m_pathGenTicker >= m_pathGenRate)
        {
            m_navPath = new NavMeshPath();

            m_navCorner = 0;
            if (NavMesh.CalculatePath(transform.position, m_target.position, NavMesh.AllAreas, m_navPath))
            {
                Debug.Log("Worked");
            }
            else
            {
                Debug.Log("Failed");
            }
            Debug.Log(m_navPath.status);

            Debug.Log(m_navPath.corners.Length);


            for (int i = 0; i < m_navPath.corners.Length - 1; i++)
            {
                Debug.DrawLine(m_navPath.corners[i], m_navPath.corners[i + 1], Color.red, 10.0f);
                //Debug.Log(m_navPath.corners[i] + " : " + m_navPath.corners[i + 1]);
            }
        }

    }
}
