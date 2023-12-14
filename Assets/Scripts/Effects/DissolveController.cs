using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DissolveController : MonoBehaviour
{
    private bool isVisible = false;
    public float dissolveSpeed = 0.5f; // Speed of the dissolve effect
    public Material dissolveMaterial;
    private bool isDissolving = false;
    private float dissolveValue;
    public float minDissolve = -2f;
    public float maxDissolve = 1.2f;
    public UnityEvent OnFadeOutEnd;
        

    void Start()
    {
        dissolveValue = minDissolve;
        // Get the material of the GameObject this script is attached to
        //dissolveMaterial = GetComponent<Renderer>().material;

    
        //dissolveMaterial.SetFloat("_Dissolve", dissolveValue);
    }

    public void SetDissolveMaterial(Material material,bool startFromMax)
    {
        this.dissolveMaterial = material;
        dissolveValue = startFromMax ? maxDissolve : minDissolve;
        material.SetFloat("_Dissolve", dissolveValue);
    }

    // Public method to start the dissolve effect
    public void StartFadingIn()
    {
        if (!isDissolving && !isVisible) // Only start the coroutine if it isn't already running
        {
            AudioManager.Instance.PlaySFX("DissolveIn");
            StartCoroutine(FadeInEffect());
        }

        isVisible = true;
    }
    

    private IEnumerator FadeInEffect()
    {
        isDissolving = true;


        while (dissolveValue < maxDissolve)
        {
            // Increment the dissolve value based on the speed
            dissolveValue += Time.deltaTime * dissolveSpeed;

            // Clamp the value to ensure it doesn't exceed 1
            dissolveValue = Mathf.Clamp(dissolveValue,minDissolve, maxDissolve);

            // Update the material's dissolve float property
            dissolveMaterial.SetFloat("_Dissolve", dissolveValue);

            // Wait until the next frame
            yield return null;
        }

        isDissolving = false; // The effect is complete, allow it to be triggered again
    }

    // Optionally, you could have a method to reverse the dissolve effect
    public void ReverseDissolving()
    {
        if (!isDissolving) // Only start the coroutine if it isn't already running
        {
            StartCoroutine(ReverseDissolveEffect());
        }
    }

    private IEnumerator ReverseDissolveEffect()
    {
        isDissolving = true;

    
        while (dissolveValue > minDissolve)
        {
          
            dissolveValue -= Time.deltaTime * dissolveSpeed;

            dissolveValue = Mathf.Clamp(dissolveValue,minDissolve, maxDissolve);


            dissolveMaterial.SetFloat("_Dissolve", dissolveValue);

       
            yield return null;
        }

        isDissolving = false;
       gameObject.SetActive(false);
    }
}
