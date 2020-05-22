using UnityEngine;

namespace mellofnd
{
	public class Rotator : MonoBehaviour
	{
		public float AnglesPerSecond;

		private void Update()
		{
			transform.Rotate(transform.forward * (AnglesPerSecond * Time.deltaTime));
		}
	}
}