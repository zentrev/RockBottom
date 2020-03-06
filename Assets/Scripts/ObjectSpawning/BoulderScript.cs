using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoulderScript : MonoBehaviour
{
    [SerializeField] float lifetime = 5.0f;
    private bool isThrown = false;
    private Animator animator = null;
    private Rigidbody rb = null;
    public ObjectSpawn spawner = null;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.SetFloat("Timer", lifetime);
        rb = gameObject.GetComponent<Rigidbody>();
        spawner = gameObject.GetComponentInParent<ObjectSpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isThrown)
        {
            lifetime -= Time.deltaTime;
            if (lifetime <= -1.0f)
            {
                Destroy(gameObject);
            }
            animator.SetFloat("Timer", lifetime);
        }
    }

    public void setThrown()
    {
        isThrown = true;
        rb.isKinematic = false;
    }

    public void spawnNewBoulder()
    {
        spawner.spawnGrabbable();
    }


}
