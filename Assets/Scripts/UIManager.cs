using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Subject<SoundEvent>
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
        NotifyObserver(SoundEvent.click);
        Application.Quit();
    }

    public void RestartGame()
    {
        NotifyObserver(SoundEvent.music);
        NotifyObserver(SoundEvent.click);
        ResumeGame();
        StartCoroutine(LoadSceneIndexAsync(SceneManager.GetActiveScene().buildIndex));
    }

    public void PauseGame()
    {
        NotifyObserver(SoundEvent.click);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        NotifyObserver(SoundEvent.click);
        Time.timeScale = 1f;
    }

    public void JustClick()
    {
        NotifyObserver(SoundEvent.click);
    }


    IEnumerator LoadSceneAsync(string name)
    {
        NotifyObserver(SoundEvent.music);
        AsyncOperation op = SceneManager.LoadSceneAsync(name);
        while (!op.isDone)
        {
            Debug.Log(op.progress);
            yield return null;
        }
    }

    IEnumerator LoadSceneIndexAsync(int index)
    {
        NotifyObserver(SoundEvent.music);
        AsyncOperation op = SceneManager.LoadSceneAsync(index);
        while (!op.isDone)
        {
            Debug.Log(op.progress);
            yield return null;
        }
    }
}
