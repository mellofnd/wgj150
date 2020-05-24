//code based on EngiGame's solution: https://www.youtube.com/watch?v=ErJgQY5smnw

using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    private static bool m_applicationIsQuitting = false;
    public static T GetInstance () {
        if (m_applicationIsQuitting) return null;
        if (instance == null) { 
            instance = FindObjectOfType <T> ();

            if (instance == null) {
                GameObject go = new GameObject ();
                go.name = typeof (T).Name;
                instance = go.AddComponent<T> ();
            }
        }
        return instance;
    }

    protected virtual void Awake() {
        if (instance == null) {
            instance = this as T;
            DontDestroyOnLoad (gameObject);
        }
        else if (instance != this) {
            Destroy (gameObject);
        }
        else DontDestroyOnLoad (gameObject);
    }

    private void OnApplicationQuit() {
        m_applicationIsQuitting = true;
    }
}
