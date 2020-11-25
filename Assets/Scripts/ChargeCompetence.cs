using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

public class ChargeCompetence : MonoBehaviour
{
    private MySqlConnection connection;
    private string connectionString = "Host=localhost; Port=3306; Database=apc; Uid=root; Pwd=root;";
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        connexion();
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

    private bool CheckConnexion(string name, string pass)
    {
        string commandText = string.Format("SELECT user_name, user_passhash FROM user WHERE user_name = '{0}'", name);
        if (connection != null)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (name == reader.GetString(0) && pass == reader.GetString(1))
                    {
                        // Connexion ok
                        Debug.Log("connexion ok");
                        reader.Close();
                        return true;
                    }
                }
                // ID ou mot de passe invalide
                Debug.Log("connexion pas ok : nom ou mdp invalide");
                reader.Close();
                return false;
            }
            catch (System.Exception ex)
            {
                // Erreur MySQL
                Debug.LogError("MySQL error: " + ex.ToString());
                return false;
            }
        }
        // Pas de connexion
        return false;
    }
    public void connexion()
    {
        string compte = PlayerPrefs.GetString("compte");
        string mdp = PlayerPrefs.GetString("mdp");
        string commande = string.Format("SELECT * FROM competence WHERE competence_id IN (SELECT competence_id FROM validation_comp WHERE user_id IN (SELECT user_id FROM user WHERE user_name = '{0}'))", compte);
        // Faire la vérification avec la bdd ici
        ConnexionBDD();
        if (CheckConnexion(compte, mdp))
        {
            MySqlCommand ordre = connection.CreateCommand();
            ordre.CommandText = commande;
            try
            {
                MySqlDataReader reader = ordre.ExecuteReader();
                while (reader.Read())
                {
                    button.GetComponentInChildren<Text>().text += reader.GetString(0);
                }
                reader.Close();

            }
            catch (System.Exception ex1)
            {
                button.GetComponentInChildren<Text>().text += "Pas de connexion";
                // Erreur MySQL
                Debug.LogError("MySQL error: " + ex1.ToString());
            }
            

            DeconnexionBDD();
            
        }
        else
        {
            DeconnexionBDD();
        }
    }

}
