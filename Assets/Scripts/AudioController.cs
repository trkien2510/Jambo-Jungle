using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] Toggle bgmToggle;
    [SerializeField] Toggle sfxToggle;

    void Start()
    {
        bgmToggle.onValueChanged.AddListener(OnBgmToggleChanged);
        sfxToggle.onValueChanged.AddListener(OnSfxToggleChanged);

        bgmToggle.isOn = true;
        sfxToggle.isOn = true;
    }

    void OnBgmToggleChanged(bool isOn)
    {
        AudioManager.Instance.SetBgm(isOn);
    }

    void OnSfxToggleChanged(bool isOn)
    {
        AudioManager.Instance.SetSfx(isOn);
    }
}
