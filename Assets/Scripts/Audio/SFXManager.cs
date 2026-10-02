using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;
 
    #region Cache
    private AudioSource audioSource;
    private float clipLength;
    #endregion

    #region Private Variables
    [SerializeField]
    [Tooltip("Template Prefab SFX Object")]
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

    //Play SFX by SFXManager.Instance.PlaySoundClip(audioClip, position, volume);

    public void PlaySoundClip(AudioClip audioClip, Vector3 position, float volume)
    {
        audioSource = Instantiate(sfxObject, position, transform.rotation);

        //Pass audio clip and volume to SFX object
        audioSource.clip = audioClip;
        clipLength = audioClip.length;
        audioSource.volume = volume;

        audioSource.Play();

        Destroy(audioSource.gameObject, clipLength);

    }
}
