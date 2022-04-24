/*
 * Jaden Pleasants
 * Assignment 8
 * Script for setting the game's difficulty
 */
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour {
    private Button button;
    public int difficulty;

    void SetDifficulty() {
        Debug.Log($"{gameObject.name} was clicked");
        GameManager.Instance.StartGame(difficulty);
    }

    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    void Update() {

    }
}
