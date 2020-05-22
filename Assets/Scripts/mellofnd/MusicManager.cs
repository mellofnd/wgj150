using System.Collections.Generic;
using UnityEngine;

namespace mellofnd
{
	[RequireComponent(typeof(AudioSource))]
	public class MusicManager : MonoBehaviour
	{
		private static MusicManager s_instance;

		private AudioSource m_audioSource;

		private int m_musicIndex = -1;

		[SerializeField] private List<AudioClip> m_musicTracks = new List<AudioClip>();

		private void Awake()
		{
			if (s_instance && s_instance != this)
			{
				Destroy(gameObject);
			}
			else
			{
				s_instance = this;
				m_audioSource = GetComponent<AudioSource>();
			}
		}

		private void Start()
		{
			if (m_musicTracks.Count == 1)
			{
				m_audioSource.loop = true;
				m_audioSource.clip = m_musicTracks[0];
				m_audioSource.Play();
			}
			else
			{
				PlayNext();
			}
		}

		public static MusicManager Get()
		{
			return s_instance;
		}

		private void OnDestroy()
		{
			s_instance = null;
		}

		public void SetVolume(float value)
		{
			m_audioSource.volume = value;
		}

		public void PlayNext()
		{
			CancelInvoke(nameof(PlayNext));

			m_musicIndex++;
			m_musicIndex %= m_musicTracks.Count;

			m_audioSource.clip = m_musicTracks[m_musicIndex];
			m_audioSource.Play();

			Invoke(nameof(PlayNext), m_musicTracks[m_musicIndex].length);
		}
	}
}