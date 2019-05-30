using UnityEngine;
using System.Collections;

public class FireLight : MonoBehaviour
{

    private float totalSeconds;     // The total of seconds the flash wil last
    public float maxTime;
    public float maxBetweenTime;
    public float maxIntensity;     // The maximum intensity the flash will reach
    public Light myLight;        // Your light
    private bool isFlashing = false;
    public GameObject emission;
    

    public void Update()
    {
        if (!isFlashing)
            StartCoroutine(flashNow(Random.Range(0, maxTime)));
    }
    public IEnumerator flashNow(float seconds)
    {
        isFlashing = true;
        totalSeconds = seconds;
        float waitTime = totalSeconds / 2;
        // Get half of the seconds (One half to get brighter and one to get darker)
        while (myLight.intensity < maxIntensity)
        {
            if(emission!=null)
                emission.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            myLight.intensity += Time.deltaTime / waitTime;        // Increase intensity
            yield return null;
        }
        while (myLight.intensity > 0)
        {
            if (emission != null)
                emission.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            myLight.intensity -= Time.deltaTime / waitTime;
            yield return null;
        }
        yield return new WaitForSeconds(Random.Range(0, maxBetweenTime));
        isFlashing = false;
        yield return null;
    }
}