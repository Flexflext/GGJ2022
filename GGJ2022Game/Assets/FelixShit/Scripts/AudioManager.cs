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

    private float masterVolume;
    

    private void Awake()
    {
        Instance = this;

        StartCoroutine(C_SoundDelay());
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
        masterVolume = _value;
        mainMixer.audioMixer.SetFloat("MasterVolume", Mathf.Log10(_value) * 20);
    }

}
