using UnityEngine;

public class ProjectorLightChange : MonoBehaviour
{
    public Light lightHologram;
    public GameObject hologram;

    [Header("Materials for projection")]
    public Material materialDefault;
    public Material materialAlert;

    private Material hologramMaterial;
    private Color lightColor;

    //Swap colors of light and projector material
    public void SwapColor(bool routeSafe)
    {
        if (materialDefault != null && routeSafe == true)
        {
            hologramMaterial = materialDefault;
            lightColor = Color.blue;
        }

        if (materialAlert != null && routeSafe == false)
        {
            hologramMaterial = materialAlert;
            lightColor = Color.red;
        }

        if (hologramMaterial != null && lightColor != null)
        {
            hologram.GetComponent<MeshRenderer>().material = hologramMaterial;
            lightHologram.color = lightColor;
        }

    }
}
