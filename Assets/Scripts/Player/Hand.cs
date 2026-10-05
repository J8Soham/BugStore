using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool IsGrabbing { get; private set; }
    public bool IsStunned { get; private set; }

    #region Cached Components
    private MeshRenderer m_renderer;
    private SpriteRenderer m_spriteRenderer;
    private Bug m_heldBug;
    #endregion

    #region Editor Variables
    /* Commented out in favor for Animator
    [SerializeField] 
    private Sprite m_openHandSprite;
    [SerializeField] 
    private Sprite m_closedHandSprite;
    */
    [SerializeField] 
    private Transform m_holdPoint;
    #endregion

    #region Private Variables
    #endregion

    #region Animation Variables
    private Animator m_anmr;

    [SerializeField]
    [Tooltip("Play this sound when hurt")]
    private AudioClip m_audioOuch;

    [SerializeField]
    [Tooltip("Play this sound when grabbing")]
    private AudioClip m_audioGrab;

    [SerializeField]
    [Tooltip("Play this sound when releasing")]
    private AudioClip m_audioRelease;

    #endregion

    #region Initialization
    private void Awake() {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_anmr = GetComponent<Animator>(); //For Animation
        SetHandState(false);
        UpdateSprite();
    }
    #endregion

    #region Grab Mechanics
    public void SetHandState(bool grabbing) {
        IsGrabbing = grabbing;
        /* Commented out because UpdateSprite() does the same thing
        if (m_spriteRenderer != null)
        {
            m_spriteRenderer.sprite = grabbing ? m_closedHandSprite : m_openHandSprite;
        }
        */
        UpdateSprite();
    }
    public void TryGrab()
    {
        if (IsStunned) return;

        IsGrabbing = true;
        SFXManager.Instance.PlaySoundClip(m_audioGrab, transform.position, 0.1f);
        UpdateSprite();
    }

    public void Release()
    {
        IsGrabbing = false;
        SFXManager.Instance.PlaySoundClip(m_audioRelease, transform.position, 0.1f);
        UpdateSprite();

        if (m_heldBug != null)
        {
            m_heldBug.OnReleased(this);
            m_heldBug = null;
        }
    }
    #endregion

    #region Bug Catching
    private void OnTriggerStay2D(Collider2D other)
    {
    
        if (!IsGrabbing || m_heldBug != null || IsStunned) return;

        if (other.CompareTag("Bug"))
        {
            Bug bug = other.GetComponent<Bug>();    
            if (bug != null && !bug.IsBeingHeld){
                CatchBug(bug);
            }
        }
    }

    private void CatchBug(Bug bug)
    {
        m_heldBug = bug;
        Transform snapPoint = (m_holdPoint != null) ? m_holdPoint : transform;
        m_heldBug.OnCaught(this, snapPoint);
    }
    #endregion

    #region Helpers
    private void UpdateSprite()
    {
        m_anmr.SetBool("isGrabbing",IsGrabbing);
        m_anmr.SetBool("isStunned",IsStunned);
    }
    #endregion

    #region Stun Mechanics
    public void StunHand(float duration)
    {
        if (!IsStunned)
        {
            StartCoroutine(StunRoutine(duration));
        }
    }

    private IEnumerator StunRoutine(float duration)
    {
        IsStunned = true;
        SFXManager.Instance.PlaySoundClip(m_audioOuch, transform.position, 0.1f);
        UpdateSprite();
        /* Commented out in favor for Animator
        if (m_renderer != null) {
            m_renderer.material.color = Color.red;
        }
        */

        yield return new WaitForSeconds(duration);
        IsStunned = false;
        UpdateSprite();
        /* Commented out in favor for Animator
        if (m_renderer != null) {
            m_renderer.material.color = Color.white;
        }
        */
    }
    #endregion

}
