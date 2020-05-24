using UnityEngine;

namespace mellofnd
{
    public class ShadowSpriteRenderer : MonoBehaviour
    {
        [SerializeField] private Color m_color = new Color(0, 0, 0, .2f);

        [SerializeField] private Vector3 m_offset = new Vector3(-.125f, -.125f, 0f);

        private SpriteRenderer m_parentSpriteRenderer;

        private Transform m_shadowContainer;

        private SpriteRenderer m_spriteRenderer;

        private void Start()
        {
            m_shadowContainer = new GameObject().transform;
            m_shadowContainer.SetParent(transform);

            m_parentSpriteRenderer = GetComponent<SpriteRenderer>();

            m_spriteRenderer = m_shadowContainer.gameObject.AddComponent<SpriteRenderer>();

            m_spriteRenderer.sprite = m_parentSpriteRenderer.sprite;
            m_spriteRenderer.sortingOrder = m_parentSpriteRenderer.sortingOrder - 1;
            m_spriteRenderer.color = m_color;

            m_shadowContainer.rotation = transform.rotation;
            m_shadowContainer.localScale = transform.localScale;
        }

        private void LateUpdate()
        {
            var position = transform.position + m_offset;
            m_shadowContainer.position = position;
            m_spriteRenderer.sprite = m_parentSpriteRenderer.sprite;
        }
    }
}