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
    [SerializeField] private GameObject damageEffect, healEffect;
    [SerializeField] private AudioSource damageAudio, healAudio;

    public bool IsDead { get { return this.health <= 0.0f; } }

    public UnityEvent deathEvent = new UnityEvent();

    public void ChangeHealth(float amount, Vector3? location = null)
    {
        //Change health
        this.health = Mathf.Clamp(this.health + amount, 0.0f, this.maxHealth);

        Debug.Log($"Health Changed. Amount: {amount} | New Health: {this.health}");

        Vector3 spawnLocation = (location == null ? new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z) : location.Value);

        //If healed...
        if (amount > 0)
        {
            if (this.healEffect != null)
            {
                GameObject.Instantiate(this.healEffect, spawnLocation, Quaternion.identity);
            }

            if (this.healAudio != null)
            {
                this.healAudio.Play();
            }
        }
        //If damaged...
        else if (amount < 0)
        {
            if (this.damageEffect != null)
            {
                GameObject.Instantiate(this.damageEffect, spawnLocation, Quaternion.identity);
            }

            if (this.damageAudio != null)
            {
                this.damageAudio.Play();
            }
        }

        if (this.IsDead)
        {
            Debug.Log("Dead");
            deathEvent.Invoke();
            if(WaveManager.Instance != null) WaveManager.Instance.RemoveMe(this.gameObject);
        }
    }

    public void Annihilate()
    {
        ChangeHealth(-health);
    }
}
