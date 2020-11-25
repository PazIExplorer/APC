using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValidateDialogue : MonoBehaviour
{
   
    public Text chatText;
    // Start is called before the first frame update
    void Start()
    {
        DialogueValide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DialogueValide()
    {
        chatText.text += "\n"+ PlayerPrefs.GetString("competenceChoisi") + " ?";
    }
}
