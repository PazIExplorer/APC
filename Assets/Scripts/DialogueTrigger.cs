
using UnityEngine;
using UnityEngine.UI;


public class DialogueTrigger : MonoBehaviour
{
    
    public Dialogue dialogue;
    public Text chatText;

    

    public bool isInRange;
    // Update is called once per frame
    void Update()
    {
            TriggerDialogue();
       
    }

    

    void TriggerDialogue()
    {
        chatText.text = "Voulez vous faire valider:    ";
        chatText.text += dialogue.chat;
        chatText.text += " ?";
    }
}
