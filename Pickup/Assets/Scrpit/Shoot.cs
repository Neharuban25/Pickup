using UnityEngine;
using TMPro;

public class GunShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float bulletSpeed = 30f;
    public float fireRate = 0.2f;

    public int maxAmmo = 30;
    public int currentAmmo = 0;

    public TMP_Text ammoText;
    public Animator animator;

    // Audio Sources
    public AudioSource shootingAudioSource;
    public AudioSource noAmmoAudioSource;

    private float nextFireTime = 0f;
    private bool firstPickupAmmoGiven = false;

    void Start()
    {
        UpdateAmmoText();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) &&
            Time.time >= nextFireTime)
        {
            if (currentAmmo > 0)
            {
                Shoot();
            }
            else
            {
                PlayNoAmmoSound();
            }

            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }

        // Reduce ammo
        currentAmmo--;

        // Update text
        UpdateAmmoText();

        // Shooting sound
        if (shootingAudioSource != null)
        {
            shootingAudioSource.Play();
        }

        if (animator != null)
        {
            animator.SetBool("Shoot", true);
            Invoke(nameof(StopShootAnimation), 0.1f);
        }

        Debug.Log("Ammo: " + currentAmmo);
    }

    void PlayNoAmmoSound()
    {
        if (noAmmoAudioSource != null)
        {
            noAmmoAudioSource.Play();
        }

        Debug.Log("NO AMMO");
    }

    void StopShootAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("Shoot", false);
        }
    }

    public void GunPickedUp()
    {
        if (!firstPickupAmmoGiven)
        {
            currentAmmo = maxAmmo;
            firstPickupAmmoGiven = true;

            UpdateAmmoText();
        }
    }

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;

        if (currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }

        UpdateAmmoText();

        Debug.Log("Ammo Collected: " + currentAmmo);
    }

    void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo.ToString();
        }
    }
}