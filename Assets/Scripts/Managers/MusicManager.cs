using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class MusicManager : MonoBehaviour
{
    public enum MusicType
    {
        AnalogTime,
        DinoCountdown,
        LaboratoryTheme,
        MenuCat
    }


    [SerializeField] private MusicList[] musicList;
    public static MusicManager instance;

    public AudioSource audioSource;


    void Awake() { instance = this; }

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(MusicType sound, float volume = 1)
    {
        AudioClip[] clips = instance.musicList[(int)sound].sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

        instance.audioSource.clip = randomClip;
        instance.audioSource.volume = volume * PlayerPrefs.GetFloat("masterVolume", 0.8f) * PlayerPrefs.GetFloat("musicVolume", 1);
        instance.audioSource.Play();
    }

    public static void UpdateVolume(float volume = 1)
    {
        instance.audioSource.volume = volume * PlayerPrefs.GetFloat("masterVolume", 0.8f) * PlayerPrefs.GetFloat("musicVolume", 1);
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(MusicType));
        Array.Resize(ref musicList, names.Length);

        for (int i = 0; i < musicList.Length; i++)
        {
            musicList[i].name = names[i];
        }
    }

#endif

}

[Serializable]
public struct MusicList
{
    [HideInInspector] public string name;
    [SerializeField] public AudioClip[] sounds;
}