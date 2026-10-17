using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;

    void Awake() { instance = this; }

    [SerializeField] Slider masterVolumeSlider;

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("masterVolume", masterVolumeSlider.value);
        PlayerPrefs.Save();
        MusicManager.UpdateVolume();
    }

    void OnEnable()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume", 0.8f);
    }
}
