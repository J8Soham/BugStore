using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The text component for the current score")]
    private TMP_Text m_scoreText; 

    [SerializeField]
    [Tooltip("The text component for the current score")]
    private TMP_Text m_waveText; 

    [SerializeField]
    [Tooltip("The text component for the larva count")]
    private TMP_Text m_larvaText; 

    [SerializeField]
    [Tooltip("The text component for the lady bug count")]
    private TMP_Text m_ladyBugText; 

    [SerializeField]
    [Tooltip("The text component for the cricket count")]
    private TMP_Text m_cricketText; 
    
    [SerializeField]
    [Tooltip("The heart panel")]
    private Image[] m_heartImages;
    #endregion

    #region Private Variables
    private string p_defaultScoreText;
    private string p_defaultWaveText;
    private string p_defaultLarvaText;
    private string p_defaultLadyBugText;
    private string p_defaultCricketText;
    #endregion
    
    #region Intialization
    private void Awake() { 
        p_defaultScoreText = m_scoreText.text;
        p_defaultWaveText = m_waveText.text;
        p_defaultLarvaText = m_larvaText.text;
        p_defaultLadyBugText = m_ladyBugText.text;
        p_defaultCricketText = m_cricketText.text;

        m_scoreText.text = p_defaultScoreText.Replace("%D", "0");  
        m_waveText.text = p_defaultWaveText.Replace("%D", "0"); 
        m_larvaText.text = p_defaultLarvaText.Replace("%D", "0"); 
        m_ladyBugText.text = p_defaultLadyBugText.Replace("%D", "0"); 
        m_cricketText.text = p_defaultCricketText.Replace("%D", "0"); 
    }
    #endregion

    #region Update Health Functions
    public void DisplayLife(int livesToDisplay)
    {
        if (m_heartImages == null) {
            return;
        }

        for (int i = 0; i < m_heartImages.Length; i++)
        {
            m_heartImages[i].enabled = (i < livesToDisplay);
        }
    }
    #endregion

    #region Wave Methods
    public void UpdateWave(int waveToDisplay)
    {
        m_waveText.text = p_defaultWaveText.Replace("%D", waveToDisplay.ToString());  
    }
    public void UpdateBugs(int larvaCount, int ladyBugCount, int cricketCount) {
        m_larvaText.text = p_defaultLarvaText.Replace("%D", larvaCount.ToString());  
        m_ladyBugText.text = p_defaultLadyBugText.Replace("%D", ladyBugCount.ToString());  
        m_cricketText.text = p_defaultCricketText.Replace("%D", cricketCount.ToString());  
    }
    #endregion

    #region Score Methods
    public void UpdateScore() {
        m_scoreText.text = p_defaultScoreText.Replace("%D", ScoreManager.singleton.CurrentScore.ToString());  
    }
    #endregion
}
