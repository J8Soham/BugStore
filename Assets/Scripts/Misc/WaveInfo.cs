using UnityEngine;

[System.Serializable]
public class WaveInfo
{
    #region Editor Variables
    [SerializeField] 
    [Tooltip("Number of waves.")]
    public int m_count;
    
    public int Count {
        get {
            return m_count;  
        } 
    }
    #endregion
}