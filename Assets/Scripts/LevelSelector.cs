using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Collections.Generic;

public class LevelSelector : MonoBehaviour
{
    private string valeur;
    public Button[] matiereButtons;
    public string level;

    private MySqlConnection connection;
    private void Start()
    {
        
        
        
        
            //trier les boutons a afficher
            //tout mettre en setactive false
            //appeler la base de donnee sur le bon truc donc if level == matiere on appel matiere etc
            //for matiere[i] i allant de 0 au max de matiere (par exemple)
            //matiere[i].text = table[i].text
            //matiere[i].setactive true
        
        
    }

    public void LoadLevelMatiere(string scene)
    {
        SceneManager.LoadScene(scene);
        
    }

    
    
    public void setMatiere(Button button)
    {
        PlayerPrefs.SetString("matiereChoisi", button.gameObject.GetComponentInChildren<Text>().text);
    }
    public void setCours(Button button)
    {
        PlayerPrefs.SetString("coursChoisi", button.gameObject.GetComponentInChildren<Text>().text);
    }

    public void setCompetence(Button button)
    {
        Debug.Log(button.gameObject.GetComponentInChildren<Text>().text);
        PlayerPrefs.SetString("competenceChoisi",button.gameObject.GetComponentInChildren<Text>().text);
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

    public string[] GetMatieres()
    {
        var matieres = new List<string>();

        string commandText = string.Format("SELECT matiere_name FROM matiere");
        if (connection != null)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    matieres.Add(reader.GetString(0));
                }
                reader.Close();
            }
            catch (System.Exception ex)
            {
                // Erreur MySQL
                Debug.LogError("MySQL error: " + ex.ToString());
            }
        }

        return matieres.ToArray();
    }

    public string[] GetCours()
    {
        var cours = new List<string>();

        string commandText = string.Format("SELECT cours_name FROM cours WHERE matiere_id IN (SELECT matiere_id FROM matiere WHERE matiere_name = '{0}')", PlayerPrefs.GetString("matiereChoisi"));
        if (connection != null)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cours.Add(reader.GetString(0));
                }
                reader.Close();
            }
            catch (System.Exception ex)
            {
                // Erreur MySQL
                Debug.LogError("MySQL error: " + ex.ToString());
            }
        }

        return cours.ToArray();
    }

    public string[] GetCompetences()
    {
        var comp = new List<string>();

        string commandText = string.Format("SELECT competence_name FROM competence WHERE cours_id IN (SELECT cours_id FROM cours WHERE cours_name = '{0}')", PlayerPrefs.GetString("coursChoisi"));
        if (connection != null)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comp.Add(reader.GetString(0));
                }
                reader.Close();
            }
            catch (System.Exception ex)
            {
                // Erreur MySQL
                Debug.LogError("MySQL error: " + ex.ToString());
            }
        }

        return comp.ToArray();
    }

}
    
