using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    [SerializeField] private List<AudioClip> sounds = null;
    private Dictionary<string, AudioClip> soundsDic = new Dictionary<string, AudioClip>();
    public static SoundSystem instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (AudioClip sound in sounds)
        {  
            soundsDic.Add(sound.name, sound);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioClip PlayAudio(string nameAudio)
    {
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSpec in audios)
        {
            if (audioSpec.clip == null)
            {
                audioSpec.clip = soundsDic[nameAudio];
                audioSpec.PlayOneShot(soundsDic[nameAudio]);
                return soundsDic[nameAudio];
            }
        }
        
        return soundsDic[nameAudio];
    }

    public void PauseAudios()
    {
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioActual in audios)
        {
            audioActual.Pause();
        }
    }
}
