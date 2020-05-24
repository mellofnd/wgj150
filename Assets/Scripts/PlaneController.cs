using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private float m_horizontalSpeed;

    [SerializeField] private float m_moveSpeedMax = 16f;

    [SerializeField] private float m_moveSpeedMin = 12f;

    [SerializeField] private float m_rollLerpSpeed = 1f;

    [SerializeField] private float m_verticalSpeed;

    [SerializeField] private Image m_hitImage;
    
    public static Transform Instance { get; set; }

    private Rigidbody m_rigidbody;

    private float m_currentRoll;

    private Vector3 m_pitchYawRoll;

    private ShootingController m_shootingController;

    private float m_currentMoveSpeed;

    private bool m_canMove = true;

    [SerializeField] private float m_knockbackDuration = .25f;

    private float m_stuckCounter;

    private Vector3 m_lastStuckPosition;

    private CameraController m_camera;

    private HealthSystem m_health;

    [SerializeField] private ParticleSystem m_circleParticles;

    private void Awake()
    {
        Instance = transform;
        m_rigidbody = GetComponent<Rigidbody>();
        m_pitchYawRoll = transform.localEulerAngles;
        m_health = GetComponent<HealthSystem>();
        m_shootingController = GetComponent<ShootingController>();
    }

    private void Start()
    {
        m_camera = FindObjectOfType<CameraController>();
    }

    private void OnEnable()
    {
        m_health.OnDied += HandleDeath;
        m_health.OnDamageTaken += HandleDamageTaken;
    }

    private void HandleDamageTaken()
    {
        m_camera.Shake();
        var color = m_hitImage.color;
        color.a = 1;
        m_hitImage.color = color;
        color.a = 0;
        m_hitImage.DOColor(color, .3f).SetEase(Ease.InQuad);
    }

    private void HandleDeath()
    {
        //SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        Instance = null;
    }

    private void FixedUpdate()
    {
        if (m_canMove)
            m_rigidbody.velocity = transform.forward * m_currentMoveSpeed;

        AntiStuck();
    }
    
    private void AntiStuck()
    {
        if (Vector3.Distance(m_lastStuckPosition, transform.position) < .1f)
            m_stuckCounter += Time.fixedDeltaTime;
        else
            m_stuckCounter = 0;

        if (m_stuckCounter >= 1f)
        {
            var newMovePosition = transform.position + Random.insideUnitSphere * 5;
            newMovePosition.y = Mathf.Abs(newMovePosition.y);
            m_rigidbody.MovePosition(newMovePosition);
            m_stuckCounter = 0;
        }
        
        m_lastStuckPosition = transform.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        m_health.TakeDamage(1);
        var hitDirection = other.contacts[0].normal;
        StartCoroutine(KnockBackRoutine(hitDirection));
    }
    
    private IEnumerator KnockBackRoutine(Vector3 hitDirection)
    {
        m_canMove = false;
        m_rigidbody.velocity = (hitDirection + transform.forward).normalized * m_moveSpeedMin;
        
        yield return new WaitForSeconds(m_knockbackDuration);
    
        m_canMove = true;
    }

    private void Update()
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            m_shootingController.Shoot();
            m_currentMoveSpeed = m_moveSpeedMin;
        }
        else
        {
            m_currentMoveSpeed = m_moveSpeedMax;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_circleParticles.Play();
            m_camera.Shake();
        }
        
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");

        float roll = 0;
        if (horizontalInput > 0)
            roll = -45f;
        else if (horizontalInput < 0)
            roll = 45f;

        m_pitchYawRoll.z = Mathf.Lerp(m_pitchYawRoll.z, roll, m_rollLerpSpeed * Time.deltaTime);
        m_pitchYawRoll.y += horizontalInput * Time.deltaTime * m_horizontalSpeed;
        m_pitchYawRoll.x += verticalInput * Time.deltaTime * m_verticalSpeed;

        transform.eulerAngles = m_pitchYawRoll;
    }
}