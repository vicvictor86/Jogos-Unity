using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    [SerializeField] private List<AudioClip> sounds = null;
    private Dictionary<string, AudioClip> soundsDic = new Dictionary<string, AudioClip>();

    private AudioSource audioSource;    
    
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
        AudioClip audioClip = soundsDic[nameAudio];
        audioSource.PlayOneShot(soundsDic[nameAudio]);
        return soundsDic[nameAudio];
    }
}
