using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField] private Enemy enemyPrefab;
    [NonReorderable] public List<Wave> waves = new List<Wave>();
    public List<Enemy> waveEnemies = new List<Enemy>();
    public int currentWave = 0;
    private bool flipping = false;

    public event Action AllWavesDestroyed;

    public void SpawnWave() {
        waveEnemies.Clear();

        GameplayManager.instance.UIManager.SetWaveText(currentWave+1);

        Vector2 enemyDimensions = enemyPrefab.GetBounds();
        Vector2 enemyStartPosition = GetStartPosition(waves[currentWave].enemiesInRow);
        Vector2 enemyPosition = enemyStartPosition;

        for (int i = 0; i < waves[currentWave].rows; i++) {
            for (int j = 0; j < waves[currentWave].enemiesInRow; j++) {
                GameObject enemyGO = ObjectPoolManager.instance.SpawnFromPool("Enemy", enemyPosition, Quaternion.identity);
                enemyGO.name = "Wave " + i + " - Enemy " + j;
                Enemy enemy = enemyGO.GetComponent<Enemy>();
                enemy.ResetCharacterHealth();
                enemy.SetMoveSpeed(waves[currentWave].enemySpeed[0]);
                enemy.SetEnemySprite();
                waveEnemies.Add(enemy);
                enemy.EnemyDeath += RemoveEnemyFromCurrentWave;
                enemy.OutOfBounds += FlipEnemies;
                enemyPosition.x += enemyDimensions.x;
            }
            enemyPosition.x = enemyStartPosition.x;
            enemyPosition.y -= enemyDimensions.y;
        }
    }

    private void FlipEnemies() {
        if (flipping) { return; }
        flipping = true;
        for (int i = 0; i < waveEnemies.Count; i++) {
            waveEnemies[i].FlipMovement();
        }
        MoveEnemiesDown();
    }

    private void MoveEnemiesDown() {
        Vector2 enemyDimensions = enemyPrefab.GetBounds();
        for (int i = 0; i < waveEnemies.Count; i++) {
            waveEnemies[i].transform.position = new Vector2(waveEnemies[i].transform.position.x, waveEnemies[i].transform.position.y - enemyDimensions.y);
        }
        StartCoroutine(DoneFlippingEnemies());
    }

    IEnumerator DoneFlippingEnemies() {
        yield return new WaitForSeconds(.1f);
        flipping = false;
    }

    private void RemoveEnemyFromCurrentWave(Enemy enemy) {
        enemy.EnemyDeath -= RemoveEnemyFromCurrentWave;
        enemy.OutOfBounds -= FlipEnemies;
        waveEnemies.Remove(enemy);
        IncreaseRemainingEnemiesSpeed();
        CheckWaveOver();
    }
    private void IncreaseRemainingEnemiesSpeed() {
        if(waveEnemies.Count == 0) { return; }
        int totalWaveEnemies = waves[currentWave].enemiesInRow * waves[currentWave].rows;
        int currentWaveEnemies = waveEnemies.Count;
        float percentageComplete = 1f-((float)currentWaveEnemies / (float)totalWaveEnemies);

        float newSpeed = waves[currentWave].enemySpeed[0];

        if (percentageComplete > .9f) {
            newSpeed = waves[currentWave].enemySpeed[4];
        }
        else if (percentageComplete > .75f) {
            newSpeed = waves[currentWave].enemySpeed[3];
        }
        else if(percentageComplete > .5f) {
            newSpeed = waves[currentWave].enemySpeed[2];
        }
        else if(percentageComplete > .25f) {
            newSpeed = waves[currentWave].enemySpeed[1];
        }

        if(newSpeed != waveEnemies[0].moveSpeed){
            for (int i = 0; i < waveEnemies.Count; i++) {
                waveEnemies[i].SetMoveSpeed(newSpeed);
            }
        }
    }

    private void CheckWaveOver() {
        if(waveEnemies.Count <= 0) {
            Debug.Log("Wave Complete");
            currentWave++;
            if (currentWave == waves.Count) {
                AllWavesDestroyed?.Invoke();
            }
            else {
                StartCoroutine(StartNextWave());
            }
        }
        else {
            //speed up invaders
        }
    }

    IEnumerator StartNextWave() {
        GameplayManager.instance.Player.StopShooting();
        yield return new WaitForSeconds(2f);
        SpawnWave();
        yield return new WaitForSeconds(0.5f);
        GameplayManager.instance.Player.StartShooting();
    }

    private Vector2 GetStartPosition(int enemiesInWave) {
        Vector2 enemyDimensions = enemyPrefab.GetBounds();
        Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector2(0f, Screen.height));

        //X
        pos.x += enemyDimensions.x / 2f;
        float screenWidthWorldSpace = 2*(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height / 2f)).x);
        float waveWidth = enemyDimensions.x * enemiesInWave;
        float bufferSpace = (screenWidthWorldSpace - waveWidth)/2f;
        pos.x += bufferSpace;

        //Y
        pos.y -= enemyDimensions.y / 2f;
        pos.y -= enemyDimensions.y * 2;

        return pos;
    }

    public void ResetWaves() {
        currentWave = 0;
        for (int i = 0; i < waveEnemies.Count; i++) {
            waveEnemies[i].gameObject.SetActive(false);
        }
        waveEnemies.Clear();
    }

}


