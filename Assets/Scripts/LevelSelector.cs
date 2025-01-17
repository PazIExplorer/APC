﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Collections.Generic;

public class LevelSelector : MonoBehaviour
{
    
    public Button[] matiereButtons;
    public string level;
    private string[] matieres;
    private MySqlConnection connection;
    private void Start()
    {


        if (level == "matiere")
        {
            matieres = GetMatieres();
        }else if(level == "cours")
        {
            matieres = GetCours();
        }else if(level == "competence")
        {
            matieres = GetCompetences();
        }else
        {
            return;
        }
        
            //trier les boutons a afficher
            //tout mettre en setactive false
            //appeler la base de donnee sur le bon truc donc if level == matiere on appel matiere etc
            //for matiere[i] i allant de 0 au max de matiere (par exemple)
            //matiere[i].text = table[i].text
            //matiere[i].setactive true
            for(int i = 0; i < matiereButtons.Length; i++)
        {
            matiereButtons[i].gameObject.SetActive(false);
        }

            for(int i=0; i < matieres.Length; i++)
        {
            matiereButtons[i].gameObject.GetComponentInChildren<Text>().text = matieres[i];
            matiereButtons[i].gameObject.SetActive(true);
        }
        
        
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
        ConnexionBDD();
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

        DeconnexionBDD();
        return matieres.ToArray();
    }

    public string[] GetCours()
    {
        ConnexionBDD();
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

        DeconnexionBDD();
        return cours.ToArray();
    }

    public string[] GetCompetences()
    {
        ConnexionBDD();
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

        DeconnexionBDD();
        return comp.ToArray();
    }

}
    
