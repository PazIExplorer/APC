﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

public class changementScene : MonoBehaviour
{
    private string levelToLoad;
    private MySqlConnection connection;

    // Input fields / mdp
    public InputField InputId;
    public InputField InputMdp;

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
        string commandText = string.Format("SELECT user_name, user_passhash, user_type, user_id FROM user WHERE user_name = '{0}'", name);
        if (connection != null)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if(name == reader.GetString(0) && pass == reader.GetString(1))
                    {
                        if (reader.GetString(2) == "controleur")
                        {
                            levelToLoad = "ProfHub";
                        }
                        else
                        {
                            levelToLoad = "BlocMega";
                        }
                        // Connexion ok
                        PlayerPrefs.SetInt("userid", reader.GetInt32(3));
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
        // Debugs
        Debug.Log("Identifiant  : " + InputId.text);
        Debug.Log("Mot de passe : " + InputMdp.text);

        // Faire la vérification avec la bdd ici
        ConnexionBDD();
        if (CheckConnexion(InputId.text, InputMdp.text))
        {
            PlayerPrefs.SetString("compte", InputId.text);
            PlayerPrefs.SetString("mdp", InputMdp.text);

            DeconnexionBDD();
            LoadLevel();
        }
        else
        {
            DeconnexionBDD();
        }
    }

    public void inscription()
    {
        // Debugs
        Debug.Log("Identifiant  : " + InputId.text);
        Debug.Log("Mot de passe : " + InputMdp.text);

        ConnexionBDD();

        string commandText = string.Format("INSERT INTO `user` (`user_id`, `user_name`, `user_passhash`, `user_type`, `user_createtime`, `user_fullname`, `user_xp`)"
            + " VALUES (NULL, '{0}', '{1}', 'etudiant', CURRENT_TIMESTAMP, '{0}', '0');", InputId.text, InputMdp.text);
        if (connection != null)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            try
            {
                int ligneModif = command.ExecuteNonQuery();
                if (ligneModif > 0)
                {
                    Debug.Log("Compte créé");
                    if (CheckConnexion(InputId.text, InputMdp.text))
                    {
                        PlayerPrefs.SetString("compte", InputId.text);
                        PlayerPrefs.SetString("mdp", InputMdp.text);

                        DeconnexionBDD();
                        LoadLevel();
                    }
                }
                else
                {
                    Debug.Log("Erreur inscription");
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
    public void LoadLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
