using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource BGM;
    [SerializeField] private AudioSource SFX;

    [Header("Audio Clips")]
    [SerializeField] public AudioClip music;
    [SerializeField] public AudioClip step;
    [SerializeField] public AudioClip hurt;
    [SerializeField] public AudioClip jump;
    [SerializeField] public AudioClip jumpHurt;
    [SerializeField] public AudioClip landing;
    [SerializeField] public AudioClip shoot;
    [SerializeField] public AudioClip click;

    [Header("Audio Mixer")]
    [SerializeField] AudioMixer audioMixer;

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

    public void PlayBGM(AudioClip clip)
    {
        if (BGM.clip == clip) return;
        BGM.clip = clip;
        BGM.loop = true;
        BGM.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }

    public void SetBGMVolume(float sliderValue)
    {
        if (audioMixer == null)
        {
            return;
        }
        float volume = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume(float sliderValue)
    {
        if (audioMixer == null)
        {
            return;
        }
        float volume = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("SFXVolume", volume);
    }
}
