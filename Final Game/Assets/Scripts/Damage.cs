using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage = 10f; 

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerhealth pHealth = other.gameObject.GetComponent<playerhealth>();
            if (pHealth != null)
            {
                pHealth.TakeDamage(damage);
            }
        }
    }
}
