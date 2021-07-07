using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource bgmTrackA;
    private AudioSource bgmTrackB;
    private AudioSource sfxSource;

    private bool isTrackA;

    [SerializeField] private AudioMixerGroup bgmMixerGroup;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;
    [SerializeField] private GameObject spatialAudioPrefab;

    private void Awake()
    {
        //Create audio sources
        bgmTrackA = this.gameObject.AddComponent<AudioSource>();
        bgmTrackB = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();

        bgmTrackA.outputAudioMixerGroup = bgmMixerGroup;
        bgmTrackB.outputAudioMixerGroup = bgmMixerGroup;
        sfxSource.outputAudioMixerGroup = sfxMixerGroup;

        //loop BGM
        bgmTrackA.loop = true;
        bgmTrackB.loop = true;
    }

    public void StopBGM()
    {
        if (bgmTrackA.isPlaying) bgmTrackA.Stop();
        if (bgmTrackB.isPlaying) bgmTrackB.Stop();
    }

    public void PlayBGM(AudioClip bgmClip)
    {
        //Determine active track
        AudioSource activeTrack = (isTrackA) ? bgmTrackA : bgmTrackB;

        activeTrack.clip = bgmClip;
        activeTrack.volume = 1;
        activeTrack.Play();
    }

    public void PlayBGM(AudioClip bgmClip, float volume)
    {
        //Determine active track
        AudioSource activeTrack = (isTrackA) ? bgmTrackA : bgmTrackB;

        activeTrack.clip = bgmClip;
        activeTrack.volume = volume;
        activeTrack.Play();
    }

    public void PauseBGM()
    {
        if (bgmTrackA.isPlaying) bgmTrackA.Pause();
        if (bgmTrackB.isPlaying) bgmTrackB.Pause();
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        PlaySFX(sfxClip, 1f);
    }

    //Call this to play SFX
    public void PlaySFX(AudioClip sfxClip, float volume = 1f)
    {
        sfxSource.PlayOneShot(sfxClip);
    }

    public void Play3DSFX(AudioClip sfxClip, Vector3 position, float volume = 1f, float spread = 10f)
    {
        var go = Instantiate(spatialAudioPrefab, position, Quaternion.identity);
        var src = go.GetComponent<AudioSource>();
        src.outputAudioMixerGroup = sfxMixerGroup;
        src.volume = volume;
        src.spread = spread;
        src.PlayOneShot(sfxClip);

        StartCoroutine(DestroyAudio(sfxClip.length, go));
    }

    IEnumerator DestroyAudio(float duration, GameObject objToDestroy)
    {
        yield return new WaitForSecondsRealtime(duration);
        Destroy(objToDestroy);
    }
}
