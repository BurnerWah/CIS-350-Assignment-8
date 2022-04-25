/*
 * Jaden Pleasants
 * Assignment 8
 * Thing to handle difficulty buttons
 */
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButtonX : MonoBehaviour {
    private Button button;
    public int difficulty;
    DifficultyLevel Difficulty => (DifficultyLevel)difficulty;

    // Start is called before the first frame update
    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    // When a button is clicked, call the StartGame() method
    // and pass it the difficulty value (1, 2, 3) from the button 
    void SetDifficulty() {
        Debug.Log($"{button.gameObject.name} was clicked");
        GameManagerX.Instance.StartGame(Difficulty);
    }
}
