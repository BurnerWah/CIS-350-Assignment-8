/*
 * Jaden Pleasants
 * Assignment 8
 * Manages most of the game state I guess
 * I have a headache rn so be forewarned my comments are gonna be hot garbage
 */
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    public bool isGameActive;

    private float spawnRate = 1f;
    private int score;
    // This property lets the score text updates act as a side-effect of updating the score.
    public int Score {
        get => score;
        set {
            score = value;
            scoreText.text = $"Score: {score}";
        }
    }

    public void UpdateScore(int scoreToAdd) => Score += scoreToAdd;

    public void GameOver() {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public void StartGame(int difficulty) {
        spawnRate /= difficulty;
        isGameActive = true;
        Score = 0;
        StartCoroutine(SpawnTarget());
        titleScreen.SetActive(false);
    }

    IEnumerator SpawnTarget() {
        while (isGameActive) {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
}
