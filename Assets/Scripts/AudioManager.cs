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
    public AudioClip turretBroke;
    public AudioClip click;

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
