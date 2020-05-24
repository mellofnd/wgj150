using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugCode : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
}