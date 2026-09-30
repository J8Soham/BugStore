using UnityEngine;
using System.Collections;

public class BugSpawner : MonoBehaviour
{
    public static BugSpawner instance;

    #region Editor Variables
    [SerializeField]
    [Tooltip("MinX")]
    private float m_minSpawnX;
    [SerializeField] 
    [Tooltip("MaxX")]
    private float m_maxSpawnX;
    [SerializeField]
    [Tooltip("SpawnY")]
    private float m_spawnY;
    [SerializeField] 
    [Tooltip("Number of waves (if 0 then infinite)")]
    private WaveInfo m_waves;
    #endregion

    #region Private Variables 
    private int p_currentWaveIndex;
    private bool p_isSpawningActive;
    #endregion

    #region Initialization
    private void Awake()
    {
        p_currentWaveIndex = 0;
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        p_isSpawningActive = true;
        StartCoroutine(WaveLoopRoutine()); 
    }
    #endregion

    #region Spawning Logic
    private IEnumerator WaveLoopRoutine() 
    {
        for (int i = 0; i < m_waves.Count; i++)
        {
            p_currentWaveIndex = i;
            // WaveInfo currentWave = m_waves[i];
            
            // for each type of bug if cna include add to a list
            // SpawnBug(group.BugPrefab);
            // yield return new WaitForSeconds(currentWave.SpawnInterval);
        
            yield return new WaitForSeconds(1.0f);
        }

        p_isSpawningActive = false;
    }

    private void SpawnBug(GameObject bugPrefab)
    {
        float randomX = UnityEngine.Random.Range(m_minSpawnX, m_maxSpawnX); 
        Vector3 spawnPosition = new Vector3(randomX, m_spawnY, 0f);
        Instantiate(bugPrefab, spawnPosition, Quaternion.identity); 
    }
    #endregion
}