using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HintGenerator : MonoBehaviour
{
    [SerializeField] private float _showTime = 3f;
    private TextMeshProUGUI _hintText;

    private void Awake()
    {
        _hintText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        StartCoroutine(ShowHint("Your task is to destroy the three Jedi"));
    }

    private void EnableHint()
    {
        gameObject.SetActive(true);
    }

    private void DisableHint()
    {
        gameObject.SetActive(false);
    }

    private void SetHint(string hint)
    {
        _hintText.text = hint;
    }

    public IEnumerator ShowHint(string hint, Action callback)
    {
        SetHint(hint);
        EnableHint();
        yield return new WaitForSecondsRealtime(_showTime);
        DisableHint();
        callback?.Invoke();
    }

    public IEnumerator ShowHint(string hint)
    {
        SetHint(hint);
        EnableHint();
        yield return new WaitForSecondsRealtime(_showTime);
        DisableHint();
    }
}
