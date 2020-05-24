using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    private void OnEnable()
    {
        Tower.OnTowerDestroyed += CheckTower;
    }

    private void CheckTower(Tower tower)
    {
        // foreach (var summon in tower.Summons)
        // {
        //     summon.Die();
        // }  

        Tower.ActiveTowers.Remove(tower);
        if (Tower.ActiveTowers.Count <= 0)
        {
            Victory();
        }
    }

    private void Victory()
    {
        SceneManager.LoadScene(1);
    }
}