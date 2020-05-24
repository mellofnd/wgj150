using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DefeatScreenManager : MonoBehaviour
{
    [SerializeField] private Slider m_bar;
    [SerializeField] private GameObject m_defeatScreen;
        
    // Update is called once per frame
    void Update()
    {
        if(m_bar.value == 0){
            m_defeatScreen.SetActive(true);
        }
    }

    public void RestartScene(){
        SceneManager.LoadScene(0);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
