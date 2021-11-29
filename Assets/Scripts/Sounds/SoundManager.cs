using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Sounds
{
    public static class SoundManager
    {
        private static GameObject _oneShotGameObject;
        private static AudioSource _oneShotAudioSource;
        private static Dictionary<Sound, float> _soundTimerDictionary;

        public static void Initialize()
        {
            _soundTimerDictionary = new Dictionary<Sound, float>
            {
                [Sound.PlayerMove] = 0f
            };
        }

        public static void PlaySound(Sound sound, bool loop)
        {
            
        }

        public static void PlaySound(Sound sound)
        {
            if(CanPlaySound(sound))
            {
                if (_oneShotAudioSource == null)
                {
                    _oneShotGameObject = new GameObject("One Shot Sound");
                    _oneShotAudioSource = _oneShotGameObject.AddComponent<AudioSource>();
                }
                _oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
            }
        }

        private static AudioClip GetAudioClip(Sound sound)
        {
            foreach (var soundAudioClip in SoundAssets.I.SoundAudioClipArray)
            {
                if (soundAudioClip.sound == sound)
                {
                    return soundAudioClip.audioClip;
                }
            }

            Debug.LogError("Sound " + sound + "not found!");
            return null;
        }

        private static bool CanPlaySound(Sound sound)
        {
            switch (sound)
            {
                default:
                    return true;
                case Sound.PlayerMove:
                    if (_soundTimerDictionary.ContainsKey(sound))
                    {
                        var lastTimePlayed = _soundTimerDictionary[sound];
                        const float playerMoveTimerMax = .35f;
                        if (lastTimePlayed + playerMoveTimerMax < Time.time)
                        {
                            _soundTimerDictionary[sound] = Time.time;
                            return true;
                        }
                        return false;
                    }
                    else return true;
            }
        }
    }
}
