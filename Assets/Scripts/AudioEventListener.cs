using UnityEngine;

public class AudioEventListener : MonoBehaviour, IObserver
{
    [SerializeField] Subject _playerStateSubject;

    public void OnNotify(PlayerAction action)
    {
        switch (action)
        {
            case PlayerAction.hurt:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.hurt);
                break;
            case PlayerAction.jump:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
                break;
            case PlayerAction.run:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.step);
                break;
            case PlayerAction.dead:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.hurt);
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        _playerStateSubject.AddObserver(this);
    }

    private void OnDisable()
    {
        _playerStateSubject.RemoveObserver(this);
    }
}
