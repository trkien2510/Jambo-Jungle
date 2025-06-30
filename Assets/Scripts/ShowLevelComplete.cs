using UnityEngine;

public class ShowLevelComplete : MonoBehaviour
{
    [SerializeField] GameObject setting;
    [SerializeField] GameObject levelComplete;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0f;
        setting.SetActive(false);
        levelComplete.SetActive(true);
    }
}
