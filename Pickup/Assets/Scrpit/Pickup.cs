using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public Transform handPosition;
    public float pickupDistance = 2f;

    public AudioSource pickupAudioSource;

    private bool pickedUp = false;

    void Update()
    {
        if (pickedUp)
            return;

        if (handPosition == null)
            return;

        float distance = Vector3.Distance(
            transform.position,
            handPosition.position
        );

        if (distance <= pickupDistance && Input.GetKeyDown(KeyCode.E))
        {
            pickedUp = true;

            // Pickup sound
            if (pickupAudioSource != null)
            {
                pickupAudioSource.Play();
            }

            // Gun hand-ku attach aagum
            transform.SetParent(handPosition);

            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}