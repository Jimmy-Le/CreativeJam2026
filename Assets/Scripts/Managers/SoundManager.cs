using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    public enum SoundType
    {
        Break,
        Drag,
        Error,
        GlassBreak,
        AnalogTime,
        DinoCountdown,
        LaboratoryTheme,
        MenuCat,
        Walk,
        Click,
        Clock,
        Meow,
        Reverse
    }


    [SerializeField] private SoundList[] soundList;
    public static SoundManager instance;

    public AudioSource audioSource;
    

    void Awake() { instance = this; }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        AudioClip[] clips = instance.soundList[(int)sound].sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0,clips.Length)];
       
        instance.audioSource.PlayOneShot(randomClip, volume);
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);

        for(int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
    }

#endif

}




[Serializable]
public struct SoundList
{
    [HideInInspector] public string name;
    [SerializeField] public AudioClip[] sounds;
}