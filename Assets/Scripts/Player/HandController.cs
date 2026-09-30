using UnityEngine;
using UnityEngine.SceneManagement;

public class HandController : MonoBehaviour
{
    #region Cached References
    private Camera m_mainCamera;
    #endregion

    #region Editor Variables
    [SerializeField] 
    private Hand m_leftHand;
    [SerializeField] 
    private Hand m_rightHand;

    [SerializeField]
    [Tooltip("Distance from the cursor to each hand horizontally.")]
    private float m_handOffset;

    [SerializeField] 
    private float m_screenMinX;
    [SerializeField] 
    private float m_screenMaxX;
    [SerializeField] 
    private float m_minY;
    [SerializeField] 
    private float m_maxY;

    [Tooltip("Maximum X position for the Left Hand")]
    [SerializeField] 
    private float m_leftHandMaxX;

    [Tooltip("Minimum X position for the Right Hand")]
    [SerializeField] 
    private float m_rightHandMinX;
    #endregion

    #region Initialization
    private void Awake()
    {
        m_mainCamera = Camera.main;
    }
    #endregion

    #region Unity Frame Loop
    private void Update()
    {
        UpdateHandPositions();
        HandleHandInput();
    }
    #endregion

    #region Movement &amp; Position Clamping
    private void UpdateHandPositions()
    {
        Vector3 mouseScreenPos = Input.mousePosition; 
        Vector3 mouseWorldPos = m_mainCamera.ScreenToWorldPoint(mouseScreenPos); 
        mouseWorldPos.z = 0f;

        float clampedY = Mathf.Clamp(mouseWorldPos.y, m_minY, m_maxY);
        float rawLeftX = mouseWorldPos.x - m_handOffset;
        float clampedLeftX = Mathf.Clamp(rawLeftX, m_screenMinX, m_leftHandMaxX);
        if (m_leftHand != null)
        {
            m_leftHand.transform.position = new Vector3(clampedLeftX, clampedY, 0f);
        }

        float rawRightX = mouseWorldPos.x + m_handOffset;
        float clampedRightX = Mathf.Clamp(rawRightX, m_rightHandMinX, m_screenMaxX);
        if (m_rightHand != null)
        {
            m_rightHand.transform.position = new Vector3(clampedRightX, clampedY, 0f);
        }
    }
    #endregion

    #region Input Forwarding
    private void HandleHandInput()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            m_leftHand?.TryGrab();
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            m_leftHand?.Release();
        }

        if (Input.GetMouseButtonDown(1)) 
        {
            m_rightHand?.TryGrab();
        }
        else if (Input.GetMouseButtonUp(1)) 
        {
            m_rightHand?.Release();
        }
    }
    #endregion
}
