using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;
 
    #region Private Variables
    [SerializeField]
    [Tooltip("Template SFX Object")]
    private AudioSource sfxObject;
    #endregion

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    public void PlaySoundClip(AudioClip audioClip, Vector3 position, float volume)
    {
        
    }
}
