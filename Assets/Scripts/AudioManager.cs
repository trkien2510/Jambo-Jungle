using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource SFX;

    [Header("Audio Clips")]
    public AudioClip music;
    public AudioClip step;
    public AudioClip hurt;
    public AudioClip jump;
    public AudioClip shoot;
    public AudioClip enemyShoot;
    public AudioClip explosion;
    public AudioClip click;

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
        PlayBGM();
    }

    public void PlayBGM()
    {
        if (BGM != null && BGM != null && isBgmOn)
        {
            BGM.clip = music;
            BGM.loop = true;
            BGM.Play();
        }
    }

    public void SetBgm(bool isOn)
    {
        isBgmOn = isOn;
        if (isOn)
            PlayBGM();
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
