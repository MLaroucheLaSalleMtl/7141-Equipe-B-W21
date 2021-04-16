using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseScore : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    void Start()
    {
        playerData.score = 0;
        playerData.lives = 5;
    }

}
