using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Singleton that tracks score, triggers win, and manages bonus coin state.
/// Lecture 06 — Physics Forces & Collectibles (slide 81)
/// Extended with Bonus Tasks 1, 2 & 3.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // ── Core score settings ───────────────────────────────────────────────────
    [Header("Score")]
    [SerializeField] int targetScore = 10;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject winPanel;

    // ── Spawner reference (for red coin threshold check) ─────────────────────
    [Header("Spawner")]
    [SerializeField] Spawner spawner;

    // ── Bonus Task 2 — Blue Coin Burst ────────────────────────────────────────
    [Header("Bonus Task 2 — Blue Coin Burst")]
    [SerializeField] GameObject blueCoinPrefab;
    [SerializeField] int blueCoinCount = 50;
    [SerializeField] float burstRadius = 6f;
    [SerializeField] Transform blueCoinParent; // optional parent to keep hierarchy tidy

    // ── Bonus Task 3 — Bonus Coin HUD ─────────────────────────────────────────
    [Header("Bonus Task 3 — Bonus Coin HUD")]
    [SerializeField] TMP_Text bonusCoinText;
    [SerializeField] GameObject bonusCoinPanel;

    // ── Internal state ────────────────────────────────────────────────────────
    int score;
    int bonusCoinsCollected;

    // ═════════════════════════════════════════════════════════════════════════
    void Awake() => Instance = this;

    void Start()
    {
        UpdateScoreUI();

        // Hide bonus panel until red coin is collected
        if (bonusCoinPanel != null)
            bonusCoinPanel.SetActive(false);
    }

    // ── Core Score ────────────────────────────────────────────────────────────

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();

        // Tell the spawner to check if the red coin should now appear
        if (spawner != null)
            spawner.CheckRedCoinSpawn(score);

        if (score >= targetScore) Win();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score} / {targetScore}";
    }

    void Win()
    {
        if (winPanel != null) winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ── Bonus Task 2 — Blue Coin Burst ────────────────────────────────────────

    /// <summary>
    /// Spawns 50 blue coins in a circle of radius <see cref="burstRadius"/> around <paramref name="playerPos"/>.
    /// Also activates the Bonus Coin HUD (Task 3).
    /// </summary>
    public void TriggerBlueCoinBurst(Vector3 playerPos)
    {
        if (blueCoinPrefab == null)
        {
            Debug.LogWarning("[GameManager] blueCoinPrefab is not assigned!");
            return;
        }

        bonusCoinsCollected = 0;

        Transform parent = blueCoinParent != null ? blueCoinParent : transform;

        for (int i = 0; i < blueCoinCount; i++)
        {
            // Distribute evenly around the full 360° ring
            float angle = (360f / blueCoinCount) * i;
            float rad   = angle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(rad), 0f, Mathf.Sin(rad)) * burstRadius;
            Vector3 spawnPos = playerPos + offset + Vector3.up * 0.5f;

            Instantiate(blueCoinPrefab, spawnPos, Quaternion.identity, parent);
        }

        Debug.Log($"[GameManager] {blueCoinCount} blue coins spawned around player!");

        // Activate bonus HUD (Task 3)
        UpdateBonusCoinUI();
        if (bonusCoinPanel != null)
            bonusCoinPanel.SetActive(true);
    }

    // ── Bonus Task 3 — Bonus Coin HUD ─────────────────────────────────────────

    /// <summary>
    /// Called by each BlueCoin when collected.
    /// </summary>
    public void AddBonusCoin()
    {
        bonusCoinsCollected++;
        UpdateBonusCoinUI();

        if (bonusCoinsCollected >= blueCoinCount)
            Debug.Log("[GameManager] All bonus coins collected!");
    }

    void UpdateBonusCoinUI()
    {
        if (bonusCoinText != null)
            bonusCoinText.text = $"BonusCoins: {bonusCoinsCollected}/{blueCoinCount}";
    }
}
