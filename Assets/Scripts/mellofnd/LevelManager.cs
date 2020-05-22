using UnityEngine;
using UnityEngine.SceneManagement;

namespace mellofnd
{
	public class LevelManager : MonoBehaviour
	{
		private static LevelManager s_instance;

		private void Awake()
		{
			s_instance = this;
		}

		public static LevelManager Get()
		{
			return s_instance;
		}

		private void OnDestroy()
		{
			s_instance = null;
		}

		public void LoadNext()
		{
			var index = SceneManager.GetActiveScene().buildIndex + 1;
			index %= SceneManager.sceneCountInBuildSettings;
			SceneManager.LoadScene(index);
		}

		public void Reload()
		{
			var index = SceneManager.GetActiveScene().buildIndex;
			SceneManager.LoadScene(index);
		}

		public void ReloadGame()
		{
			SceneManager.LoadScene(0);
		}
	}
}