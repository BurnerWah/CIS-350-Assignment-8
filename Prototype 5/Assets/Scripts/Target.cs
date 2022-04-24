/*
 * Jaden Pleasants
 * Assignment 8
 * Script for the targets that get spawned
 */
using UnityEngine;

public class Target : MonoBehaviour {
    private Rigidbody targetRb;
    private readonly float minSpeed = 12;
    private readonly float maxSpeed = 16;
    private readonly float maxTorque = 10;
    private readonly float xRange = 4;
    private readonly float ySpawnPos = -6;

    public int pointValue;
    public ParticleSystem explosionParticle;

    void Start() {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce, ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque, RandomTorque, RandomTorque, ForceMode.Impulse);
        transform.position = RandomSpawnPos;
    }

    Vector3 RandomForce => Vector3.up * Random.Range(minSpeed, maxSpeed);
    float RandomTorque => Random.Range(-maxTorque, maxTorque);
    Vector3 RandomSpawnPos => new Vector3(Random.Range(-xRange, xRange), ySpawnPos);

    void Update() {

    }

    private void OnMouseDown() {
        if (GameManager.Instance.isGameActive) {
            GameManager.Instance.UpdateScore(pointValue);
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad")) {
            GameManager.Instance.GameOver();
        }
    }
}
