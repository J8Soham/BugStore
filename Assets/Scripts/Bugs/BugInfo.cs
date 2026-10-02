using UnityEngine;

[System.Serializable]
public class BugInfo
{
    #region Editor Variables
    [SerializeField] 
    [Tooltip("Name of the bug.")]
    public string m_bugName;
    [SerializeField] 
    [Tooltip("Max tosses for the particular bug.")]
    public int m_maxTosses;
    [SerializeField] 
    [Tooltip("Points awarded for graduated bug.")]
    public float m_pointValue;
    [SerializeField] 
    [Tooltip("Launch force of the bug.")]
    public float m_launchForce;
    [SerializeField] 
    [Tooltip("If catching possible.")]
    public bool m_isHazardous;
    [SerializeField] 
    [Tooltip("If catching possible.")]
    public float m_stunDuration;
    [SerializeField] 
    [Tooltip("Damage done when dropped.")]
    public int m_damagesOnDrop;
    [SerializeField] 
    [Tooltip("Rarity of the bug (on scale of 1-10)")]
    public int m_rarity;
    
    public string BugName {
        get {
            return m_bugName;  
        } 
    }
    public int MaxTosses {
        get {
            return m_maxTosses;  
        } 
    }
    public float PointValue {
        get {
            return m_pointValue;  
        } 
    }
    public float LaunchForce {
        get {
            return m_launchForce;  
        } 
    }
    public bool IsHazardous {
        get {
            return m_isHazardous;  
        } 
    }
    public float StunDuration {
        get {
            return m_stunDuration;  
        } 
    }
    public int DamageOnDrop {
        get {
            return m_damagesOnDrop;  
        } 
    }
    public int Rarity {
        get {
            return m_rarity;  
        } 
    }
    #endregion
}
