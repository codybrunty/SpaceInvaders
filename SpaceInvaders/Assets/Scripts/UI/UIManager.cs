using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour{

    [SerializeField] private GameObject startButton; 
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI gameOverText;

    public void EnableStartButton() {
        startButton.SetActive(true);
    }
    public void DisableStartButton() {
        startButton.SetActive(false);
    }

    public void EnableGameOverUI() {
        gameOverText.gameObject.SetActive(true);
    }
    public void DisableGameOverUI() {
        gameOverText.gameObject.SetActive(false);
    }
    public void EnableWaveUI() {
        waveText.gameObject.SetActive(true);
    }
    public void DisableWaveUI() {
        waveText.gameObject.SetActive(false);
    }

    public void SetGameOverText(string txt) {
        gameOverText.text = txt;
    }
    public void SetWaveText(int wave) {
        waveText.text = "Wave: "+wave;
    }
}
