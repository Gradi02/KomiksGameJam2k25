using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public sounds[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }


        DontDestroyOnLoad(gameObject);

        foreach (sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.playOnAwake = s.playonawake;
        }
    }

    public void ChangeVolume(float change)
    {
        foreach (sounds s in sounds)
        {
            s.source.volume = change;
        }
    }

    public void Play(string name)
    {
        sounds s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " wasn't found!");
            return;
        }

        s.source.Play();
    }

    public void Stop(string name)
    {
        sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " wasn't found!");
            return;
        }
        Debug.Log(s);
        Debug.Log(s.source);
        Debug.Log(name);
        s.source.Stop();
    }

    public string PlayLoop(string soundName)
    {
        sounds sound = Array.Find(sounds, s => s.name == soundName);
        if (sound == null)
        {
            Debug.LogWarning($"Sound: {soundName} not found!");
            return "";
        }
        Debug.Log(sound);
        Debug.Log(sound.source);
        Debug.Log(soundName);
        sound.source.loop = true;
        sound.source.Play();
        return soundName;
    }
}



[System.Serializable]
public class sounds
{
    public string name;

    public AudioClip clip;

    public bool playonawake;

    [Range(0f, 1f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;
}