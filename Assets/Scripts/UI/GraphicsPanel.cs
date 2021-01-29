using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsPanel : MonoBehaviour
{
    public Slider slider;
    private int _qualityLevels;
    public TMP_Text settingsText;
    public GameSettings gameSettings;
    
    private void Start()
    {
        slider.maxValue = QualitySettings.names.Length-1;
        slider.value = Mathf.Min(gameSettings.GetQuality(), slider.maxValue);
        gameSettings.SetQuality(slider);
        
        UpdateSettingsText();
    }
    
    public void UpdateSettingsText()
    {
        settingsText.text = QualitySettings.names[gameSettings.GetQuality()];
    }
}
