
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

public class DialogueTrigger : MonoBehaviour
{
    public string validite;
    public Dialogue dialogue;
    public Text chatText;
    private MySqlConnection connection;
    private string compte;


    // Ucalled once
    void Start()
    {
        compte = PlayerPrefs.GetString("compte");
        ConnexionBDD();
        TriggerDialogue();
        DeconnexionBDD();
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
    
    public void TriggerDialogue()
    {
        string commandText = "";
        // chatText.text += dialogue.chat;
        if(validite == "valide")
        {
             commandText = string.Format(
            "SELECT co.cours_name, c.competence_name, c.competence_description, v.* FROM validation_comp v, competence c, cours co "
            + "WHERE v.user_id IN (SELECT user_id FROM user WHERE user_name = '{0}') "
            + "AND v.validation_type = 2 AND v.competence_id = c.competence_id AND c.cours_id = co.cours_id",
            compte);
        }else
        if(validite == "pending")
        {
             commandText = string.Format(
             "SELECT co.cours_name, c.competence_name, c.competence_description, v.* FROM validation_comp v, competence c, cours co "
             + "WHERE v.user_id IN (SELECT user_id FROM user WHERE user_name = '{0}') "
             + "AND v.validation_type = 1 AND v.competence_id = c.competence_id AND c.cours_id = co.cours_id",
             compte);
        }else
        if(validite == "refuse")
        {
             commandText = string.Format(
                        "SELECT co.cours_name, c.competence_name, c.competence_description, v.* FROM validation_comp v, competence c, cours co "
                        + "WHERE v.user_id IN (SELECT user_id FROM user WHERE user_name = '{0}') "
                        + "AND v.validation_type = 0 AND v.competence_id = c.competence_id AND c.cours_id = co.cours_id",
                        compte);
        }
        else
        {
            
            return;
        }
        
        if (connection != null)
        {
            Debug.Log("Dialogue trigger");
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    chatText.text += "\nCours " + reader.GetString(0) + " - " + reader.GetString(1) + " (" + reader.GetString(2) + ")";
                }
                // Fin de la lecture
                reader.Close();
            }
            catch (System.Exception ex)
            {
                // Erreur MySQL
                Debug.LogError("MySQL error: " + ex.ToString());
            }
        }
    }
}
