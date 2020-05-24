using System.Linq;
using UnityEngine;

namespace mellofnd
{
    public static class SoundManager
    {
        private static GameObject s_oneShotGameObject;

        private static AudioSource s_oneShotAudioSource;

        private static float s_volume = 1f;

        public static void PlaySound(SoundType type)
        {
            if (!s_oneShotGameObject)
                s_oneShotGameObject = new GameObject("SoundFX");

            var audioSource = s_oneShotGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(GetAudioClip(type), s_volume);
        }

        public static void PlaySound(SoundType type, Vector3 position)
        {
            AudioSource.PlayClipAtPoint(GetAudioClip(type), position, s_volume);
        }

        public static void PlaySound(SoundType type, float volume)
        {
            if (!s_oneShotGameObject)
                s_oneShotGameObject = new GameObject("SoundFX");

            var audioSource = s_oneShotGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(GetAudioClip(type), volume);
        }

        public static void SetVolume(float value)
        {
            s_volume = value;
        }

        private static AudioClip GetAudioClip(SoundType type)
        {
            var sounds = GameAssets.Get().GetSoundClips().First(x => x.Type == type).Clips;
            return sounds[Random.Range(0, sounds.Length)];
        }
    }
}