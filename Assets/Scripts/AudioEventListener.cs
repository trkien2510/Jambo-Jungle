using UnityEngine;

public class AudioEventListener : MonoBehaviour, IObserver<SoundEvent>
{
    public void OnNotify(SoundEvent action)
    {
        switch (action)
        {
            case SoundEvent.music:
                AudioManager.Instance.PlayBGM(AudioManager.Instance.music);
                break;
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
            case SoundEvent.bossFire:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.bossFire);
                break;
            case SoundEvent.laser:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.laser);
                break;
            case SoundEvent.bossTheme:
                AudioManager.Instance.PlayBGM(AudioManager.Instance.bossTheme);
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        foreach (var subject in FindObjectsOfType<Subject<SoundEvent>>())
        {
            subject.AddObserver(this);
        }
    }

    private void OnDisable()
    {
        foreach (var subject in FindObjectsOfType<Subject<SoundEvent>>())
        {
            subject.RemoveObserver(this);
        }
    }
}
