using UnityEngine;

namespace mellofnd
{
	public class DestroyAfterSeconds : MonoBehaviour
	{
		[SerializeField] private float m_seconds = 1f;

		private void Start()
		{
			Destroy(gameObject, m_seconds);
		}
	}
}