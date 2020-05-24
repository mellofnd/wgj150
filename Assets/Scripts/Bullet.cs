using mellofnd;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage { get; set; }

    public float MoveSpeed { get; set; }

    private Rigidbody m_rigidbody;

    [SerializeField] private ParticleSystem m_hitParticles;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        gameObject.AddComponent<DestroyAfterSeconds>().Seconds = 10;
    }

    private void FixedUpdate()
    {
        m_rigidbody.velocity = transform.forward * MoveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<HealthSystem>();
        if (health) health.TakeDamage(Damage);

        var particleModule = Instantiate(m_hitParticles, transform.position, Quaternion.identity).main;
        particleModule.startColor = new ParticleSystem.MinMaxGradient(GetComponent<Renderer>().material.color, GetComponent<Renderer>().material.color);
        
        Destroy(gameObject);
    }
}