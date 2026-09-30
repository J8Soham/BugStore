using UnityEngine;
using System.Collections;

public class LadyBug : Bug
{
    #region Editor Variables
    [SerializeField] 
    [Tooltip("How long it can hover.")]
    private float m_hoverDuration;

    [SerializeField] 
    [Tooltip("The speed in which it hovers.")]
    private float m_hoverSpeed;

    [SerializeField] 
    [Tooltip("The +- x direction for hover.")]
    private float m_hoverWidth;
    
    #endregion

    #region Catch Mechanics
    public override void OnReleased(Hand releasingHand)
    {
        base.OnReleased(releasingHand);
        StartCoroutine(HoverRoutine());
    }

    private IEnumerator HoverRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        float originalGravity = m_rb.gravityScale;
        
        m_rb.gravityScale = 0.1f;
        float timer = 0f;
        Vector2 startPos = transform.position;

        m_anmr.SetBool("isFlying", true); //Transition to "flying" animation

        while (timer < m_hoverDuration && !IsBeingHeld)
        {
            timer += Time.deltaTime;
            float offsetX = Mathf.Sin(timer * m_hoverSpeed) * m_hoverWidth;
            m_rb.linearVelocity = new Vector2(offsetX, m_rb.linearVelocity.y * 0.5f);
            yield return null;
        }
        m_rb.gravityScale = originalGravity;

        m_anmr.SetBool("isFlying", false); //Transition to "idle" animation
    }
    #endregion
}
