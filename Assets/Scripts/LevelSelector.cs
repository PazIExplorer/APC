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
        
        
        
        
            //trier les boutons a afficher
            //tout mettre en setactive false
            //appeler la base de donnee sur le bon truc donc if level == matiere on appel matiere etc
            //for matiere[i] i allant de 0 au max de matiere (par exemple)
            //matiere[i].text = table[i].text
            //matiere[i].setactive true
        
        
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
    
