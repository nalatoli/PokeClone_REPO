using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/* Sound Handler */
public class Sound
{
    /* Adjustable Parameters */
    public string name;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume = 0.7F;
}

[System.Serializable]
public class Music
{
    /* Adjustable Parameters */
    public Sound sound;
    public bool dyanamicLoop; 
    public float loopStart;
    public float loopEnd;
}

public class SoundManager : MonoBehaviour
{
    /* Public Instance */
    public static SoundManager instance;

    /* Public Parameters */
    [SerializeField]
    public Sound[] sounds;

    /* Private Parameters */
    private AudioSource soundSource;
    private AudioSource musicSource;
    private IEnumerator running;

    void Awake ()
    {
        /* Initialize Instance (Throw Error If More Than One) */
        if (instance != null)
            Debug.LogError("More than one Sound Manager in this scene.");
        else
            instance = this;

        /* Get Components */
        AudioSource[] sources = GetComponents<AudioSource>();
        soundSource = sources[0];
        musicSource = sources[1];
    }

   public void PlaySound(string _name)
   {
        /* Find Sound Effect */
        for (int i = 0; i < sounds.Length; i++) {
            if (sounds[i].name == _name)
            {
                /* Play Sound and Return */
                soundSource.volume = sounds[i].volume;
                soundSource.clip = sounds[i].clip;
                soundSource.Play();
                return;
            }
        }      
    }

    public IEnumerator PlayMusic(Music music)
    {
        /* Adjust Parameter */
        while (musicSource.volume != music.sound.volume) {
            musicSource.volume = Mathf.MoveTowards(musicSource.volume, music.sound.volume, Time.deltaTime);
            yield return null;
        }

        /* Play Music If Different */
        if(musicSource.clip != music.sound.clip)
        {
            /* Stop Current Music */
            if (running != null) StopCoroutine(running);

            /* Start New Music */
            running = PlayMusicLoop(music);
            StartCoroutine(running);
        }
    }

    private IEnumerator PlayMusicLoop(Music music)
    {
        /* Start Song At Beginining */
        musicSource.Stop();
        musicSource.clip = music.sound.clip;
        musicSource.time = 0;
        musicSource.Play();

        /* While Coroutine is Running */
        while (true)
        {
            /* Wait Until The End of Loop */
            yield return new WaitUntil(() => ((musicSource.time >= music.loopEnd) && music.dyanamicLoop));

            /* Start At The Beginning of the Loop */
            musicSource.time = music.loopStart;
        }
    }
}
