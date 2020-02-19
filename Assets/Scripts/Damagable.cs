using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    [SerializeField]
    private float health = 100.0f;
    [SerializeField]
    private float maxHealth = 100.0f;

    public bool IsDead { get { return this.health <= 0.0f; } }

    public UnityEvent deathEvent = new UnityEvent();

    public void ChangeHealth(float amount)
    {
        //Change health
        this.health = Mathf.Clamp(this.health + amount, 0.0f, this.maxHealth);

        Debug.Log($"Health Changed. Amount: {amount} | New Health: {this.health}");

        //If healed...
        if (amount > 0)
        {

        }
        //If damaged...
        else if (amount < 0)
        {

        }

        if (this.IsDead)
        {
            Debug.Log("Dead");
            deathEvent.Invoke();
        }
    }
}
