/*
 * Jaden Pleasants
 * Assignment 8
 * Handles target-related logic
 */
using System.Collections;
using UnityEngine;

public class TargetX : MonoBehaviour {
    private Rigidbody rb;
    public int pointValue;
    public GameObject explosionFx;

    public float timeOnScreen = 1.0f;

    private readonly float minValueX = -3.75f;
    private readonly float minValueY = -3.75f;
    private readonly float spaceBetweenSquares = 2.5f;


    void Start() {
        rb = GetComponent<Rigidbody>();
        transform.position = RandomSpawnPosition;
        // begin timer before target leaves screen
        StartCoroutine(RemoveObjectRoutine());
    }

    // When target is clicked, destroy it, update score, and generate explosion
    private void OnMouseDown() {
        if (GameManagerX.IsGameActive) {
            Destroy(gameObject);
            GameManagerX.Instance.UpdateScore(pointValue);
            Explode();
        }
    }

    // Generate a random spawn position based on a random index from 0 to 3
    Vector3 RandomSpawnPosition => new Vector3(minValueX + (SquareIndex * spaceBetweenSquares),
                                               minValueY + (SquareIndex * spaceBetweenSquares),
                                               0);

    int SquareIndex => Random.Range(0, 4);


    // If target that is NOT the bad object collides with sensor, trigger game over
    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);

        if (other.gameObject.CompareTag("Sensor") && !gameObject.CompareTag("Bad")) {
            GameManagerX.Instance.GameOver();
        }
    }

    // Display explosion particle at object's position
    void Explode() => Instantiate(explosionFx, transform.position, explosionFx.transform.rotation);

    // After a delay, Moves the object behind background so it collides with the Sensor object
    IEnumerator RemoveObjectRoutine() {
        yield return new WaitForSeconds(timeOnScreen);
        if (GameManagerX.IsGameActive) {
            transform.Translate(Vector3.forward * 5, Space.World);
        }
    }
}
