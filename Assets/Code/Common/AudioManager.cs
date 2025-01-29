using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DywFunctions;


public enum Track
{
    Throught_the_Forest,
    The_Highs
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource[] tracks;
    public AudioMixer mixerTrack, mixerSfx;
    public float interpolationSpeed = 0;
    public int currentSongID = -1;


    [Range(0.00001f, 1f)] public float tracksVolume, sfxVolume;

    void Awake()
    {
        if (FindObjectsOfType<AudioManager>().Length > 1) Destroy(gameObject);
        if (tracks != null && tracks.Length == 0) tracks = GetComponentsInChildren<AudioSource>();
        instance = instance == null ? this : instance;


    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        Play(currentSongID, true);
    }

    // Update is called once per frame
    void Update()
    {
        Volume(tracksVolume, 0);
        Volume(sfxVolume, SoundType.SFX);
    }

    ///<summary>
    /// return true if the song is playing
    ///</summary>
    public bool IsPlaying(int trackIndex)
    {
        return tracks[trackIndex].isPlaying;
    }

    public int GetIndex(string audioName)
    {
        for (int i = 0; i < tracks.Length; i++)
        {
            if (tracks[i].name == audioName) return i;
        }
        return -1;
    }
    public void Play(string audioName, bool loop = false, bool interpolate = true)
    {
        var index = GetIndex(audioName);
        Play(index, loop, interpolate);
    }

    public void Play(Track track, bool loop = false, bool interpolate = true)
    {
        var audioName = track.ToString().Replace('_', ' ');
        var index = GetIndex(audioName);
        Play(index, loop, interpolate);
    }

    public void Play(int trackIndex, bool loop = false, bool interpolate = true)
    {
        if (currentSongID == trackIndex) return;
        if (interpolate)
        {
            StartCoroutine(TurnDownVolume(currentSongID));
            StartCoroutine(TurnUpVolume(trackIndex));
        }
        else
        {
            if (currentSongID != -1)
                tracks[currentSongID].Stop();
            tracks[trackIndex].volume = 1;
        }
        tracks[trackIndex].Play();
        tracks[trackIndex].loop = loop;
        currentSongID = trackIndex;

    }
    public void Play(string name, Vector2? origin = null)
    {
        foreach (AudioSource src in tracks)
        {
            if (src.name == name)
            {
                src.Play();
            }
        }
    }
    AudioSource GetAudioSource(string name)
    {
        return tracks.FirstOrDefault(t => t.name == name);
    }
    public void PlaySFX(string name)
    {
        var sfx = GetAudioSource(name);
        if (sfx == null) return;
        
        sfx.Play();
        sfx.loop = false;
    }

    public void Pause(int trackIndex)
    {
        tracks[trackIndex].Pause();
    }
    public void Stop(int trackIndex)
    {
        StartCoroutine(TurnDownVolume(trackIndex));
    }

    public void Stop(string name)
    {
        Stop(GetIndex(name));
    }

    public void StopCurrent()
    {
        if (currentSongID == -1) return;
        Stop(currentSongID);
    }

    public void ModifyPitch(int trackIndex, float pitch)
    {
        tracks[trackIndex].pitch = pitch;
    }

    public AudioSource GetSource(int trackIndex)
    {
        return tracks[trackIndex];
    }


    public void Volume(float volume, SoundType soundType = SoundType.TRACK)
    {
        switch (soundType)
        {
            case SoundType.TRACK:
                mixerTrack.SetFloat("volume", Mathf.Log10(volume) * 20);
                break;
            case SoundType.SFX:
                mixerSfx.SetFloat("volume", Mathf.Log10(volume) * 20);
                break;
            default:
                Debug.LogWarning("Mixer not found...");
                break;
        }

    }

    IEnumerator TurnDownVolume(int soundID)
    {
        if (soundID == -1) yield break;
        while (tracks[soundID].volume > 0.1f)
        {
            tracks[soundID].volume -= interpolationSpeed;
            yield return null;
        }
        tracks[soundID].Stop();
    }
    IEnumerator TurnUpVolume(int soundID)
    {
        if (soundID == -1) yield break;
        while (tracks[soundID].volume < 1)
        {
            tracks[soundID].volume += interpolationSpeed;
            yield return null;
        }
        tracks[soundID].volume = 1;
    }
}

public enum SoundType
{
    TRACK,
    SFX
}
