using UnityEngine;

/// <summary>
/// Bonus Task 2/3 — Blue Coin.
/// Spawned in a ring around the player when the Red Coin is collected.
/// Collecting all 50 completes the bonus.
/// </summary>
public class BlueCoin : MonoBehaviour
{
    [SerializeField] float spinSpeed = 180f;

    void Update() => transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Blue Coin collected!");
        GameManager.Instance.AddBonusCoin();
        Destroy(gameObject);
    }
}
