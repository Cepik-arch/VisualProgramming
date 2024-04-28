using TMPro;
using UnityEngine;

public class ChangeQuest : MonoBehaviour
{
    public string questTitle;
    public string questDescription;

    private TMP_Text titleField;
    private TMP_Text textField;

    void Start()
    {
        titleField = GameObject.Find("QuestName").GetComponent<TMP_Text>();
        textField = GameObject.Find("QuestText").GetComponent<TMP_Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (questTitle != null && questTitle != "")
            {
                titleField.text = questTitle;
            }
            if (questTitle != null && questDescription != "")
            {
                textField.text = questDescription;
            }
            Destroy(gameObject);
        }
    }

    public void ChangeQuestText()
    {
        titleField.text = questTitle;
        textField.text = questDescription;
    }
}
