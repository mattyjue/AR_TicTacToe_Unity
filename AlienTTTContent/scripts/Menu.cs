using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Menu : MonoBehaviour
{

    public int window;
    public static int level;
    public GUIStyle styles;
    public string difficulty;


    void Start()
    {
        window = 1;
    }
    void OnGUI()
    {
        GUI.BeginGroup(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200));
        if (window == 1)
        {
            GUI.Label(new Rect(50, 10, 180, 30), "Choose Difficulty");
            if (GUI.Button(new Rect(10, 40, 180, 30), "Easy"))
            {
                difficulty = "Easy";
                SceneManager.LoadScene("gameScene");
            }
            if (GUI.Button(new Rect(10, 80, 180, 30), "Medium"))
            {
                difficulty = "Medium"; 
                SceneManager.LoadScene("gameScene");
            }
            if (GUI.Button(new Rect(10, 120, 180, 30), "Hard"))
            {
                difficulty = "Hard";
                SceneManager.LoadScene("gameScene");
            }
            if (GUI.Button(new Rect(10, 160, 180, 30), "Exit"))
            {
                window = 2;
            }
        }
        if (window == 2)
        {
            GUI.Label(new Rect(50, 10, 180, 30), "Do you want to leave us?");
            if (GUI.Button(new Rect(10, 40, 180, 30), "Yes"))
            {
                Application.Quit();
            }
            if (GUI.Button(new Rect(10, 80, 180, 30), "No"))
            {
                window = 1;
            }
        }
        GUI.EndGroup();
    }

}
