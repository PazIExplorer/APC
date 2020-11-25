using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public string Scene;

    public void loadSceneMatiereSelect()
    {
        
        
        SceneManager.LoadScene("MatiereSelect");
    }

    public void loadSceneValide()
    {
        
        SceneManager.LoadScene(Scene);
    }
}
