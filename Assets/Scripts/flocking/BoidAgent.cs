using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidAgent : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 5;
    [SerializeField] private float maxSpeed = 10;
    [SerializeField] private float cohesionDist = 2;
    [SerializeField] private float avoidanceDist = 1;
    [SerializeField] private float rotationSpeed = 1;
    
    private GameObject[] mBoids;
    private float speed;
    
    private int neighbourCount;
    private float m_boundSize;
    private bool returning = false;

    void Start()
    {
        speed = initialSpeed;
        mBoids = FlockManager.GetInstance().boids;
        m_boundSize = FlockManager.GetInstance ().BoundSize;
    }
    
    void Update()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) >= m_boundSize) 
            returning = true;
        else 
            returning = false;

        if (returning) {
            Vector3 distance = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp (transform.rotation,
                                                   Quaternion.LookRotation(distance),
                                                   rotationSpeed * Time.deltaTime);
        } else {
            // if (Random.Range (0,5) < 1) ApplyRules ();
            ApplyRules ();
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
    }

    private void ApplyRules () {
        Vector3 avoidPos = Vector3.zero;
        Vector3 stirDir = Vector3.zero;
        Vector3 cohesionPos = Vector3.zero;
        Vector3 allignPos = Vector3.zero;

        float distance = 0;
        float avgSpeed = 0.1f;
        neighbourCount = 0;
        
        Vector3 goalPos = FlockManager.GetInstance ().GoalPos;

        foreach (var neighbour in mBoids)
        {
            if (neighbour != gameObject) {

                distance = Vector3.Distance (neighbour.transform.position, transform.position);
            
                if (distance <= cohesionDist) {
                    neighbourCount ++;  
                    Debug.DrawLine (transform.position,
                                    neighbour.transform.position,
                                    new Color(1, 1, 1, 0.5f));
                    cohesionPos += neighbour.transform.position;
                    allignPos += neighbour.transform.forward;
                    if (distance <= avoidanceDist) {
                        avoidPos += neighbour.transform.position;
                    }

                    avgSpeed += neighbour.GetComponent <BoidAgent> ().speed;
                } 
            }
        }

        if (neighbourCount > 0) {
            //Ajuste da direção de coesão e alinhamento
            cohesionPos = cohesionPos/neighbourCount;
            allignPos = allignPos/neighbourCount;

            //Ajuste da velocidade média do agente
            speed = avgSpeed / neighbourCount;
            speed = Mathf.Clamp (speed, 0, maxSpeed);

            //Correção do vetor de movimento do agente
            stirDir = (cohesionPos + avoidPos + allignPos + goalPos) - transform.position;
        } else 
        {
            stirDir = goalPos - transform.position;
        }
            
            transform.rotation = Quaternion.Slerp (transform.rotation,
                                                   Quaternion.LookRotation(stirDir),
                                                   Time.deltaTime * rotationSpeed);

    }
}
