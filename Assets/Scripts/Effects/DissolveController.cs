using System.Collections;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    public float dissolveSpeed = 1.0f; // Speed of the dissolve effect
    private Material dissolveMaterial;
    private bool isDissolving = false;
    private float dissolveValue;
    public float minDissolve = -0.61f;
    public float maxDissolve = 1.2f;
        

    void Start()
    {
        dissolveValue = minDissolve;
        // Get the material of the GameObject this script is attached to
        dissolveMaterial = GetComponent<Renderer>().material;

    
        dissolveMaterial.SetFloat("_Dissolve", dissolveValue);
    }

    // Public method to start the dissolve effect
    public void StartFadingIn()
    {
        if (!isDissolving) // Only start the coroutine if it isn't already running
        {
            AudioManager.Instance.PlaySFX("DissolveIn");
            StartCoroutine(FadeInEffect());
        }
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
    }
}
