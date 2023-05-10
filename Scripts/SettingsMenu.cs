using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// music 
public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
