using TMPro;
using UnityEngine;

public class ChangeScreen : MonoBehaviour
{
    public TMP_InputField valueInput;
    private float inputValue;

    public float minValue;
    public float maxValue;

    public GameObject falseIMG;
    public GameObject trueIMG;

    // Update is called once per frame
    void Update()
    {

        inputValue = float.Parse(valueInput.text);

        if (falseIMG != null && trueIMG != null)
        {
            if (inputValue >= minValue && inputValue <= maxValue)
            {
                trueIMG.SetActive(true);
                falseIMG.SetActive(false);

            }
            else
            {
                trueIMG.SetActive(false);
                falseIMG.SetActive(true);
            }
        }
    }
}
