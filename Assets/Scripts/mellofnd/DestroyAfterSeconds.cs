using UnityEngine;

namespace mellofnd
{
    public class DestroyAfterSeconds : MonoBehaviour
    {
        public float Seconds = 1f;

        private void Start()
        {
            Destroy(gameObject, Seconds);
        }
    }
}