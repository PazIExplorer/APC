using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelSelector : MonoBehaviour
{
    private string valeur;
    public Button[] matiereButtons;
    public string level;
    private void Start()
    {
        Debug.Log(PlayerPrefs.GetString("competence"));
        if (level == "matiere")
        {
            valeur = PlayerPrefs.GetString("matiere");
        }
        else if(level == "cours"){
            valeur = PlayerPrefs.GetString("cours");
        }
        else if(level == "competence")
        {
            valeur = PlayerPrefs.GetString("competence");
        }
        
        
            //trier les boutons a afficher
        
        
    }

    public void LoadLevelMatiere(string scene)
    {
        SceneManager.LoadScene(scene);
        
    }

    
    
    public void setMatiere(Button button)
    {
        PlayerPrefs.SetString("matiere", button.gameObject.GetComponentInChildren<Text>().text);
    }
    public void setCours(Button button)
    {
        PlayerPrefs.SetString("cours", button.gameObject.GetComponentInChildren<Text>().text);
    }

    public void setCompetence(Button button)
    {
        Debug.Log(button.gameObject.GetComponentInChildren<Text>().text);
        PlayerPrefs.SetString("competenceChoisi",button.gameObject.GetComponentInChildren<Text>().text);
    }
    

}
    
