/*
 * Jaden Pleasants
 * Assignment 8
 * Destroys an object a couple seconds after its created
 */
using UnityEngine;

public class DestroyObjectX : MonoBehaviour {
    void Start() {
        Destroy(gameObject, 2);
    }
}
