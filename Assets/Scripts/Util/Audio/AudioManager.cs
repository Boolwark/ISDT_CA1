using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.ObjectPooling;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    //sfx stands for sound effects
    public Sound[] musicSounds, sfxSounds; 

    public AudioSource musicSource, sfxSource;
    // Start is called before the first frame update
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
            return;
        }

        musicSource.clip = s.clip;
        musicSource.Play();
        musicSource.loop = true;
    }
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
            return;
        }
        Debug.Log("PlAYING" + name);

        sfxSource.clip = s.clip;
        sfxSource.PlayOneShot(sfxSource.clip);
    }

    private void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}