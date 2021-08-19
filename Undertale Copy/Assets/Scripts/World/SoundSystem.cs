using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    [SerializeField] private List<AudioClip> sounds = null;
    private Dictionary<string, AudioClip> soundsDic = new Dictionary<string, AudioClip>();

    private AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
                audioSpec.PlayOneShot(soundsDic[nameAudio]);
            }
        }
        
        return soundsDic[nameAudio];
    }

    public void PauseAudios()
    {
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in audios)
        {
            audio.Pause();
        }
    }
}
