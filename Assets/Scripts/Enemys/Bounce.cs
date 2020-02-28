using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public Rigidbody rb;
    private float timer;

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
        timer += Time.deltaTime;
        float newY = Mathf.Sin(timer);
        Debug.Log(newY);
        rb.position.Set(rb.position.x, rb.position.y + newY, rb.position.z);
        //rb.MovePosition(new Vector3(rb.position.x, rb.position.y + newY, rb.position.z));
    }
}
