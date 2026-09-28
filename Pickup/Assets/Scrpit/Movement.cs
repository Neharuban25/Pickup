using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    public Animator animator;
    public CharacterController controller;

    public AudioSource walkAudioSource;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        if (movement.magnitude > 0.1f)
        {
            controller.Move(
                movement.normalized * moveSpeed * Time.deltaTime
            );

            Quaternion targetRotation =
                Quaternion.LookRotation(movement);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            animator.SetBool("isWalking", true);

            // Walk sound
            if (walkAudioSource != null && !walkAudioSource.isPlaying)
            {
                walkAudioSource.Play();
            }
        }
        else
        {
            animator.SetBool("isWalking", false);

            // Stop walk sound
            if (walkAudioSource != null && walkAudioSource.isPlaying)
            {
                walkAudioSource.Stop();
            }
        }
    }
}