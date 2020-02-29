using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public Rigidbody bounceRoot;
    private float timer;
    [SerializeField]
    private float speed = 10.0f, parentVerticalOffset = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        if (bounceRoot == null)
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
        Vector3 movement = new Vector3(bounceRoot.transform.position.x, bounceRoot.transform.position.y + (newY * Time.deltaTime), bounceRoot.transform.position.z) - bounceRoot.transform.position;
        bounceRoot.transform.Translate(movement, Space.World);

        Vector3 parentPosition = bounceRoot.transform.parent.position;
        Vector3 parentDistance = new Vector3(parentPosition.x, parentPosition.y + this.parentVerticalOffset, parentPosition.z) - bounceRoot.transform.position;

        //Restrict bounce (prevents slowly drifing up/down due to slight timing offsets)
        if (parentDistance.magnitude > 0.6f)
        {
            bounceRoot.transform.Translate(parentDistance.normalized * (parentDistance.magnitude - 0.5f), Space.World);
        }
    }
}
