using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAssets : MonoBehaviour
{
    [SerializeField] private SoundAudioClip[] soundAudioClipArray;

    public SoundAudioClip[] SoundAudioClipArray => soundAudioClipArray;
    public static SoundAssets I
    {
        get
        {
            if (_i != null) _i = Instantiate(Resources.Load<SoundAssets>("SoundAssets"));
            return _i;
        }
    }
    private static SoundAssets _i;
}