using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private Bullet m_bullet;

    [SerializeField] private Transform[] m_bulletSpawningPoints;

    [SerializeField] private float m_cooldown = .25f;

    [SerializeField] private int m_damage;

    [SerializeField] private float m_shootSpeed;

    private float m_nextShootTime;

    public void Shoot()
    {
        if (Time.time < m_nextShootTime) return;

        m_nextShootTime = Time.time + m_cooldown;
        foreach (var point in m_bulletSpawningPoints)
        {
            var bullet = Instantiate(m_bullet, point.position, point.transform.rotation);
            bullet.MoveSpeed = m_shootSpeed;
            bullet.Damage = m_damage;    
        }
    }
}