
using UnityEngine;
using UnityEngine.UI;


public class DialogueTrigger : MonoBehaviour
{
    
    public Dialogue dialogue;
    public Text chatText;

    

    public bool isInRange;
    // Ucalled once
    void Start()
    {
            TriggerDialogue();
       
    }

    

    void TriggerDialogue()
    {
        chatText.text += dialogue.chat;
        
    }
}
