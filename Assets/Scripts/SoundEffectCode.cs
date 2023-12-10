using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;

public class SoundEffectCode : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] string mixerName;

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(mixerName, sliderValue);
    }
}
