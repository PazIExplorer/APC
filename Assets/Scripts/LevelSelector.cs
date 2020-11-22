using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelSelector : MonoBehaviour
{
    public string valeur;
    public Button[] matiereButtons;

    private void Start()
    {
        if (valeur != null)
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
}
