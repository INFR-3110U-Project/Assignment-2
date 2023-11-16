using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    //used for volume
    public AudioMixerGroup sfxMixerGroup;
    public AudioMixer mixer;
    public List<SFX> sounds = new List<SFX>();

    //Makes list easier to get information from
    private Dictionary<string, AudioClip> soundLibrary = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioSource> loopLibrary = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            //fills library with all sounds
            soundLibrary[sounds[i].soundName] = sounds[i].clip;
        }

        Debug.Log("Sound is working.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //add sound mid game (unused but nice to have)
    public void addSound(string name, AudioClip clip)
    {
        soundLibrary[name] = clip;
    }

    //plays a file from library
    public void playSound(string name, Vector3 pos)
    {
        //early exit if sound doesn't exist
        if (!soundLibrary.ContainsKey(name))
        {
            return;
        }

        //creates sound to play
        GameObject obj = new GameObject("Sound Effect", typeof(AudioSource));
        AudioSource source = obj.GetComponent<AudioSource>();
        source.clip = soundLibrary[name];
        source.spatialBlend = 1.0f;
        obj.transform.position = pos;
        source.outputAudioMixerGroup = sfxMixerGroup;
        source.Play();
        Destroy(obj, source.clip.length);
    }
    //for use in 2D (if needed)
    public void playSound(string name)
    {
        //early exit if sound doesn't exist
        if (!soundLibrary.ContainsKey(name))
        {
            return;
        }

        //creates sound to play
        GameObject obj = new GameObject("Sound Effect", typeof(AudioSource));
        AudioSource source = obj.GetComponent<AudioSource>();
        source.clip = soundLibrary[name];
        source.outputAudioMixerGroup = sfxMixerGroup;
        source.Play();
        Destroy(obj, source.clip.length);
    }

    //loops music
    public void playLoop(string name)
    {
        //early exit if sound doesn't exist
        if (!soundLibrary.ContainsKey(name))
        {
            return;
        }

        AudioSource source;

        //create audio source if it doens't exist yet
        if (!loopLibrary.ContainsKey(name))
        {
            GameObject obj = new GameObject(name + " loop", typeof(AudioSource));
            source = obj.GetComponent<AudioSource>();
            source.clip = soundLibrary[name];
            source.outputAudioMixerGroup = sfxMixerGroup;

            loopLibrary[name] = source;
        }
        else
        {
            source = loopLibrary[name];
        }

        //Ensure sound is looping without overlapping
        source.loop = true;
        source.Stop();
        source.Play();
    }

    public void stopLoop(string name)
    {
        //early exit if sound doesn't exist
        if (!loopLibrary.ContainsKey(name))
        {
            return;
        }

        loopLibrary[name].Stop();
    }

    public void stopAllLoops()
    {
        foreach(AudioSource source in loopLibrary.Values)
        {
            source.Stop();
        }
    }
}

//used to hold sounds
[System.Serializable]
public class SFX
{
    public AudioClip clip;
    public string soundName;
}
