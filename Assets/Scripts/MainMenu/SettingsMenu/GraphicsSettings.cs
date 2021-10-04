using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    private Resolution[] _resolutions;
    [SerializeField] private Dropdown _resolutionDropdown;
    [SerializeField] private Dropdown _qualityDropdown;
    [SerializeField] private Toggle _fulscreenToggle;

    private void Awake()
    {
        _resolutions = Screen.resolutions;
        _resolutionDropdown.ClearOptions();
        List<string> temporaryResolutions = new List<string>();
        int currentResolution = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            temporaryResolutions.Add(_resolutions[i].ToString());
            if (_resolutions[i].Equals(Screen.currentResolution))
            {
                currentResolution = i;
            }
        }
        _resolutionDropdown.AddOptions(temporaryResolutions);
        _resolutionDropdown.value = currentResolution;
        _qualityDropdown.value = QualitySettings.GetQualityLevel();
        _fulscreenToggle.isOn = Screen.fullScreen;
    }

    private void Start()
    {
        _resolutionDropdown.RefreshShownValue();
        _qualityDropdown.RefreshShownValue();
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int index)
    {
        Resolution resolution = _resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
