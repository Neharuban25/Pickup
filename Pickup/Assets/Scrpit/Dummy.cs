using UnityEngine;

public class DummyHit : MonoBehaviour
{
    public Animator animator;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            animator.SetBool("Hit", true);
        }
    }
}