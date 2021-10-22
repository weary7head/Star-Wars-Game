using Assets.Scripts.Player;
using System;
using UnityEngine;

public class Hint : MonoBehaviour
{
    [SerializeField] private string _hint;
    [SerializeField] private HintGenerator _hintGenerator;
    private Action _destroyed;

    private void OnEnable()
    {
        _destroyed += OnDestroy;
    }

    private void OnDisable()
    {
        _destroyed -= OnDestroy;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            StartCoroutine(_hintGenerator.ShowHint(_hint, _destroyed));
        }
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
