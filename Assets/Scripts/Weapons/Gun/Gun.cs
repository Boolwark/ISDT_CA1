using Effects;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    private RecoilEffect recoilEffect;
    public float shakeDuration = 2f;
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20f;
    public float recoilRecoverySpeed = 0.8f;
    public float maxSpreadTime = 1f;
    [SerializeField] private Vector3 Spread = new Vector3(.1f, .1f, .1f); 
    private BulletSpreadType bulletSpreadType;
    public Canvas ammoDisplayCanvas;
    public float recoilStrength = 1f;
    public int nVibrations = 2;
    
    public TMP_Text ammoDisplayText;
    protected XRGrabInteractable grabbable;
    public VisualEffect muzzleEffect;
    private float timeOfLastShot = 0f;
    public float rapidShootTime = 0f;

    private enum BulletSpreadType
    {
        Simple,
        Texture
    }
    

    private Vector3 GetSpread(float shootTime = 0)
    {
        Vector3 spread = Vector3.zero;
        if (bulletSpreadType == BulletSpreadType.Simple)
        {
            spread = new Vector3(Random.Range(-Spread.x, Spread.x),Random.Range(-Spread.y, Spread.y),Random.Range(-Spread.z, Spread.z));
            Mathf.Clamp01(shootTime / maxSpreadTime);
        }
        else if (bulletSpreadType == BulletSpreadType.Texture)
        {
            
        }

        return spread;
    }
    protected virtual void Start()
    {
        grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
        grabbable.selectEntered.AddListener(ShowAmmoDisplay);
        grabbable.selectExited.AddListener(HideAmmoDisplay);
        ammoDisplayCanvas.gameObject.SetActive(false);
        recoilEffect = FindObjectOfType<RecoilEffect>();
    }

    public virtual void FireBullet(ActivateEventArgs args)
    {
        float delta = Time.time - timeOfLastShot;
        if (delta >= recoilRecoverySpeed)
        {
            rapidShootTime += delta;
        }
        else
        {
            rapidShootTime = 0f;
        }

        if (recoilEffect != null)
        {
            recoilEffect.Activate(recoilStrength,nVibrations,shakeDuration);
        }
        GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, Quaternion.identity);
        spawnedBullet.GetComponent<Rigidbody>().velocity = (spawnPoint.forward * fireSpeed) + GetSpread(rapidShootTime);
        muzzleEffect.Play();
        AudioManager.Instance.PlaySFX("GunShot");
        Destroy(spawnedBullet, 5);
        timeOfLastShot = Time.time;
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