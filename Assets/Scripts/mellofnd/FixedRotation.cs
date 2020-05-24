using UnityEngine;

namespace mellofnd
{
    public class FixedRotation : MonoBehaviour
    {
        private Quaternion m_rotation;

        private void Awake()
        {
            m_rotation = transform.rotation;
        }

        private void LateUpdate()
        {
            transform.rotation = m_rotation;
        }
    }
}