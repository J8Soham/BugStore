using UnityEngine;

public class Ground : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bug"))
        {
            Bug bug = other.GetComponent<Bug>();
            if (bug != null)
            {   
                GameManager.singleton.LoseLife(bug.Info.m_damagesOnDrop);
                Destroy(other.gameObject); 
            }
        }
    }
}