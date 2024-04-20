using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BridgePcActions : MonoBehaviour
{
    private bool routeSafe;
    public ProjectorLightChange projectorLight;

    public TMP_InputField speedIndicator;
    public TMP_InputField routeIndicator;

    private float shipSpeed;

    public void CheckRoute(Block block) 
    {
        projectorLight.SwapColor(routeSafe);
        //Save to variable route (needs to be declared in editor)
        block.SaveValue("route", routeSafe);

        if (routeSafe)
        {
            routeIndicator.text = "<color=green>Route is planned</color>";
        }
        else
        {
            routeIndicator.text = "<color=red>Need new route</color>";
        }
    } 

    public void NewRoute(Block block)
    {
        routeSafe = Random.value < 0.5f;
        CheckRoute(block);
    }

    public void ChangeSpeed(Block block)
    {
        if (float.TryParse(block.GetValueByName("speed").ToString(), out float speed))
        {
            if (shipSpeed == speed)
            {
                return;
            }
            StartCoroutine(ChangeSpeedCoroutine(speed));
        }
        else
        {
            block.WriteToDebugField("Wrong input in speed value");
            return;
        }
    }
    private IEnumerator ChangeSpeedCoroutine(float targetSpeed)
    {
        float increment = (targetSpeed > shipSpeed) ? 1f : -1f;

        while (shipSpeed != targetSpeed)
        {
            shipSpeed += increment;
            speedIndicator.text = $"{Mathf.RoundToInt(shipSpeed)}";
            yield return new WaitForSeconds(0.2f);

            // Clamp shipSpeed to exactly targetSpeed to avoid overshooting
            if ((increment > 0f && shipSpeed > targetSpeed) || (increment < 0f && shipSpeed < targetSpeed))
            {
                shipSpeed = targetSpeed;
            }
        }
    }
}
