using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSelector : MonoBehaviour
{
   public void LoadLevelMatiere(string matiereName)
    {
        SceneManager.LoadScene(matiereName);
    }
}
