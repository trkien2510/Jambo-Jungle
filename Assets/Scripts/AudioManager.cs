using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource SFX;

    [Header("Audio Clips")]
    public AudioClip mainBGM;
    public AudioClip bossTheme;
    public AudioClip levelLose;
    public AudioClip playerRun;
    public AudioClip playerHurt;
    public AudioClip playerJump;
    public AudioClip playerShoot;
    public AudioClip enemyShoot;
    public AudioClip explosion;
    public AudioClip click;
    public AudioClip bossLaser;
    public AudioClip bossFireball;
    public AudioClip bossDefeated;

    [Header("Audio Mixer")]
    [SerializeField] AudioMixer audioMixer;

    private bool isBgmOn = true;
    private bool isSfxOn = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayBGM(mainBGM);
    }

    public void PlayBGM(AudioClip music)
    {
        if (BGM != null && isBgmOn)
        {
            BGM.clip = music;
            BGM.loop = true;
            BGM.Play();
        }
        else
        {
            BGM.clip = this.mainBGM;
            BGM.loop = true;
            BGM.Play();
        }
    }

    public void SetBgm(bool isOn)
    {
        isBgmOn = isOn;
        if (isOn)
            PlayBGM(mainBGM);
        else
            BGM.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (isSfxOn && clip != null)
            SFX.PlayOneShot(clip);
    }

    public void SetSfx(bool isOn)
    {
        isSfxOn = isOn;
    }
}
