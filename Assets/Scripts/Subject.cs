using System.Collections.Generic;
using UnityEngine;

public abstract class Subject<T> : MonoBehaviour
{
    private List<IObserver<T>> observers = new List<IObserver<T>>();

    public void AddObserver(IObserver<T> observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }

    public void RemoveObserver(IObserver<T> observer)
    {
        observers.Remove(observer);
    }

    protected void NotifyObserver(T action)
    {
        var currentObservers = new List<IObserver<T>>(observers);

        foreach (var observer in currentObservers)
        {
            try
            {
                observer.OnNotify(action);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }
}
