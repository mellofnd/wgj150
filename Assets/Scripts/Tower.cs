using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Tower : MonoBehaviour
{
    public static List<Tower> ActiveTowers = new List<Tower>();
    
    public TowerState State;

    [SerializeField] private float m_lookSpeed = 1f;

    [SerializeField] private LayerMask m_playerLayer;

    [SerializeField] private float m_sightRadius;

    private ShootingController m_shootingController;

    [SerializeField] private ParticleSystem m_explosionParticles;

    [SerializeField] private Transform m_towerEye;

    public static event Action<Tower> OnTowerDestroyed = delegate(Tower tower) {  };
    
    private void Awake()
    {
        m_shootingController = GetComponent<ShootingController>();
    }

    private void OnEnable()
    {
        GetComponent<HealthSystem>().OnDied += HandleDeath;
        ActiveTowers.Add(this);
    }

    private void OnDestroy()
    {
        ActiveTowers.Remove(this);
    }

    private void HandleDeath()
    {
        OnTowerDestroyed?.Invoke(this);
        Instantiate(m_explosionParticles, m_towerEye.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        State = Physics.OverlapSphere(m_towerEye.position, m_sightRadius, m_playerLayer).Length > 0
            ? TowerState.Active
            : TowerState.Idle;
    }

    private void Update()
    {
        switch (State)
        {
            case TowerState.Idle:
                return;

            case TowerState.Active:
                RotateToPlayer();
                TryShoot();
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(m_towerEye.position, m_sightRadius);
    }

    private void RotateToPlayer()
    {
        var direction = (PlaneController.Instance.position - m_towerEye.position).normalized;
        var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        m_towerEye.rotation = Quaternion.Lerp(m_towerEye.rotation, lookRotation, m_lookSpeed * Time.deltaTime);
    }

    private void TryShoot()
    {
        //Check line of sight
        var direction = (PlaneController.Instance.transform.position - m_towerEye.position).normalized;
        var player = Physics.Raycast(m_towerEye.position, direction, m_playerLayer);

        if (!player) return;

        m_shootingController.Shoot();
    }
}

public enum TowerState
{
    Idle,

    Active
}