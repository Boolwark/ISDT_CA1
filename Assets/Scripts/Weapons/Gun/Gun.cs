using TMPro;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20f;
    public Canvas ammoDisplayCanvas;
    public TMP_Text ammoDisplayText;
    protected XRGrabInteractable grabbable;
    public VisualEffect muzzleEffect;

    protected virtual void Start()
    {
        grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
        grabbable.selectEntered.AddListener(ShowAmmoDisplay);
        grabbable.selectExited.AddListener(HideAmmoDisplay);
        ammoDisplayCanvas.gameObject.SetActive(false);
    }

    public virtual void FireBullet(ActivateEventArgs args)
    {
        GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, Quaternion.identity);
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
        muzzleEffect.Play();
        AudioManager.Instance.PlaySFX("GunShot");
        Destroy(spawnedBullet, 5);
    }

    protected void ShowAmmoDisplay(SelectEnterEventArgs args)
    {
        ammoDisplayCanvas.gameObject.SetActive(true);
    }

    protected void HideAmmoDisplay(SelectExitEventArgs args)
    {
        ammoDisplayCanvas.gameObject.SetActive(false);
    }

    protected virtual void UpdateAmmoDisplay()
    {
        // Override this method in derived classes to update ammo display text.
    }
}