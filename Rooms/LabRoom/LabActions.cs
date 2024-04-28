using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlanetDistance
{
    public string name { get; set; }
    public float distance { get; set; }

    public PlanetDistance(string name, float distance)
    {
        this.name = name;
        this.distance = distance;
    }
}

public class LabActions : MonoBehaviour
{
    [HideInInspector]
    public List<PlanetDistance> planetDistances = new List<PlanetDistance>();
    [HideInInspector]
    public float planetXDistance;
    [HideInInspector]
    public float planetYDistance;

    public TMP_Text planetsText;

    private int x;
    private int y;


    void Awake()
    {
        planetDistances = new()
        {
            new PlanetDistance("Sirius", 8),
            new PlanetDistance("Alpha Centauri", 4),
            new PlanetDistance("Betelgeuse", 643),
            new PlanetDistance("Proxima Centauri", 3),
            new PlanetDistance("Polaris", 434),
            new PlanetDistance("Antares", 550),
            new PlanetDistance("Vega", 25),
            new PlanetDistance("Deneb", 1550),
            new PlanetDistance("Arcturus", 37),
            new PlanetDistance("Rigel", 860)
        };
    }

    public void GetPlanetX(Block block)
    {
        if (int.TryParse(block.GetValueByName("x").ToString(), out  x))
        {
            if (x >= 0 && x < planetDistances.Count)
            {
                planetXDistance = planetDistances[x].distance;

                Variable existingVariable = Block.variables.Find(v => v.name == "planetX");

                if (existingVariable != null)
                {
                    // Update the existing variable's value
                    existingVariable.value = planetXDistance;
                    Debug.Log($"Updated existing variable 'planetX' with value: {planetXDistance}");
                }
                else
                {
                    // Create a new Variable instance and add it to Block.variables
                    Variable newVariable = new Variable() { name = "planetX", value = planetXDistance };
                    Block.variables.Add(newVariable);
                    Debug.Log($"Added new variable 'planetX' with value: {planetXDistance}");
                }
            }
            else
            {
                block.WriteToDebugField("Invalid planet index for x value");
            }
        }
        else
        {
            block.WriteToDebugField("Wrong input in x value");
        }
    }

    public void GetPlanetY(Block block)
    {
        if (int.TryParse(block.GetValueByName("y").ToString(), out  y))
        {
            if (y >= 0 && y < planetDistances.Count)
            {
                planetYDistance = planetDistances[y].distance;

                Variable existingVariable = Block.variables.Find(v => v.name == "planetY");

                if (existingVariable != null)
                {
                    // Update the existing variable's value
                    existingVariable.value = planetYDistance;
                    Debug.Log($"Updated existing variable 'planetY' with value: {planetYDistance}");
                }
                else
                {
                    // Create a new Variable instance and add it to Block.variables
                    Variable newVariable = new Variable() { name = "planetY", value = planetYDistance };
                    Block.variables.Add(newVariable);
                    Debug.Log($"Added new variable 'planetY' with value: {planetYDistance}");
                }
            }
            else
            {
                block.WriteToDebugField("Invalid planet index for y value");
            }
        }
        else
        {
            block.WriteToDebugField("Wrong input in y value");
        }
    }

    public void SwapPlanets(Block block)
    {
        if (x >= 0 && x < planetDistances.Count && y >= 0 && y < planetDistances.Count)
        {
            // Swap planets at indices x and y in the planetDistances list
            PlanetDistance temp = planetDistances[x];
            planetDistances[x] = planetDistances[y];
            planetDistances[y] = temp;

            block.WriteToDebugField($"Swapped planets {x} and {y}");
        }
        else
        {
            block.WriteToDebugField("Invalid planet indices for swapping");
        }
    }

    public void PrintPlanets()
    {
        string planetsInfo = null;
        foreach (PlanetDistance planet in planetDistances)
        {
            planetsInfo += $"{planet.name} ({planet.distance} light-years), ";
            planetsText.text = planetsInfo;
        }
    }

}
