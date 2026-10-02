using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    #region Intialization
    private void Awake() { 
        Cursor.lockState = CursorLockMode.None; 
    } 
    #endregion

    #region Play Button Methods
    public void PlayArena() { 
        SceneManager.LoadScene("Arena");
    } 
    #endregion
}
