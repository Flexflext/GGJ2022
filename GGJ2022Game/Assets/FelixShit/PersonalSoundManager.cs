using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum ESoundTypes
{
    Jump,
    Walk,
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

        //public Sound()
        //{
        //    Volume = 1;
        //    Pitch = 1;
        //    SoundType = ESoundTypes.Jump;
        //    Loop = false;
        //    SourceType = EAudioType.SFX;
        //    StaticSound = null;
        //    RandomSounds = new AudioClip[0];
        //}
    }

    struct SourceSound
    {
        public AudioSource Source;
        public ESoundTypes SoundType;
    }

    [SerializeField] private Sound[] allSounds;

    private SourceSound[] allSources;

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
}
