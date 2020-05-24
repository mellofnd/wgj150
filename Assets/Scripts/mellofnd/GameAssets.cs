using System.Collections.Generic;
using UnityEngine;

namespace mellofnd
{
    public class GameAssets : MonoBehaviour
    {
        [SerializeField] private List<SoundTypeAudioClip> m_soundClips = new List<SoundTypeAudioClip>();

        private static GameAssets s_instance;

        private void Awake()
        {
            s_instance = this;
        }

        private void OnDestroy()
        {
            s_instance = null;
        }

        public static GameAssets Get()
        {
            return s_instance;
        }

        public List<SoundTypeAudioClip> GetSoundClips()
        {
            return m_soundClips;
        }
    }
}