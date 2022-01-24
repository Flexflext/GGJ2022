using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixerGroup mainMixer;
    [SerializeField] private AudioMixerGroup sfxMixer;
    [SerializeField] private AudioMixerGroup ambientMixer;
    [SerializeField] private AudioMixerGroup musicMixer;
    private float volume;

    private void Awake()
    {
        Instance = this;
        
    }

    private void Start()
    {
        StartCoroutine(C_SoundDelay());

        mainMixer.audioMixer.GetFloat("MasterVolume", out volume);
        ChangeVolume(volume);
    }

    private IEnumerator C_SoundDelay()
    {
        yield return new WaitForSeconds(0.3f);

        GetComponent<PersonalSoundManager>().PlaySound(false, ESoundTypes.Walk);
    }

    public AudioMixerGroup GetMixerGroup(EAudioType _type)
    {
        switch (_type)
        {
            case EAudioType.SFX:
                return sfxMixer;
            case EAudioType.Music:
                return musicMixer;
            case EAudioType.Ambient:
                return ambientMixer;
            default:
                return sfxMixer;
        }
    }

    public void ChangeVolume(float _value)
    {
        mainMixer.audioMixer.SetFloat("MasterVolume", Mathf.Log10(_value) * 20);
    }

}
