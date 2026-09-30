using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager singleton;

    #region Private Variable
    private float m_curScore;
    public float CurrentScore
    {
        get {
            return m_curScore;
        }
    }
    #endregion

    #region Intializations
    private void Awake() {
        if (singleton == null) {
            singleton = this;
        } else if (singleton != this) {
            Destroy(gameObject);
        }
        m_curScore = 0;
    }
    #endregion

    #region Score Methods
    public void IncreaseScore(float amount)
    {
        m_curScore += amount;
    }
    #endregion
}