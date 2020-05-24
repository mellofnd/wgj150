using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int m_recover;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Plane"){
            var health = other.GetComponent<HealthSystem>();
            health.RecoverHealth(m_recover);                    
            Destroy(gameObject);
        }         
    }
}
