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

        string commandText = string.Format(
            "INSERT INTO validation_comp (`user_id`, `competence_id`, `validation_type`, `validation_ts`) "
            + "VALUES ('{0}', '{1}', 1, CURRENT_TIMESTAMP)",
            "test", 1); // TODO: Utiliser les valeurs de PlayerPrefs
        if (connection != null)
        {
            Debug.Log("Dialogue trigger");
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            try
            {
                int nbLignesModif = command.ExecuteNonQuery();
                if (nbLignesModif > 0)
                {
                    Debug.Log("Compétence mise en demande de validation.");
                } else
                {
                    Debug.Log("Erreur demande de validation de la compétence.");
                }
            }
            catch (System.Exception ex)
            {
                // Erreur MySQL
                Debug.LogError("MySQL error: " + ex.ToString());
            }
        }



        DeconnexionBDD();
    }
}