using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public UnityEvent OnStart;
    public UnityEvent OnAllEnemiesDead;

    public int enemiesLeft { get; private set; } = 0;
    public int enemiesKilled { get; private set; } = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        enemiesLeft = enemies.Length;

        OnStart?.Invoke();
    }

    private void Update()
    {
        if (enemiesLeft <= 0) OnAllEnemiesDead?.Invoke();
    }

    public void AddKilled()
    {
        enemiesLeft--;
        enemiesKilled++;
    }
}
