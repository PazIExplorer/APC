using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

public class AjouteCompetence : MonoBehaviour
{
    public Button button;
    public InputField cours;
    public InputField competence;
    public InputField description;
    private MySqlConnection connection;

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

    public void ajouterCompetence()
    {
        ConnexionBDD();
        string commandText = string.Format("INSERT INTO competence SELECT NULL, c.cours_id, '{1}', '{2}' FROM cours c WHERE cours_name LIKE '{0}%'", cours.text, competence.text, description.text);
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = commandText;
        try
        {
            int nbLignes = command.ExecuteNonQuery();
            if (nbLignes > 0)
            {
                Debug.Log("Compétence créée avec succès.");
            }
            else
            {
                Debug.Log("Erreur lors de la création de compétence.");
            }
        }
        catch (System.Exception ex)
        {
            // Erreur MySQL
            Debug.LogError("MySQL error: " + ex.ToString());
        }
        DeconnexionBDD();
        SceneManager.LoadScene("ProfHub");
    }
}
