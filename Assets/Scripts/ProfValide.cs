using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
public class ProfValide : MonoBehaviour
{
    public Button buttonNext;
    public Button buttonPrec;
    public string[] competenceAValide;
    public Text text;
    private MySqlConnection connection;
    private int pos = 0;
    // Start is called before the first frame update
    void Start()
    {
        ConnexionBDD();

        string commandText = string.Format(
            "SELECT competence_name FROM competence WHERE competence_id IN (SELECT competence_id FROM validation_comp WHERE user_id IN (SELECT user_id FROM user WHERE user_name = '{0}') AND validation_type = 1)",
            PlayerPrefs.GetString("nomEtuModif"));

        List<string> competences = new List<string>();

        if (connection != null)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    competences.Add(reader.GetString(0));
                }
                reader.Close();
            }
            catch (System.Exception ex)
            {
                // Erreur MySQL
                Debug.LogError("MySQL error: " + ex.ToString());
            }
        }

        competenceAValide = competences.ToArray();

        SetText();
    }

    // Update is called once per frame
    void Update()
    {
        if (pos >= competenceAValide.Length - 1)
        {
            buttonNext.interactable = false;
        }
        if(pos <= 0)
        {
            buttonPrec.interactable = false;
        }
    }

    private void SetText()
    {
        if (competenceAValide.Length > 0)
        {
            text.text = "Valider la compétence " + competenceAValide[pos] + " à " + PlayerPrefs.GetString("nomEtuModif") + " ?";
        }
        else
        {
            text.text = "Pas de compétences à valider.";
        }
    }

    public void Suivant()
    {
        pos += 1;
        SetText();
        buttonPrec.interactable = true;
    }

    public void Precedent()
    {
        pos -= 1;
        SetText();
        buttonNext.interactable = true;
    }

    public void Valider()
    {
        // Validation
        string commandText = string.Format(
            "UPDATE validation_comp SET validation_type = 2, validation_ts = CURRENT_TIMESTAMP "
            + "WHERE user_id IN (SELECT user_id FROM user WHERE user_name = '{0}') AND competence_id IN (SELECT competence_id FROM competence WHERE competence_name = '{1}')",
            PlayerPrefs.GetString("nomEtuModif"), competenceAValide[pos]);
        if (connection != null)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            try
            {
                int nbLignesModif = command.ExecuteNonQuery();
                if (nbLignesModif > 0)
                {
                    Debug.Log("Compétence validée.");
                }
                else
                {
                    Debug.Log("Erreur validation de la compétence.");
                }
            }
            catch (System.Exception ex)
            {
                // Erreur MySQL
                Debug.LogError("MySQL error: " + ex.ToString());
            }
        }

    }

    public void Refuser()
    {
        // Refus
        string commandText = string.Format(
            "UPDATE validation_comp SET validation_type = 3, validation_ts = CURRENT_TIMESTAMP "
            + "WHERE user_id IN (SELECT user_id FROM user WHERE user_name = '{0}') AND competence_id IN (SELECT competence_id FROM competence WHERE competence_name = '{1}')",
            PlayerPrefs.GetString("nomEtuModif"), competenceAValide[pos]);
        if (connection != null)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            try
            {
                int nbLignesModif = command.ExecuteNonQuery();
                if (nbLignesModif > 0)
                {
                    Debug.Log("Compétence refusée.");
                }
                else
                {
                    Debug.Log("Erreur refus de la compétence.");
                }
            }
            catch (System.Exception ex)
            {
                // Erreur MySQL
                Debug.LogError("MySQL error: " + ex.ToString());
            }
        }

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
