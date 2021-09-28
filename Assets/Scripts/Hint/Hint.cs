using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;

public class Hint : MonoBehaviour
{
    [SerializeField] private string _hint;
    [SerializeField] private HintGenerator _hintGenerator;
    private Action _onDestroy;

    private void OnEnable()
    {
        _onDestroy += Destroy;
    }

    private void OnDisable()
    {
        _onDestroy -= Destroy;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            StartCoroutine(_hintGenerator.ShowHint(_hint, _onDestroy));
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
