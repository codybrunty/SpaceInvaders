using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager> {
    [field: SerializeField] public Player Player { get; private set; }
    [field: SerializeField] public EnemyManager EnemyManager { get; private set; }
    [field: SerializeField] public UIManager UIManager { get; private set; }

    private bool gameInProgress = false;

    public void StartGame() {
        gameInProgress = true;
        UIManager.DisableStartButton();
        UIManager.EnableWaveUI();
        UIManager.DisableGameOverUI();
        EnemyManager.SpawnWave();
        Player.StartPlayer();
    }

    private void OnEnable() {
        EnemyManager.AllWavesDestroyed += GameWin;
        Player.PlayerDeath += GameLoss;
    }
    private void OnDisable() {
        EnemyManager.AllWavesDestroyed -= GameWin;
        Player.PlayerDeath -= GameLoss;
    }

    private void GameWin() {
        if (!gameInProgress) { return; }
        gameInProgress = false;
        Debug.Log("GameOver You Win");
        Player.ResetPlayer();
        EnemyManager.ResetWaves();
        UIManager.EnableStartButton();
        UIManager.DisableWaveUI();
        UIManager.EnableGameOverUI();
        UIManager.SetGameOverText("Game Over<br><br>You Win");
    }

    private void GameLoss() {
        if (!gameInProgress) { return; }
        gameInProgress = false;
        Debug.Log("GameOver You Lose");
        Player.ResetPlayer();
        EnemyManager.ResetWaves();
        UIManager.EnableStartButton();
        UIManager.DisableWaveUI();
        UIManager.EnableGameOverUI();
        UIManager.SetGameOverText("Game Over<br><br>You Lose");
    }

}
