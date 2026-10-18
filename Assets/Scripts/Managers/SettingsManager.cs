using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;

    void Awake() { instance = this; }

    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider soundVolumeSlider;

   

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("masterVolume", masterVolumeSlider.value);
        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("soundVolume", soundVolumeSlider.value);
        PlayerPrefs.Save();
        MusicManager.UpdateVolume();
    }

    void OnEnable()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume", 0.8f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f);
        soundVolumeSlider.value = PlayerPrefs.GetFloat("soundVolume", 1f);

    }
}
