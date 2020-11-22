using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

public class changementScene : MonoBehaviour
{
    public string LevelToLoad;
    private MySqlConnection connection;

    // Input fields / mdp
    public InputField InputId;
    public InputField InputMdp;

    private void ConnexionBDD()
    {
        if (connection == null)
        {
            // INFORMATIONS DE CONNEXION BDD
            string connectionString = "Host=localhost; Port=3306; Database=apc; Uid=apc; Pwd=lemotdepasse;";
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
        }
    }

    private void TestBDD()
    {
        string commandText = string.Format("SELECT user_name, user_passhash FROM user");
        if (connection != null)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Debug.Log(reader.GetString(0) + ", " + reader.GetString(1));
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("MySQL error: " + ex.ToString());
            }
        }
    }

    public void connexion()
    {
        // Debugs
        Debug.Log("Identifiant  : " + InputId.text);
        Debug.Log("Mot de passe : " + InputMdp.text);

        // Faire la vérification avec la bdd ici
        ConnexionBDD();
        TestBDD();
        DeconnexionBDD();

        // Si connexion ok
        LoadLevel();
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(LevelToLoad);
    }
}
