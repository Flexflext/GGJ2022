using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum ESoundTypes
{
    Jump,
    Walk,
    Slow,
    Platform,
    Wall,
    Gulp,
    Death,
    Splash,
    WallMove,
    PlatformMove,
    FireActivate
}

public enum EAudioType
{
    SFX,
    Music,
    Ambient
}

public class PersonalSoundManager : MonoBehaviour
{
    [System.Serializable]
    struct Sound
    {
        public ESoundTypes SoundType;
        [Range(0,1)]
        public float Volume;
        [Range(0, 1)]
        public float Pitch;
        public bool Loop;
        public EAudioType SourceType;
        public AudioClip StaticSound;
        public AudioClip[] RandomSounds;
    }

    struct SourceSound
    {
        public AudioSource Source;
        public ESoundTypes SoundType;
    }

    [SerializeField] private Sound[] allSounds;

    [SerializeField] private SourceSound[] allSources;

    private int idx;

    private void Start()
    {
        FillSources();
    }

    private void FillSources()
    {
        allSources = new SourceSound[allSounds.Length];

        for (int i = 0; i < allSources.Length; i++)
        {
            FillSource(ref allSources[i], allSounds[i]);
            Debug.Log("HUHU" + i);
        }
    }

    private void FillSource(ref SourceSound _source, Sound _type)
    {
        _source.SoundType = _type.SoundType;
        _source.Source = this.gameObject.AddComponent<AudioSource>();

        _source.Source.volume = _type.Volume;
        _source.Source.pitch = _type.Pitch;
        _source.Source.loop = _type.Loop;

        _source.Source.outputAudioMixerGroup = AudioManager.Instance.GetMixerGroup(_type.SourceType);
    }

    public void PlaySound(bool _random, ESoundTypes _soundtype)
    {
        for (int i = 0; i < allSources.Length; i++)
        {
            if (_soundtype == allSources[i].SoundType)
            {
                idx = i;
                break;
            }
        }

        if (_random)
        {
            allSources[idx].Source.PlayOneShot(allSounds[idx].RandomSounds[Random.Range(0, allSounds[idx].RandomSounds.Length)]);
        }
        else
        {
            allSources[idx].Source.PlayOneShot(allSounds[idx].StaticSound);
        }
    }

    public void Stop(ESoundTypes _soundtype)
    {
        for (int i = 0; i < allSources.Length; i++)
        {
            if (_soundtype == allSources[i].SoundType)
            {
                allSources[i].Source.Stop();
                break;
            }
        }
    }
}
