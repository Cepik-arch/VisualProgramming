using TMPro;
using UnityEngine;

public class SetValueToInput : MonoBehaviour
{
    public TMP_InputField inputField;

    public Block blockWithValue;
    public string nameOfValue;

    private float value;
    // Update is called once per frame
    void Update()
    {
        if (inputField != null)
        {

            object objValue = blockWithValue.GetValueByName(nameOfValue);
            if (objValue != null && objValue is float)
            {
                value = (float)objValue;
                inputField.text = value.ToString();


            }
        }
    }
}
