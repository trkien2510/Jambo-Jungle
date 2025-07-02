using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class UIEventListener : MonoBehaviour, IObserver
{
    [Header("Boss")]
    GameObject boss;
    [SerializeField] GameObject bossHealthBar;
    [SerializeField] PlayableDirector bossIntro;
    [SerializeField] PlayableDirector bossOutro;

    [Header("Game")]
    [SerializeField] GameObject openSetting;
    [SerializeField] GameObject completePanel;
    [SerializeField] GameObject losePanel;

    [Header("Player health")]
    List<Image> heartImages = new List<Image>();
    GameObject player;
    [SerializeField] GameObject lifeUnits;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private void Start()
    {
        foreach (Image unit in lifeUnits.GetComponentsInChildren<Image>())
        {
            heartImages.Add(unit);
        }

        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss");

        if (bossHealthBar != null)
        {
            bossHealthBar.SetActive(false);
        }

        UpdatePlayerHealthUI();
    }

    private void Update()
    {
        UpdatePlayerHealthUI();
    }

    public void OnNotify(GameEvent action)
    {
        switch (action)
        {
            case GameEvent.levelLose:
                Time.timeScale = 0f;
                openSetting.SetActive(false);
                losePanel.SetActive(true);
                break;
            case GameEvent.LevelComplete:
                Time.timeScale = 0f;
                openSetting.SetActive(false);
                completePanel.SetActive(true);
                break;
            case GameEvent.BossIntro:
                if (bossIntro != null)
                    bossIntro.Play();
                StartCoroutine(WaitActiveBoss());
                break;
            case GameEvent.BossDamaged:
                UpdateBossHealthUI();
                break;
            case GameEvent.BossDefeated:
                if (bossOutro != null)
                    bossOutro.Play();
                StartCoroutine(BossDefeatedRoutin());
                break;
            case GameEvent.PlayerDamaged:
                UpdatePlayerHealthUI();
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

    IEnumerator BossDefeatedRoutin()
    {
        bossHealthBar.SetActive(false);
        openSetting.SetActive(false);
        yield return new WaitForSeconds(3f);
        completePanel.SetActive(true);
    }

    private void UpdatePlayerHealthUI()
    {
        if (player == null) return;

        float currentHealth = player.GetComponent<PlayerHealth>().CurrentHealth;
        float fullUnits = currentHealth / 10f;

        for (int i = 0; i < heartImages.Count; i++)
        {
            heartImages[i].sprite = i < fullUnits ? fullHeart : emptyHeart;
        }
    }

    private void UpdateBossHealthUI()
    {
        if (boss == null) return;

        float currentHealth = boss.GetComponent<BossHealth>().Health;
        float maxHealth = boss.GetComponent<BossHealth>().MaxHealth;
        if (bossHealthBar != null)
        {
            bossHealthBar.GetComponent<Slider>().maxValue = maxHealth;
            bossHealthBar.GetComponent<Slider>().value = currentHealth;
        }
    }

    IEnumerator WaitActiveBoss()
    {
        yield return new WaitForSeconds(4f);
        bossHealthBar.SetActive(true);
        UpdateBossHealthUI();
    }
}
