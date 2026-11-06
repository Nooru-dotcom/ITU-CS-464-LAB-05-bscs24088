using UnityEngine;

/// <summary>
/// Base collectible coin that spins and awards score when touched by the player.
/// Lecture 06 — Physics Forces & Collectibles (slide 80)
/// </summary>
public class Coin : MonoBehaviour
{
    [SerializeField] protected int value = 1;
    [SerializeField] float spinSpeed = 90f;

    void Update() => transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Coin touched by {other.name}");
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(value);
            Destroy(gameObject);
        }
    }
}
