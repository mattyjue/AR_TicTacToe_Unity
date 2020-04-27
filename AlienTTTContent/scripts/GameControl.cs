using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    bool move = true;
    bool winPlayer = false;
    bool winAlien = false;
    bool equal = false;
    public Transform[] modelPlaces;
    public GameObject[] planePlaces;
    public Transform[] modelPrefabs;
    public GameObject winContent;
    public GameObject loseContent;
    public GameObject tieContent;

    int i = 0;
    int a = 0;
    int b = 0;
    public char[] OX;
    GameObject[] objectLocations;
    GameObject currentPoint;
    int index;
    public string AirClick;
    public string level;
    public bool Clicked = false;
    public bool hit;
    int[] corners = new int[] { 0, 2, 6, 8 };
    int[] toprow = new int[] { 0, 1, 2 };
    int[] middlerow = new int[] { 3, 4, 5 };
    int[] bottomrow = new int[] { 6, 7, 8 };
    int[] firstcolumn = new int[] { 0, 3, 6 };
    int[] sencondcolumn = new int[] { 1, 4, 7 };
    int[] thirdcolumm = new int[] { 2, 5, 8 };
    int[] diagnol1 = new int[] { 0, 4, 8 };
    int[] diagnol2 = new int[] { 2, 4, 6 };
    int[] nottoprow = new int[] { 3, 4, 5, 6, 7, 8 };
    int[] notbottomrow = new int[] { 0, 1, 2, 3, 4, 5 };
    int[] notmiddlerow = new int[] { 0, 1, 2, 6, 7, 8 };
    int[] notdiagnol1 = new int[] { 1, 2, 3, 5, 6, 7 };
    int[] notdiagnol2 = new int[] { 0, 1, 3, 7, 8 };
    int[] notfirstcolumn = new int[] { 1, 2, 4, 5, 7, 8 };
    int[] notsecondcolumn = new int[] { 0, 2, 3, 5, 6, 8 };
    int[] notthirdcolumn = new int[] { 0, 1, 3, 4, 6, 7 };

    Transform X;
    Transform O;

    void Start()
    {
        OX = new char[9];
        level = GetComponent<Menu>().difficulty;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();


        if (move)
        {


            if ((Input.GetMouseButtonUp(0) || Clicked) && winAlien == false && equal == false)
            {

                RaycastHit hitInfo = new RaycastHit();
                if (Clicked == true)
                {
                    hit = true;

                }

                if (Clicked == false)
                {
                    hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

                }
                if (hit)
                {
                    if (Clicked == true)
                        AirClick = GameObject.Find("scriptContainer").GetComponent<RadialButton>().hitInfo.collider.name;


                    if (Clicked == false)
                        AirClick = hitInfo.collider.name;

                    a++;
                    move = false;
                    for (int i = 0; i < 9; i++)
                    {
                        if (AirClick == planePlaces[i].name)
                        {
                            OX[i] = 'X';
                            Debug.Log("i: " + i);
                            GameObject.Find(planePlaces[i].name).SetActive(false);
                            X = Instantiate(modelPrefabs[0], planePlaces[i].transform.position, planePlaces[i].transform.rotation) as Transform;
                            X.transform.parent = GameObject.Find("ImageTarget").transform;
                            X.name = "x" + i;
                            Debug.Log("XXXXXXXXXXXXXX:" + X);
                            Vector3 pos = X.transform.position;
                            X.transform.position = new Vector3(pos.x, pos.y + 0.4f, pos.z);
                        }
                    }

                    if (OX[0] == 'X' && OX[1] == 'X' && OX[2] == 'X')
                    {
                        winPlayer = true;
                        Debug.Log("You Win! - 1st row " + OX[0] + " " + OX[1] + " " + OX[2]);
                    }
                    if (OX[3] == 'X' && OX[4] == 'X' && OX[5] == 'X')
                    {
                        winPlayer = true;
                        Debug.Log("You Win! - 2nd row " + OX[3] + " " + OX[4] + " " + OX[5]);
                    }
                    if (OX[6] == 'X' && OX[7] == 'X' && OX[8] == 'X')
                    {
                        winPlayer = true;
                        Debug.Log("You Win! - 3rd row " + OX[6] + " " + OX[7] + " " + OX[8]);
                    }
                    if (OX[0] == 'X' && OX[3] == 'X' && OX[6] == 'X')
                    {
                        winPlayer = true;
                        Debug.Log("You Win! - 1st column " + OX[0] + " " + OX[3] + " " + OX[6]);
                    }
                    if (OX[1] == 'X' && OX[4] == 'X' && OX[7] == 'X')
                    {
                        winPlayer = true;
                        Debug.Log("You Win! - 2nd column " + OX[1] + " " + OX[4] + " " + OX[7]);
                    }
                    if (OX[2] == 'X' && OX[5] == 'X' && OX[8] == 'X')
                    {
                        winPlayer = true;
                        Debug.Log("You Win! - 3rd column " + OX[2] + " " + OX[5] + " " + OX[8]);
                    }
                    if (OX[0] == 'X' && OX[4] == 'X' && OX[8] == 'X')
                    {
                        winPlayer = true;
                        Debug.Log("You Win! - 1st Diagonal " + OX[0] + " " + OX[4] + " " + OX[8]);
                    }
                    if (OX[2] == 'X' && OX[4] == 'X' && OX[6] == 'X')
                    {
                        winPlayer = true;
                        Debug.Log("You Win! - 2nd Diagonal  " + OX[2] + " " + OX[4] + " " + OX[6]);
                    }
                    if (a == 5 && winPlayer == false)
                    {
                        equal = true;
                        Debug.Log("Equal " + a);
                    }

                }
                Clicked = false;
            }
            
            if (winPlayer || winAlien || equal)
            {
                if (Clicked == true)
                    AirClick = GameObject.Find("scriptContainer").GetComponent<RadialButton>().hitInfo.collider.name;

                if (winPlayer)
                {
                    winContent.SetActive(true);
                    Debug.Log("PLAYER WINS");

                }
                if (winAlien)
                {
                    loseContent.SetActive(true);
                    Debug.Log("COMPUTER WINS");
                    // turn victory model/effects
                }
                if (equal)
                {
                    tieContent.SetActive(true);
                    Debug.Log("EQUAL");
                    // turn victory model/effects
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                if (hitInfo.collider.name == "replayButton")
                    replay();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                if (hitInfo.collider.name == "returnMenu")
                    returnMenu();
            }
        }
        if (move == false)
        {
            objectLocations = GameObject.FindGameObjectsWithTag("Plane");
            index = UnityEngine.Random.Range(0, objectLocations.Length);
            currentPoint = objectLocations[gameLevel(level, index, a)];
            Debug.Log("PC: " + currentPoint.name);
            move = true;
            b++;
            for (int i = 0; i < 9; i++)
            {
                if (currentPoint.name == planePlaces[i].name)
                {
                    OX[i] = 'O';
                    Debug.Log("i: " + i);
                    GameObject.Find(planePlaces[i].name).SetActive(false);
                    O = Instantiate(modelPrefabs[1], planePlaces[i].transform.position, planePlaces[i].transform.rotation) as Transform;
                    O.transform.parent = GameObject.Find("ImageTarget").transform;
                    O.name = "o" + i;
                    Vector3 pos = O.transform.position;
                    O.transform.position = new Vector3(pos.x, pos.y + 0.4f, pos.z);
                }
            }


            if (OX[0] == 'O' && OX[1] == 'O' && OX[2] == 'O')
            {
                winAlien = true;
                Debug.Log("Computer Wins! - 1st row " + OX[0] + " " + OX[1] + " " + OX[2]);
            }
            if (OX[3] == 'O' && OX[4] == 'O' && OX[5] == 'O')
            {
                winAlien = true;
                Debug.Log("Computer Wins! - 2nd row " + OX[3] + " " + OX[4] + " " + OX[5]);
            }
            if (OX[6] == 'O' && OX[7] == 'O' && OX[8] == 'O')
            {
                winAlien = true;
                Debug.Log("Computer Wins! - 3rd row " + OX[6] + " " + OX[7] + " " + OX[8]);
            }
            if (OX[0] == 'O' && OX[3] == 'O' && OX[6] == 'O')
            {
                winAlien = true;
                Debug.Log("Computer Wins! - 1st column " + OX[0] + " " + OX[3] + " " + OX[6]);
            }
            if (OX[1] == 'O' && OX[4] == 'O' && OX[7] == 'O')
            {
                winAlien = true;
                Debug.Log("Computer Wins! - 2nd column " + OX[1] + " " + OX[4] + " " + OX[7]);
            }
            if (OX[2] == 'O' && OX[5] == 'O' && OX[8] == 'O')
            {
                winAlien = true;
                Debug.Log("Computer Wins! - 3rd column " + OX[2] + " " + OX[5] + " " + OX[8]);
            }
            if (OX[0] == 'O' && OX[4] == 'O' && OX[8] == 'O')
            {
                winAlien = true;
                Debug.Log("Computer Wins! - 1st Diagonal " + OX[0] + " " + OX[4] + " " + OX[8]);
            }
            if (OX[2] == 'O' && OX[4] == 'O' && OX[6] == 'O')
            {
                winAlien = true;
                Debug.Log("Computer Wins! - 2nd Diagonal  " + OX[2] + " " + OX[4] + " " + OX[6]);
            }
            if (b == 5 && winAlien == false)
            {
                equal = true;
                Debug.Log("Equal " + b);
            }
        }
    }



    public void returnMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void replay()
    {

        if (GameObject.FindGameObjectsWithTag("XOdelete") != null)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("XOdelete").Length; i++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("XOdelete")[i]);
            }
        }

        for (int i = 0; i < 9; i++)
        {
            if (!planePlaces[i].activeSelf)
                planePlaces[i].SetActive(true);
        }


        move = true;
        winPlayer = false;
        winAlien = false;
        equal = false;
        Clicked = false;
        tieContent.SetActive(false);
        loseContent.SetActive(false);
        winContent.SetActive(false);

        a = 0;
        b = 0;

        OX = new char[9];

    }

    public int gameLevel(string level, int index, int turncount)
    {
        if (level == "Easy")
        {
            if(turncount>=2)
            {
                if (((OX[0] == 'X' && OX[2] == 'X') && (index == 1)) || ((OX[0] == 'X' && OX[1] == 'X') && (index == 2)) || ((OX[1] == 'X' && OX[2] == 'X') && (index == 0)))
                {
                    index = UnityEngine.Random.Range(0, nottoprow.Length);
                }
                if (((OX[3] == 'X' && OX[4] == 'X') && (index == 5)) || ((OX[4] == 'X' && OX[5] == 'X') && (index == 3)) || ((OX[3] == 'X' && OX[5] == 'X') && (index == 4)))
                {
                    index = UnityEngine.Random.Range(0, notmiddlerow.Length);
                }
                if (((OX[6] == 'X' && OX[7] == 'X') && (index == 8)) || ((OX[7] == 'X' && OX[8] == 'X') && (index == 6)) || ((OX[6] == 'X' && OX[8] == 'X') && (index == 7)))
                {
                    index = UnityEngine.Random.Range(0, notbottomrow.Length);
                }
                if (((OX[0] == 'X' && OX[3] == 'X') && (index == 5)) || ((OX[1] == 'X' && OX[4] == 'X') && (index == 7)) || ((OX[2] == 'X' && OX[5] == 'X') && (index == 8)))
                {
                    index = UnityEngine.Random.Range(0, notfirstcolumn.Length);
                }
                if (((OX[1] == 'X' && OX[4] == 'X') && (index == 7)) || ((OX[1] == 'X' && OX[7] == 'X') && (index == 4)) || ((OX[4] == 'X' && OX[7] == 'X') && (index == 1)))
                {
                    index = UnityEngine.Random.Range(0, notmiddlerow.Length);
                }
                if (((OX[2] == 'X' && OX[5] == 'X') && (index == 8)) || ((OX[5] == 'X' && OX[8] == 'X') && (index == 2)) || ((OX[2] == 'X' && OX[8] == 'X') && (index == 5)))
                {
                    index = UnityEngine.Random.Range(0, notthirdcolumn.Length);
                }
                if (((OX[0] == 'X' && OX[4] == 'X') && (index == 8)) || ((OX[4] == 'X' && OX[8] == 'X') && (index == 0)) || ((OX[0] == 'X' && OX[8] == 'X') && (index == 4)))
                {
                    index = UnityEngine.Random.Range(0, notdiagnol1.Length);
                }
                if (((OX[2] == 'X' && OX[4] == 'X') && (index == 6)) || ((OX[4] == 'X' && OX[6] == 'X') && (index == 8)) || ((OX[2] == 'X' && OX[6] == 'X') && (index == 4)))
                {
                    index = UnityEngine.Random.Range(0, notdiagnol2.Length);
                }
                else
                {
                    index = UnityEngine.Random.Range(0, objectLocations.Length);
                }
            }
            
        }
        if (level == "Medium")
        {
            index = UnityEngine.Random.Range(0, objectLocations.Length);
            
        }
        if (level == "Hard")
        {
            
            if (turncount > 2)
            {
                if (((OX[0] == 'O' && OX[2] == 'O') && (index != 1)))
                {
                    index = 1;
                }
                else if (((OX[1] == 'O' && OX[2] == 'O') && (index != 0)))
                {
                    index = 0;
                }
                else if (((OX[1] == 'O' && OX[0] == 'O') && (index != 2)))
                {
                    index = 2;
                }
                else if (((OX[3] == 'O' && OX[4] == 'O') && (index != 5)))
                {
                    index = 5;
                }
                else if (((OX[3] == 'O' && OX[5] == 'O') && (index != 4)))
                {
                    index = 4;
                }
                else if (((OX[4] == 'O' && OX[5] == 'O') && (index != 3)))
                {
                    index = 3;
                }
                else if (((OX[6] == 'O' && OX[7] == 'O') && (index != 8)))
                {
                    index = 8;
                }
                else if (((OX[0] == 'O' && OX[8] == 'O') && (index != 7)))
                {
                    index = 7;
                }
                else if (((OX[0] == 'O' && OX[4] == 'O') && (index != 8)))
                {
                    index = 8;
                }
                else if (((OX[0] == 'O' && OX[8] == 'O') && (index != 4)))
                {
                    index = 4;
                }
                else if (((OX[4] == 'O' && OX[8] == 'O') && (index != 0)))
                {
                    index = 0;
                }
                else if (((OX[2] == 'O' && OX[4] == 'O') && (index != 6)))
                {
                    index = 6;
                }
                else if (((OX[2] == 'O' && OX[6] == 'O') && (index != 4)))
                {
                    index = 4;
                }
                else if (((OX[6] == 'O' && OX[4] == 'O') && (index != 2)))
                {
                    index = 2;
                }
                else if (((OX[0] == 'O' && OX[3] == 'O') && (index != 6)))
                {
                    index = 6;
                }
                else if (((OX[0] == 'O' && OX[6] == 'O') && (index != 3)))
                {
                    index = 2;
                }
                else if (((OX[3] == 'O' && OX[6] == 'O') && (index != 0)))
                {
                    index = 0;
                }
                else if (((OX[1] == 'O' && OX[4] == 'O') && (index != 7)))
                {
                    index = 7;
                }
                else if (((OX[4] == 'O' && OX[7] == 'O') && (index != 1)))
                {
                    index = 1;
                }
                else if (((OX[1] == 'O' && OX[7] == 'O') && (index != 4)))
                {
                    index = 4;
                }
                else if (((OX[2] == 'O' && OX[5] == 'O') && (index != 8)))
                {
                    index = 8;
                }
                else if (((OX[2] == 'O' && OX[8] == 'O') && (index != 5)))
                {
                    index = 5;
                }
                else if (((OX[5] == 'O' && OX[8] == 'O') && (index != 2)))
                {
                    index = 2;
                }
                else if (((OX[0] == 'X' && OX[2] == 'X') && (index != 1)))
                {
                    index = 1;
                }
                else
                    index = UnityEngine.Random.Range(0, objectLocations.Length);
                


                if (((OX[1] == 'X' && OX[2] == 'X') && (index != 0)))
                {
                    index = 0;
                }
                else if (((OX[1] == 'X' && OX[0] == 'X') && (index != 2)))
                {
                    index = 2;
                }
                else if (((OX[3] == 'X' && OX[4] == 'X') && (index != 5)))
                {
                    index = 5;
                }
                else if (((OX[3] == 'X' && OX[5] == 'X') && (index != 4)))
                {
                    index = 4;
                }
                else if (((OX[4] == 'X' && OX[5] == 'X') && (index != 3)))
                {
                    index = 3;
                }
                else if (((OX[6] == 'X' && OX[7] == 'X') && (index != 8)))
                {
                    index = 8;
                }
                else if (((OX[6] == 'X' && OX[8] == 'X') && (index != 7)))
                {
                    index = 7;
                }
                else if (((OX[0] == 'X' && OX[4] == 'X') && (index != 8)))
                {
                    index = 8;
                }
                else if (((OX[0] == 'X' && OX[8] == 'X') && (index != 4)))
                {
                    index = 4;
                }
                else if (((OX[4] == 'X' && OX[8] == 'X') && (index != 0)))
                {
                    index = 0;
                }
                else if (((OX[2] == 'X' && OX[4] == 'X') && (index != 6)))
                {
                    index = 6;
                }
                else if (((OX[2] == 'X' && OX[6] == 'X') && (index != 4)))
                {
                    index = 4;
                }
                else if (((OX[6] == 'X' && OX[4] == 'X') && (index != 2)))
                {
                    index = 2;
                }
                else if (((OX[0] == 'X' && OX[3] == 'X') && (index != 6)))
                {
                    index = 6;
                }
                else if (((OX[0] == 'X' && OX[6] == 'X') && (index != 3)))
                {
                    index = 2;
                }
                else if (((OX[3] == 'X' && OX[6] == 'X') && (index != 0)))
                {
                    index = 0;
                }
                else if (((OX[1] == 'X' && OX[4] == 'X') && (index != 7)))
                {
                    index = 7;
                }
                else if (((OX[4] == 'X' && OX[7] == 'X') && (index != 1)))
                {
                    index = 1;
                }
                else if (((OX[1] == 'X' && OX[7] == 'X') && (index != 4)))
                {
                    index = 4;
                }
                else if (((OX[2] == 'X' && OX[5] == 'X') && (index != 8)))
                {
                    index = 8;
                }
                else if (((OX[2] == 'X' && OX[8] == 'X') && (index != 5)))
                {
                    index = 5;
                }
                else if (((OX[5] == 'X' && OX[8] == 'X') && (index != 2)))
                {
                    index = 2;
                }


            }
            if (turncount <= 2)
            { index = index = UnityEngine.Random.Range(0, notdiagnol1.Length); }
            else
            {
                index = UnityEngine.Random.Range(0, objectLocations.Length);
            }

        }
        return index;
    }
}