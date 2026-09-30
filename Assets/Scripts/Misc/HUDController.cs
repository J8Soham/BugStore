using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    #region Editor Variables
    // [SerializeField]
    // [Tooltip("The part of the health that decreases.")]
    // private RectTransform m_healthBar;

    [SerializeField]
    [Tooltip("The text component for the current score")]
    private TMP_Text m_scoreText; 
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

    #region Update Health Bar
    // public void UpdateHealth(float percent) { 
        // m_healthBar.sizeDelta = new Vector2(p_originalWidth * percent, m_healthBar.sizeDelta.y); 
    // }
    #endregion

    #region Score Methods
    public void UpdateScore() {
        m_scoreText.text = p_defaultText.Replace("%D", ScoreManager.singleton.CurrentScore.ToString());  
    }
    #endregion
}
