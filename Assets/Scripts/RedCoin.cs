using UnityEngine;

/// <summary>
/// Bonus Task 1 — Red Coin.
/// A special coin (red) that spawns once at a random score between 1 and 10.
/// Collecting it triggers the Blue Coin burst (Bonus Task 2).
/// </summary>
public class RedCoin : MonoBehaviour
{
    [SerializeField] int value = 3;
    [SerializeField] float spinSpeed = 120f;

    void Update() => transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Red Coin collected! Triggering Blue Coin burst.");
        GameManager.Instance.AddScore(value);
        GameManager.Instance.TriggerBlueCoinBurst(other.transform.position);
        Destroy(gameObject);
    }
}
