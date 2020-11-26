using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

public class ValidateDialogue : MonoBehaviour
{
   
    public Text chatText;
    private MySqlConnection connection;
    // Start is called before the first frame update
    void Start()
    {
        DialogueValide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DialogueValide()
    {
        chatText.text += "\n"+ PlayerPrefs.GetString("competenceChoisi") + " ?";
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
    public void SetCompetenceEnCoursValidation()
    {
        ConnexionBDD();

        bool validationCompExistsInDB = false;

        if (connection != null)
        {
            string commandText1 = string.Format(
                "SELECT user_id, competence_id FROM validation_comp WHERE user_id = '{0}' AND competence_id IN (SELECT competence_id FROM competence WHERE competence_name = '{1}')",
                PlayerPrefs.GetInt("userid"), PlayerPrefs.GetString("competenceChoisi"));

            MySqlCommand command1 = connection.CreateCommand();
            command1.CommandText = commandText1;
            try
            {
                MySqlDataReader reader = command1.ExecuteReader();
                if (reader.Read())
                {
                    validationCompExistsInDB = true;
                }
                else
                {
                    validationCompExistsInDB = false;
                }
                reader.Close();
            }
            catch (System.Exception ex)
            {
                // Erreur MySQL
                Debug.LogError("MySQL error: " + ex.ToString());
            }

            if (validationCompExistsInDB)
            {
                // Mise à jour de l'entrée existante dans la BDD
                string commandText = string.Format(
                    "UPDATE validation_comp SET validation_type = 1, validation_ts = CURRENT_TIMESTAMP "
                    + "WHERE user_id IN (SELECT user_id FROM user WHERE user_name = '{0}') AND competence_id IN (SELECT competence_id FROM competence WHERE competence_name = '{1}')",
                    PlayerPrefs.GetString("nomEtuModif"), PlayerPrefs.GetString("competenceChoisi"));
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = commandText;
                try
                {
                    int nbLignesModif = command.ExecuteNonQuery();
                    if (nbLignesModif > 0)
                    {
                        Debug.Log("Compétence mise en demande de validation. (UPDATE)");
                    }
                    else
                    {
                        Debug.Log("Erreur demande de validation de la compétence. (UPDATE)");
                    }
                }
                catch (System.Exception ex)
                {
                    // Erreur MySQL
                    Debug.LogError("MySQL error: " + ex.ToString());
                }
            }
            else
            {
                // Création de l'entrée dans la BDD
                string commandText = string.Format(
                "INSERT INTO validation_comp (`user_id`, `competence_id`, `validation_type`, `validation_ts`) "
                + "SELECT '{0}', c.competence_id, 1, CURRENT_TIMESTAMP FROM competence c WHERE competence_name = '{1}'",
                PlayerPrefs.GetInt("userid"), PlayerPrefs.GetString("competenceChoisi"));
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = commandText;
                try
                {
                    int nbLignesModif = command.ExecuteNonQuery();
                    if (nbLignesModif > 0)
                    {
                        Debug.Log("Compétence mise en demande de validation. (INSERT)");
                    }
                    else
                    {
                        Debug.Log("Erreur demande de validation de la compétence. (INSERT)");
                    }
                }
                catch (System.Exception ex)
                {
                    // Erreur MySQL
                    Debug.LogError("MySQL error: " + ex.ToString());
                }
            }
            
        }

        DeconnexionBDD();
    }
}