using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class SpawnZombie : MonoBehaviour
{
    public static SpawnZombie instance;
    public GameObject enemyPrefab; // Prefab of the enemy to be spawned
    public Transform[] spawnPoints; // Array of spawn points
    public Text spawnTimeText; // Reference to UI Text for displaying spawn time
    public float defaultSpawnInterval = 2f; // Default time between spawns in seconds
    private int enemyCount = 0; // Counter to track how many enemies have been spawned
    public int maxEnemies = 5; // Default maximum number of enemies to spawn

    private float spawnInterval;
    private NeuralNetwork neuralNetwork;
    private float lastSpawnTime; // Time of the last spawn

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Initialize the neural network with an example architecture
        int[] layers = new int[] { 3, 5, 3 }; // Adjust based on desired network depth
        neuralNetwork = new NeuralNetwork(layers);

        // Start initial spawning
        EnemyGeneration();
    }

    public void EnemyGeneration()
    {
        lastSpawnTime = Time.time; // Initialize last spawn time
        // Start the repeated spawning of enemies
        InvokeRepeating("SpawnEnemy", 0f, defaultSpawnInterval);
    }

    void SpawnEnemy()
    {
        if (enemyCount >= maxEnemies)
        {
            CancelInvoke("SpawnEnemy");
            return;
        }

        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }

        // Calculate time since last spawn in milliseconds
        float timeSinceLastSpawn = (Time.time - lastSpawnTime) * 1000f;
        if (spawnTimeText != null)
        {
            spawnTimeText.text = $"Time Spawn: {timeSinceLastSpawn:F0} ms";
        }
        lastSpawnTime = Time.time;

        // Neural network input: [current enemy count, current maxEnemies, defaultSpawnInterval]
        float[] inputs = new float[] { enemyCount, maxEnemies, defaultSpawnInterval };

        // Get the neural network output
        float[] nnOutput = neuralNetwork.FeedForward(inputs);

        // Interpret the neural network output
        int spawnPointIndex = Mathf.Clamp(Mathf.RoundToInt(nnOutput[0] * spawnPoints.Length), 0, spawnPoints.Length - 1);
        spawnInterval = Mathf.Clamp(nnOutput[1], 1f, 10f); // Ensure interval stays within reasonable bounds
        maxEnemies = Mathf.Clamp(Mathf.RoundToInt(nnOutput[2] * 10), 1, 20); // Adjust max enemies dynamically

        // Choose a spawn point based on neural network output
        Transform spawnPoint = spawnPoints[spawnPointIndex];

        // Spawn the enemy at the chosen spawn point
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // Increment the counter
        enemyCount++;
    }

    public void OnEnemyKilled()
    {
        // Decrease the enemy count and spawn a new enemy if necessary
        enemyCount--;

        if (enemyCount < maxEnemies)
        {
            SpawnEnemy();
        }
    }
}

