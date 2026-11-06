using UnityEngine;

/// <summary>
/// Spawns normal coins at random positions on a timer.
/// Also handles the one-time Red Coin spawn (Bonus Task 1) at a random score threshold between 1–10.
/// Lecture 06 — Physics Forces & Collectibles (slide 79)
/// </summary>
public class Spawner : MonoBehaviour
{
    [Header("Normal Coin Settings")]
    [SerializeField] GameObject coinPrefab;
    [SerializeField] Transform player;
    [SerializeField] float interval = 1.5f;
    [SerializeField] int maxCoins = 5;
    [SerializeField] float area = 7f;

    [Header("Bonus Task 1 — Red Coin")]
    [SerializeField] GameObject redCoinPrefab;

    // The red coin will appear once the player's score reaches this threshold (chosen randomly 1–10)
    int redCoinScoreThreshold;
    bool redCoinSpawned = false;

    void Start()
    {
        // Pick a random score (1–10) at which the red coin will spawn
        redCoinScoreThreshold = Random.Range(1, 11);
        Debug.Log($"[Spawner] Red coin will appear at score: {redCoinScoreThreshold}");

        InvokeRepeating(nameof(SpawnCoin), 1f, interval);
    }

    void SpawnCoin()
    {
        if (transform.childCount >= maxCoins) return;

        float x = Random.Range(-area, area);
        float z = Random.Range(-area, area);
        Vector3 pos = new Vector3(x, 0.5f, z);

        if (Vector3.Distance(pos, player.position) < 2f) return;

        Instantiate(coinPrefab, pos, Quaternion.identity, transform);
    }

    /// <summary>
    /// Called by GameManager whenever the score changes.
    /// Spawns the red coin once when the threshold score is reached.
    /// </summary>
    public void CheckRedCoinSpawn(int currentScore)
    {
        if (redCoinSpawned || redCoinPrefab == null) return;
        if (currentScore < redCoinScoreThreshold) return;

        redCoinSpawned = true;

        float x = Random.Range(-area, area);
        float z = Random.Range(-area, area);
        Vector3 pos = new Vector3(x, 0.5f, z);

        // Ensure it doesn't spawn on top of the player
        if (Vector3.Distance(pos, player.position) < 2f)
            pos += new Vector3(3f, 0f, 3f);

        Instantiate(redCoinPrefab, pos, Quaternion.identity, transform);
        Debug.Log($"[Spawner] Red coin spawned at score {currentScore}!");
    }
}
