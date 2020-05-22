using System.Collections.Generic;
using UnityEngine;

namespace mellofnd
{
	public class GameAssets : MonoBehaviour
	{
		private static GameAssets s_instance;

		[SerializeField] private List<SoundTypeAudioClip> m_soundClips = new List<SoundTypeAudioClip>();

		private void Awake()
		{
			s_instance = this;
		}

		public static GameAssets Get()
		{
			return s_instance;
		}

		private void OnDestroy()
		{
			s_instance = null;
		}

		public List<SoundTypeAudioClip> GetSoundClips()
		{
			return m_soundClips;
		}
	}
}