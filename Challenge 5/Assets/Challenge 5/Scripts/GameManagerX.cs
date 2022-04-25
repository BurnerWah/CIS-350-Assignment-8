/*
 * Jaden Pleasants
 * Assignment 8
 * Manages game staate
 */
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerX : Singleton<GameManagerX> {
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timeText;
    public GameObject titleScreen;
    public Button restartButton;
    public List<GameObject> targetPrefabs;

    private int score;
    public int Score {
        get => score;
        set {
            score = value;
            scoreText.text = $"Score: {score}";
        }
    }
    private float spawnRate = 1.5f;
    private bool isGameActive;
    public static bool IsGameActive => Instance.isGameActive;

    private readonly float spaceBetweenSquares = 2.5f;
    private readonly float minValueX = -3.75f;
    private readonly float minValueY = -3.75f;

    public void StartGame(DifficultyLevel difficulty = DifficultyLevel.Easy) {
        spawnRate /= (int)difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        StartCoroutine(TimeHandlerThing());
        Score = 0;
        titleScreen.SetActive(false);
    }

    IEnumerator SpawnTarget() {
        while (isGameActive) {
            yield return new WaitForSeconds(spawnRate);
            var index = Random.Range(0, targetPrefabs.Count);

            if (isGameActive) {
                Instantiate(targetPrefabs[index],
                            RandomSpawnPosition,
                            targetPrefabs[index].transform.rotation);
            }
        }
    }

    IEnumerator TimeHandlerThing() {
        int timeLeft = 60;
        timeText.text = $"Time: {timeLeft}";
        while (isGameActive) {
            yield return new WaitForSeconds(1);
            if (isGameActive) {
                timeText.text = $"Time: {--timeLeft}";
                if (timeLeft == 0) {
                    GameOver();
                }
            }
        }
    }

    Vector3 RandomSpawnPosition => new Vector3(minValueX + (SquareIndex * spaceBetweenSquares),
                                               minValueY + (SquareIndex * spaceBetweenSquares),
                                               0);

    int SquareIndex => Random.Range(0, 4);

    public void UpdateScore(int scoreToAdd) => Score += scoreToAdd;

    public void GameOver() {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
