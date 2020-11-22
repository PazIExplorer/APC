using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class changementScene : MonoBehaviour
{
    public string LevelToLoad;

    // Input fields / mdp
    public InputField InputId;
    public InputField InputMdp;

    public void connexion()
    {
        // Debugs
        Debug.Log("Identifiant  : " + InputId.text);
        Debug.Log("Mot de passe : " + InputMdp.text);

        // Faire la vérification avec la bdd ici

        // Si connexion ok
        LoadLevel();
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(LevelToLoad);
    }
}
