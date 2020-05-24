using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event Action OnDied = delegate {  };
    public event Action OnDamageTaken = delegate {  };


    [SerializeField] private int m_maxHealth = 20;

    [SerializeField] private int m_health = 1;

    public int MaxHealth{
        get{return m_maxHealth;}
    }

    public int Health{
        get{return m_health;}
    }

    private void Awake()
    {
        m_health = m_maxHealth;
    }

    public void RecoverHealth(int recover)
    {
        m_health += recover;

        if (m_health < 0) m_health = 0;

        if (m_health > m_maxHealth) m_health = m_maxHealth;
    }

    public void TakeDamage(int damage)
    {
        m_health -= damage;

        if (m_health < 0) m_health = 0;

        if (m_health > m_maxHealth) m_health = m_maxHealth;

        if (m_health <= 0)
            Die();

        OnDamageTaken?.Invoke();
    }

    private void Die()
    {
        OnDied?.Invoke();
        enabled = false;
    }
}

