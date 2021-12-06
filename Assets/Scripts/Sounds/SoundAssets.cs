using System.Collections.Generic;
using UnityEngine;

namespace Sounds
{
    public class SoundAssets : MonoBehaviour
    {
        [SerializeField] private SoundAudioClip[] soundAudioClipArray;

        public IEnumerable<SoundAudioClip> SoundAudioClipArray => soundAudioClipArray;
        
        public static SoundAssets Instance
        {
            get
            {
                if (_instanceSoundAssets == null) _instanceSoundAssets = Instantiate(Resources.Load<SoundAssets>("SoundAssets"));
                return _instanceSoundAssets;
            }
        }
        
        private static SoundAssets _instanceSoundAssets;
    }
}