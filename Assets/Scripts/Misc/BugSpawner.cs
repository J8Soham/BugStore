using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
            cc_hud.UpdateWave((p_currentWaveIndex + 1));
            List<BugGroup> vaildBugGroup = new List<BugGroup>();
            Dictionary<string, int> bugCounts = new Dictionary<string, int>();
            int totalWeight = 0;
            foreach (BugGroup bug in m_waves.BugGroup){
                bugCounts.Add(bug.BugName, 0);
                if (bug.FirstSpawnWave <= i) {
                    vaildBugGroup.Add(bug);
                    totalWeight += bug.Rarity;
                }
            }
            List<GameObject> bugsToSpawn = new List<GameObject>();
            int increaseBugs = m_waves.WaveBugsIncrease * p_currentWaveIndex;
            for (int count = 0; count < (m_waves.StartBugs + increaseBugs); count++){
                int roll = Random.Range(0, totalWeight);
                int cummulate = 0;
                foreach (BugGroup bug in vaildBugGroup)
                {
                    cummulate += bug.Rarity;
                    if (roll < cummulate){
                        bugsToSpawn.Add(bug.BugPrefab);
                        bugCounts[bug.BugName]++;
                        break;
                    }
                }
            }
            cc_hud.UpdateBugs(bugCounts["Larva"], bugCounts["LadyBug"], bugCounts["Cricket"]);
            foreach (GameObject bugPrefab in bugsToSpawn){
                SpawnBug(bugPrefab);
                yield return new WaitForSeconds(m_waves.IntervalBetweenSpawn);
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