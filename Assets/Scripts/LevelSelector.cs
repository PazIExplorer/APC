using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelSelector : MonoBehaviour
{
    private string valeur;
    public Button[] matiereButtons;

    private void Start()
    {
        Debug.Log(PlayerPrefs.GetString("competence"));
        valeur = PlayerPrefs.GetString("competence");
        if (valeur != "Untagged")
        {
            for (int i = 0; i < matiereButtons.Length; i++)
            {
                matiereButtons[i].gameObject.SetActive(false);
                if (matiereButtons[i].gameObject.CompareTag(valeur))
                {
                    matiereButtons[i].gameObject.SetActive(true);
                }

                
            }
        }
        else
        {
            for (int i = 0; i < matiereButtons.Length; i++)
            {
                matiereButtons[i].gameObject.SetActive(true);
               
            }
        }
    }
    public void LoadLevelMatiere(string scene)
    {
        SceneManager.LoadScene(scene);
        
    }

    public void setButtonTag(Button tagButton)
    {
        PlayerPrefs.SetString("competence",tagButton.gameObject.tag);
        
    }
    
    
    public void setOwnTag(Button button)
    {
        
        button.gameObject.tag = button.gameObject.name;
    }
    
    

}
    
