using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventListener : MonoBehaviour, IObserver
{
    [SerializeField] Subject _playerSubject;
    public void OnNotify(PlayerAction action)
    {
        if (action == PlayerAction.hurt)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.hurt);
        }
    }

    private void OnEnable()
    {
        _playerSubject.AddObserver(this);
    }

    private void OnDisable()
    {
        _playerSubject.RemoveObserver(this);
    }
}
