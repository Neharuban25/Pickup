using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public float pickupDistance = 2f;
    public int ammoAmount = 30;

    public Transform player;

    public AudioClip pickupSound;

    private bool pickedUp = false;

    void Update()
    {
        if (pickedUp)
            return;

        if (player == null)
            return;

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        if (distance <= pickupDistance && Input.GetKeyDown(KeyCode.E))
        {
            GunShoot gun = player.GetComponentInChildren<GunShoot>();

            if (gun != null)
            {
                gun.AddAmmo(ammoAmount);

                // Pickup sound
                if (pickupSound != null)
                {
                    AudioSource.PlayClipAtPoint(
                        pickupSound,
                        transform.position
                    );
                }

                pickedUp = true;

                Destroy(gameObject);
            }
        }
    }
}