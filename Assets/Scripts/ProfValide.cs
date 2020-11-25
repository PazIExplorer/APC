using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfValide : MonoBehaviour
{
    public Button buttonNext;
    public Button buttonPrec;
    public string[] competenceAValide;
    public Text text;
    private int pos = 0;
    // Start is called before the first frame update
    void Start()
    {
        text.text = competenceAValide[pos];
    }

    // Update is called once per frame
    void Update()
    {
        if (pos == competenceAValide.Length)
        {
            buttonNext.interactable = false;
        }
        if(pos == 0)
        {
            buttonPrec.interactable = false;
        }
    }

    public void Suivant()
    {
        pos += 1;
    }

    public void Precedent()
    {
        pos -= 1;
    }

    public void Valider()
    {
        //envoie en base de valide
    }

    public void Refuser()
    {
        //envoie en base de refus
    }
}
