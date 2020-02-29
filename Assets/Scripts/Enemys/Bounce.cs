using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public Rigidbody rb;
    private float timer;
    [SerializeField]
    private float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
        {
            Debug.Log("No RigidBody connected.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * speed;

        if (timer >= 180.0f)
        {
            timer = 0.0f;
        }

        float newY = Mathf.Sin(timer);
        Vector3 movement = new Vector3(rb.transform.position.x, rb.transform.position.y + (newY * Time.deltaTime), rb.transform.position.z) - rb.transform.position;
        rb.MovePosition(movement);
        //rb.transform.Translate(movement, Space.World);

        Vector3 parentDistance = rb.transform.parent.position - rb.transform.position;

        while (parentDistance.magnitude > 0.5f)
        {
            rb.transform.Translate(parentDistance.normalized, Space.World);
        }
    }
}
