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
    [Tooltip("Number of waves.")]
    private WaveInfo m_waves;
    #endregion

    #region Private Variables 
    private HUDController cc_hud;
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
        cc_hud = FindAnyObjectByType<HUDController>();
        StartCoroutine(WaveLoopRoutine()); 
    }
    #endregion

    #region Spawning Logic
    private IEnumerator WaveLoopRoutine() 
    {
        for (int i = 0; i < m_waves.Count; i++)
        {
            p_currentWaveIndex = i;
            Debug.Log(p_currentWaveIndex + " : " + i + " : " + m_waves.Count);
            cc_hud.UpdateWave((p_currentWaveIndex + 1));
            BugGroup[] bugGroup = m_waves.BugGroup;
            int count = 0;
            while (count < m_waves.MaxBugs){
                foreach (BugGroup bug in bugGroup)
                {
                    if (bug.FirstSpawnWave <= i) {
                        count += 1;
                        SpawnBug(bug.BugPrefab);
                        yield return new WaitForSeconds(m_waves.IntervalBetweenSpawn);
                    }
                }
            }
            yield return new WaitForSeconds((m_waves.IntervalBetweenSpawn * 2));
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