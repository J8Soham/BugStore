using System.Collections;
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
    [SerializeField] 
    private Sprite m_openHandSprite;
    [SerializeField] 
    private Sprite m_closedHandSprite;
    [SerializeField] 
    private Transform m_holdPoint;
    #endregion

    #region Private Variables
    #endregion

    #region Initialization
    private void Awake() {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        SetHandState(false);
        UpdateSprite();
    }
    #endregion

    #region Grab Mechanics
    public void SetHandState(bool grabbing) {
        IsGrabbing = grabbing;
        if (m_spriteRenderer != null)
        {
            m_spriteRenderer.sprite = grabbing ? m_closedHandSprite : m_openHandSprite;
        }
    }
    public void TryGrab()
    {
        if (IsStunned) return;

        IsGrabbing = true;
        UpdateSprite();
    }

    public void Release()
    {
        IsGrabbing = false;
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
        if (m_spriteRenderer != null)
        {
            m_spriteRenderer.sprite = IsGrabbing ? m_closedHandSprite : m_openHandSprite;
        }
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
        if (m_renderer != null) {
            m_renderer.material.color = Color.red;
        }

        yield return new WaitForSeconds(duration);
        IsStunned = false;
        if (m_renderer != null) {
            m_renderer.material.color = Color.white;
        }
    }
    #endregion

}
