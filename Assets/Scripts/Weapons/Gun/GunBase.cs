using System;
using CodeMonkey.Utils;
using Effects;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.XR.Interaction.Toolkit;
using Weapons;
using Weapons.Magazines.Weapons.Magazines;
using Random = UnityEngine.Random;

public class GunBase : MonoBehaviour
{
    private RecoilEffect recoilEffect;

    public GunData gunData;
    public Transform spawnPoint;
    public Canvas ammoDisplayCanvas;

    
    public TMP_Text ammoDisplayText;
    protected XRGrabInteractable grabbable;
  
    public ParticleSystem muzzleEffect;
    private float timeOfLastShot = 0f;
    private float rapidShootTime = 0f;
    private GameObject currentBulletPf;

  


    private Vector3 GetSpread(float shootTime = 0)
    {
        Vector3 spread = Vector3.zero;
      
            spread = new Vector3(Random.Range(-gunData.Spread.x, gunData.Spread.x),Random.Range(-gunData.Spread.y, gunData.Spread.y),Random.Range(-gunData.Spread.z, gunData.Spread.z));
            Mathf.Clamp01(shootTime / gunData.maxSpreadTime);
        
       

        return spread;
    }

    public void SetBulletPf(GameObject bulletPf)
    {
        currentBulletPf = bulletPf;
    }
    protected virtual void Start()
    {
        grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
        grabbable.selectEntered.AddListener(ShowAmmoDisplay);
        grabbable.selectExited.AddListener(HideAmmoDisplay);
        ammoDisplayCanvas.gameObject.SetActive(false);
        recoilEffect = FindObjectOfType<RecoilEffect>();
        currentBulletPf = gunData.bullet;
    }

    public virtual void FireBullet(ActivateEventArgs args)
    {
        float delta = Time.time - timeOfLastShot;
        if (delta >= gunData.recoilRecoverySpeed)
        {
            rapidShootTime += delta;
        }
        else
        {
          rapidShootTime = 0f;
        }

        if (recoilEffect != null)
        {
            recoilEffect.Activate(gunData.recoilStrength,gunData.nVibrations,gunData.shakeDuration);
        }
        GameObject spawnedBullet = Instantiate(currentBulletPf, spawnPoint.position, Quaternion.identity);
        if (spawnedBullet.TryGetComponent(out Rigidbody rb))
        {
            rb.velocity = (spawnPoint.forward * gunData.fireSpeed) + GetSpread(rapidShootTime);
        }

        if (spawnedBullet.TryGetComponent(out Bullet bullet))
        {
            
            bullet.damage = gunData.damage; 
        }
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
    
    }
}