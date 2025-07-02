using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Subject
{
    public void LoadHome()
    {
        ResumeGame();
        StartCoroutine(LoadSceneAsync("Main"));
    }

    public void LoadNextLevel()
    {
        ResumeGame();
        StartCoroutine(LoadSceneIndexAsync(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadPreviousLevel()
    {
        ResumeGame();
        StartCoroutine(LoadSceneIndexAsync(SceneManager.GetActiveScene().buildIndex - 1));
    }

    public void LoadLevel(string nameScene)
    {
        ResumeGame();
        StartCoroutine(LoadSceneAsync(nameScene));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        NotifyObserver(GameEvent.MainBGM);
        ResumeGame();
        StartCoroutine(LoadSceneIndexAsync(SceneManager.GetActiveScene().buildIndex));
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void JustClick()
    {
        NotifyObserver(GameEvent.Click);
    }


    IEnumerator LoadSceneAsync(string name)
    {
        NotifyObserver(GameEvent.MainBGM);
        AsyncOperation op = SceneManager.LoadSceneAsync(name);
        while (!op.isDone)
        {
            Debug.Log(op.progress);
            yield return null;
        }
    }

    IEnumerator LoadSceneIndexAsync(int index)
    {
        NotifyObserver(GameEvent.MainBGM);
        AsyncOperation op = SceneManager.LoadSceneAsync(index);
        while (!op.isDone)
        {
            Debug.Log(op.progress);
            yield return null;
        }
    }
}
