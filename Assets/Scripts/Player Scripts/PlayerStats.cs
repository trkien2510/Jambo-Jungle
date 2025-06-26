using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Subject
{
    private void Start()
    {
        NotifyObserver(PlayerAction.hurt);
    }
}
