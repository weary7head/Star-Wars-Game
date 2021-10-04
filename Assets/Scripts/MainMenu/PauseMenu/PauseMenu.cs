using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _menuUI;
    [SerializeField] private Slider _horizontalSensitivitySlider;
    [SerializeField] private Slider _verticalSensitivitySlider;

    private bool _isPaused = false;
    private float _nextTimeToPress = 0f;
    private SceneProvider _sceneProvider;

    public event Action<float> OnHorizontalSensitivityChanged; 
    public event Action<float> OnVerticalSensitivityChanged;

    private void Awake()
    {
        _sceneProvider = new SceneProvider();
        _horizontalSensitivitySlider.value = _player.GetHorizontalSensitivity();
        _verticalSensitivitySlider.value = _player.GetVerticalSensitivity();
    }

    private void OnEnable()
    {
        _player.OnPausePressed += PausePressed;
    }

    private void OnDisable()
    {
        _player.OnPausePressed -= PausePressed;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }

    private void PausePressed()
    {
        if (Time.unscaledTime>= _nextTimeToPress)
        {
            _nextTimeToPress = Time.unscaledTime + 1f;
            _isPaused = !_isPaused;
            if (_isPaused)
            {
                ActivateMenu();
            }
            else
            {
                DeactivateMenu();
            }
        }
        
    }

    private void ActivateMenu()
    {
        _menuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DeactivateMenu()
    {
        _menuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Quit()
    {
        StartCoroutine(_sceneProvider.LoadSceneAsync("Menu"));
    }

    public void SetHorizontalSensitivity(float value)
    {
        OnHorizontalSensitivityChanged?.Invoke(value);
    }

    public void SetVerticalSensitivity(float value)
    {
        OnVerticalSensitivityChanged?.Invoke(value);
    }
}
