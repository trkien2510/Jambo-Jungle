using UnityEngine;

public class AudioEventListener : MonoBehaviour, IObserver
{
    [SerializeField] Subject _playerStateSubject;
    [SerializeField] Subject _turretSubject;

    public void OnNotify(SoundEvent action)
    {
        switch (action)
        {
            case SoundEvent.hurt:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.hurt);
                break;
            case SoundEvent.jump:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
                break;
            case SoundEvent.run:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.step);
                break;
            case SoundEvent.dead:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.hurt);
                break;
            case SoundEvent.shoot:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.shoot);
                break;
            case SoundEvent.turetBroken:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.turretBroke);
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        _playerStateSubject.AddObserver(this);
        _turretSubject.AddObserver(this);
    }

    private void OnDisable()
    {
        _playerStateSubject.RemoveObserver(this);
        _turretSubject.RemoveObserver(this);
    }
}
