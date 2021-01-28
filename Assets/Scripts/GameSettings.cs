using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New GameSettings", menuName = "CG TOOLS/Game Settings", order = 0)]
public class GameSettings : ScriptableObject
{
    public static EventHandler<float> audioVolumeChanged;

    [Header("Player Movement")]
    public float moveSpeed = 2.0f;
    public float runSpeed = 3.0f;
    public float jumpHeight = 0.2f;
    public float gravityValue = -9.81f;

    [Header("Audio")] 
    [SerializeField] private float mainVol = .5f;
    
    [Header("Graphics")] 
    [SerializeField] private int quality = 0;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += Load;
    }

    private void Load(Scene arg0, LoadSceneMode arg1)
    {
        LoadQualitySettings();
        LoadAudioVolume();
    }

    public void SetMainVol(Slider slider)
    {
        mainVol = slider.value;
        audioVolumeChanged?.Invoke(this, mainVol);
    }
    
    public float GetMainVol()
    {
        return mainVol;
    }
    
    public void SetQuality(Slider slider)
    {
        quality = (int)slider.value;
        QualitySettings.SetQualityLevel(quality);
        // PlayerPrefs.SetInt(Helper.PREF_QUAL_SETTING, (int)slider.value);
    }
    
    public int GetQuality()
    {
        return quality;
    }

    private void SaveMainVol()
    {
        PlayerPrefs.SetFloat(Helper.PREF_VOL_AUDIO, mainVol);
    }
    
    private void LoadQualitySettings()
    {
        quality = PlayerPrefs.GetInt(Helper.PREF_QUAL_SETTING, QualitySettings.names.Length-1);
        QualitySettings.SetQualityLevel(quality);
    }
    
    private void LoadAudioVolume()
    {
        mainVol = PlayerPrefs.GetFloat(Helper.PREF_VOL_AUDIO, mainVol);
        audioVolumeChanged?.Invoke(this, mainVol);
    }
    
    public void Save()
    {
        SaveMainVol();
    }

}
        
