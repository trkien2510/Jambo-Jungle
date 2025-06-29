using UnityEngine;

public class AudioEventListener : MonoBehaviour, IObserver
{
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
            case SoundEvent.explosion:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.explosion);
                break;
            case SoundEvent.click:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        foreach (var subject in FindObjectsOfType<Subject>())
        {
            subject.AddObserver(this);
        }
    }

    private void OnDisable()
    {
        foreach (var subject in FindObjectsOfType<Subject>())
        {
            subject.RemoveObserver(this);
        }
    }
}
