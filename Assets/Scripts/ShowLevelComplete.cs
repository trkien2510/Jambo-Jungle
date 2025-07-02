using UnityEngine;

public class ShowLevelComplete : Subject
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            NotifyObserver(GameEvent.LevelComplete);
        }
    }
}
