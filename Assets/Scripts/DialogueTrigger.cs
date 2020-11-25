
using UnityEngine;
using UnityEngine.UI;


public class DialogueTrigger : MonoBehaviour
{
    
    public Dialogue dialogue;
    public Text chatText;

    

    
    // Ucalled once
    void Start()
    {
            TriggerDialogue();
       
    }

    

    public void TriggerDialogue()
    {
        chatText.text += dialogue.chat;
        
    }
}
