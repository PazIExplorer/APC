
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

        if(isInRange)
        {
            TriggerDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    void TriggerDialogue()
    {
        chatText.text = "Voulez vous faire valider:    ";
        chatText.text += dialogue.chat;
        chatText.text += " ?";
    }
}
