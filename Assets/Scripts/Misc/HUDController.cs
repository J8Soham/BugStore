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
    [Tooltip("The heart panel")]
    private Image[] m_heartImages;
    #endregion

    #region Private Variables
    private string p_defaultText;
    #endregion
    
    #region Intialization
    private void Awake() { 
        p_defaultText = m_scoreText.text;
        m_scoreText.text = p_defaultText.Replace("%D", "0");  
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

    #region Score Methods
    public void UpdateScore() {
        m_scoreText.text = p_defaultText.Replace("%D", ScoreManager.singleton.CurrentScore.ToString());  
    }
    #endregion
}
