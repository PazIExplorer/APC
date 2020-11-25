using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

public class LoadScene : MonoBehaviour
{
    public string Scene;
    public InputField etudiant;
    private MySqlConnection connection;

    public void loadSceneMatiereSelect()
    {
        
        
        SceneManager.LoadScene("MatiereSelect");
    }
    public void loadSceneProfSelect()
    {
        SceneManager.LoadScene("ProfSelect");
    }

    public void loadCompPropos()
    {
        SceneManager.LoadScene("ProposeComp");
    }

    public void loadSceneValide()
    {
        ConnexionBDD();

        if (connection != null)
        {
            string commandText = string.Format("SELECT user_name FROM user WHERE user_name = '{0}'", etudiant.text);
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                        // Connexion ok
                        PlayerPrefs.SetString("nomEtuModif", reader.GetString(0));
                        Debug.Log("Étudiant trouvé");
                        reader.Close();
                } else
                {
                    Debug.LogError("Étudiant non trouvé");
                }
            }
            catch (System.Exception ex)
            {
                // Erreur MySQL
                Debug.LogError("MySQL error: " + ex.ToString());
            }
        }

        DeconnexionBDD();
        SceneManager.LoadScene(Scene);
    }

    private void ConnexionBDD()
    {
        if (connection == null)
        {
            // INFORMATIONS DE CONNEXION BDD
            string connectionString = "Host=localhost; Port=3306; Database=apc; Uid=root; Pwd=root;";
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("MySQL Error: " + ex.ToString());
            }
        }
    }

    private void DeconnexionBDD()
    {
        if (connection != null)
        {
            connection.Close();
            connection = null;
        }
    }
}
