using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float m_maxDistance = 18f;

    [SerializeField] private float m_minDistance = 10f;

    [SerializeField] private float m_zoomInSpeed = 3f;

    [SerializeField] private float m_FOVSpeed = 1.5f;
    
    [SerializeField] private float m_zoomOutSpeed = 3f;

    private CinemachineVirtualCamera m_virtualCamera;

    private float m_currentZoom;

    private CinemachineFramingTransposer m_frammingComposer;

    [SerializeField] private float m_maxFOV = 90;
    
    [SerializeField] private float m_minFOV = 60;

    private CinemachineImpulseSource m_impulseSource;

    [SerializeField] private float m_shakeForce = 2f;

    private void Awake()
    {
        m_virtualCamera = GetComponent<CinemachineVirtualCamera>();
        m_frammingComposer = m_virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        m_impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void LateUpdate()
    {
        var inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;


        if (inputVector.magnitude > .1)
        {
            m_currentZoom = Mathf.Lerp(m_currentZoom, inputVector.sqrMagnitude, m_zoomInSpeed * Time.deltaTime);
        }
        else
            m_currentZoom = Mathf.Lerp(m_currentZoom, 0, m_zoomOutSpeed * Time.deltaTime);

        if (!Input.GetKey(KeyCode.Space))
            m_currentZoom = Mathf.Lerp(m_currentZoom, 1, m_zoomInSpeed * Time.deltaTime);
        
        var newFOV = m_maxFOV - m_currentZoom * (m_maxFOV - m_minFOV);
        m_virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(m_virtualCamera.m_Lens.FieldOfView, newFOV, m_FOVSpeed * Time.deltaTime);
        m_frammingComposer.m_CameraDistance = m_maxDistance - m_currentZoom * (m_maxDistance - m_minDistance);
        m_frammingComposer.m_CameraDistance =
            Mathf.Clamp(m_frammingComposer.m_CameraDistance, m_minDistance, m_maxDistance);
    }

    public void Shake()
    {
        m_impulseSource.GenerateImpulse(Vector3.one * m_shakeForce); 
    }
    
    public void SmallShake()
    {
        m_impulseSource.GenerateImpulse(Vector3.one * m_shakeForce/2f); 
    }
}