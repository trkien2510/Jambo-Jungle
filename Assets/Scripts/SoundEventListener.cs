using UnityEngine;

public class SoundEventListener : MonoBehaviour, IObserver
{
    public void OnNotify(GameEvent action)
    {
        switch (action)
        {
            case GameEvent.MainBGM:
                AudioManager.Instance.PlayBGM(AudioManager.Instance.mainBGM);
                break;
            case GameEvent.PlayerHurt:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.playerHurt);
                break;
            case GameEvent.PlayerJump:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.playerJump);
                break;
            case GameEvent.PlayerRun:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.playerRun);
                break;
            case GameEvent.PlayerDead:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.playerHurt);
                break;
            case GameEvent.PlayerShoot:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.playerShoot);
                break;
            case GameEvent.Explosion:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.explosion);
                break;
            case GameEvent.Click:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
                break;
            case GameEvent.BossFireball:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.bossFireball);
                break;
            case GameEvent.BossLaser:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.bossLaser);
                break;
            case GameEvent.BossTheme:
                AudioManager.Instance.PlayBGM(AudioManager.Instance.bossTheme);
                break;
            case GameEvent.BossDefeated:
                AudioManager.Instance.PlaySFX(AudioManager.Instance.bossDefeated);
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
