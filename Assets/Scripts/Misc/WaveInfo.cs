using UnityEngine;

[System.Serializable]
public class WaveInfo
{
    #region Editor Variables
    [SerializeField] 
    [Tooltip("Number of waves.")]
    public int m_count;

    [SerializeField] 
    [Tooltip("The group of bugs.")]
    private BugGroup[] m_bugGroup;

    [SerializeField] 
    [Tooltip("The interval between bugs.")]
    private int m_intervalBetweenSpawn;

    [SerializeField] 
    [Tooltip("Max number of bugs.")]
    private int m_startBugs;

    [SerializeField] 
    [Tooltip("Bugs increased per wave.")]
    private int m_waveBugsIncrease;
    
    public int Count {
        get {
            return m_count;  
        } 
    }

    public BugGroup[] BugGroup {
        get {
            return m_bugGroup;  
        } 
    }

    public int IntervalBetweenSpawn {
        get {
            return m_intervalBetweenSpawn;  
        } 
    }

    public int StartBugs {
        get {
            return m_startBugs;  
        } 
    }

    public int WaveBugsIncrease {
        get {
            return m_waveBugsIncrease;  
        } 
    }
    #endregion
}

[System.Serializable]
public class BugGroup
{
    #region Editor Variables
    [SerializeField] 
    [Tooltip("Name of the bug.")]
    public string m_bugName;

    [SerializeField] 
    [Tooltip("Bug Prefab.")]
    private GameObject m_bugGO;

    [SerializeField] 
    [Tooltip("When to first spawn.")]
    private int m_firstSpawnWave;
    
    [SerializeField] 
    [Tooltip("Rarity of the bug (on scale of 1-10)")]
    public int m_rarity;

    public string BugName {
        get {
            return m_bugName;  
        } 
    }

    public GameObject BugPrefab {
        get {
            return m_bugGO;  
        } 
    }

    public int FirstSpawnWave {
        get {
            return m_firstSpawnWave;  
        } 
    }
    
    public int Rarity {
        get {
            return m_rarity;  
        } 
    }
    #endregion
}