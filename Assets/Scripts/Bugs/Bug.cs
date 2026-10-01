using UnityEngine;

public class Bug : MonoBehaviour
{

    #region Cached Components
    protected Rigidbody2D m_rb;
    private HUDController cc_hud;
    private int m_currentTossCount = 0;
    #endregion

    #region Editor Variables
    [SerializeField] 
    [Tooltip("Bug info.")]
    protected BugInfo m_info;

    public BugInfo Info {
        get {
            return m_info;  
        } 
    }
    #endregion

    #region Private Variables
    private bool p_isBeingHeld;
    public bool IsBeingHeld {
        get {
            return p_isBeingHeld;  
        } 
    }
    #endregion

    #region Animation Variables
    protected Animator m_anmr;
    #endregion

    #region Initialization
    private void Awake() {
        p_isBeingHeld = false;
        m_rb = GetComponent<Rigidbody2D>();
        m_anmr = GetComponent<Animator>(); //For Animation
    }
    private void Start()
    {
        cc_hud = FindAnyObjectByType<HUDController>();
    }
    #endregion 

    #region Catch Mecahaics
    public virtual void OnCaught(Hand catchingHand, Transform holdPoint)
    {
        if (m_info.IsHazardous)
        {
            catchingHand.StunHand(2.0f);
            return;
        }
        p_isBeingHeld = true;
        m_rb.isKinematic = true;
        m_rb.linearVelocity = Vector2.zero; 
        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
    }

    public virtual void OnReleased(Hand releasingHand)
    {
        transform.SetParent(null);
        m_rb.isKinematic = false;
        p_isBeingHeld = false;
        m_rb.AddForce(Vector2.up * m_info.LaunchForce, ForceMode2D.Impulse);

        m_currentTossCount++;
        if (m_currentTossCount >= m_info.MaxTosses)
        {
            GraduateBug();
        }
    }
    #endregion 

    #region Score Mechanics
    private void GraduateBug()
    {
        ScoreManager.singleton.IncreaseScore(m_info.PointValue);
        cc_hud.UpdateScore();
        Destroy(gameObject);
    }
    #endregion 
}