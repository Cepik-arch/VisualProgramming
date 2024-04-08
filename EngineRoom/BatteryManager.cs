using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryManager : MonoBehaviour
{
    [System.Serializable]
    public class BatteryData
    {
        public Renderer batteryRenderer; // Reference to the object's renderer with the battery material
        public Material lowBatteryMaterial;
        public Material fullBatteryMaterial;
        [Range(0.0f, 1.0f)]
        public float batteryLevel = 1.0f;
    }

    public List<BatteryData> batteries = new List<BatteryData>();
    public float transitionDuration = 1.0f; // Duration of the transition in seconds

    private Coroutine[] transitionCoroutines;

    private void Start()
    {
        // Initialize transition coroutine array
        transitionCoroutines = new Coroutine[batteries.Count];
    }

    public void ChangeBattery()
    {
        for (int i = 0; i < batteries.Count; i++)
        {
            SetBatteryLevel(i, batteries[i].batteryLevel);
        }
    }

    // Call this method to update the battery level of a specific battery
    private void SetBatteryLevel(int batteryIndex, float newBatteryLevel)
    {
        batteries[batteryIndex].batteryLevel = Mathf.Clamp01(newBatteryLevel); // Clamp between 0 and 1

        // If a transition is already in progress for this battery, stop it
        if (transitionCoroutines[batteryIndex] != null)
        {
            StopCoroutine(transitionCoroutines[batteryIndex]);
        }

        // Start a new transition coroutine
        transitionCoroutines[batteryIndex] = StartCoroutine(TransitionBatteryMaterial(batteryIndex));
    }

    // Coroutine to smoothly transition battery material properties
    private IEnumerator TransitionBatteryMaterial(int batteryIndex)
    {
        BatteryData battery = batteries[batteryIndex];
        Material startMaterial = battery.batteryRenderer.material;
        Material targetMaterial = battery.batteryLevel > 0.5f ? battery.fullBatteryMaterial : battery.lowBatteryMaterial;

        Color startEmissionColor = startMaterial.GetColor("_EmissionColor");
        Color targetEmissionColor = targetMaterial.GetColor("_EmissionColor");

        float elapsedTime = 0.0f;
        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            Color lerpedColor = Color.Lerp(startEmissionColor, targetEmissionColor, t);

            // Update the emission color property of the battery material
            startMaterial.SetColor("_EmissionColor", lerpedColor);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the material ends on the target emission color
        startMaterial.SetColor("_EmissionColor", targetEmissionColor);

        // Reset the transition coroutine reference
        transitionCoroutines[batteryIndex] = null;
    }
}
