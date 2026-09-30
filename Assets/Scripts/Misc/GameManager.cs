using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager singleton;

    #region Inspector Variables
    [SerializeField] 
    [Tooltip("Number of lives.")]
    private int m_maxLives;
    #endregion

    #region Private Variables
    private int p_currentLives;
    private bool p_isGameOver;
    public int CurrentLives {
        get
        {
            return p_currentLives;
        }
    }
    public bool GameOver {
        get
        {
            return p_isGameOver;
        }
    }
    #endregion

    #region Initialization
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
            return;
        }
        p_currentLives = m_maxLives;
    }
    #endregion

    #region Game Over Management
    public void LoseLife(int livesToLose)
    {
        if (p_isGameOver) {
            return;
        }
        p_currentLives -= livesToLose;
        if (p_currentLives <= 0)
        {
            TriggerDeath();
        }
    }

    private void TriggerDeath()
    {
        p_isGameOver = true;
        // SceneManager.LoadScene("MainMenu");
    }
    #endregion
}