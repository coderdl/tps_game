using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructale : MonoBehaviour
{
    [SerializeField] float hitPoints;

    public event System.Action OnDeath;
    public event System.Action OnDamageReceived;

    public float damageTaken { get; set; }

    public float HitPointsRemaing
    {
        get
        {
            return hitPoints - damageTaken;
        }
    }

    public bool IsAlive {
        get
        {
            return HitPointsRemaing > 0;
        }
    }

    public virtual void Die()
    {
        if (!IsAlive)
            return;
        if(OnDeath != null)
        {
            OnDeath();
        }
    }

    public virtual void TakeDamage(float amount)
    {
        damageTaken += amount;

        if(OnDamageReceived != null)
        {
            OnDamageReceived();
        }
        
        if(HitPointsRemaing <= 0)
        {
            Die();
        }
    }

    public void Reset()
    {
        damageTaken = 0;
    }
}
