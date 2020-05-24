using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem m_target;
    private Slider m_bar;
    // Start is called before the first frame update
    void Start()
    {
        m_bar = GetComponent<Slider>();
        //m_bar.highValue = m_target.MaxHealth;
        m_bar.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        m_bar.value = (float)m_target.Health/m_target.MaxHealth;
    }
}
