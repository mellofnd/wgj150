using System.Collections;
using UnityEngine;

namespace mellofnd
{
	public class TimeManager : MonoBehaviour
	{
		private const float TIME_SCALE = 1f;

		private const float FIXED_DELTA_TIME = .02f;

		private static TimeManager s_instance;

		private void Awake()
		{
			s_instance = this;
		}

		public static TimeManager Get()
		{
			return s_instance;
		}

		private void OnDestroy()
		{
			Time.timeScale = TIME_SCALE;
			Time.fixedDeltaTime = FIXED_DELTA_TIME;
			s_instance = null;
		}

		public void FreezeTime(float duration = .2f)
		{
			StartCoroutine(FreezeTimeRoutine(duration));
		}

		private static IEnumerator FreezeTimeRoutine(float duration)
		{
			Time.timeScale = .02f;
			Time.fixedDeltaTime = Time.timeScale * FIXED_DELTA_TIME;

			yield return new WaitForSeconds(duration);

			Time.timeScale = TIME_SCALE;
			Time.fixedDeltaTime = FIXED_DELTA_TIME;
		}
	}
}