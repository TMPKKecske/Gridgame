using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using System;

public class GameManager : MonoBehaviour
{
    public string WhichTouch = "";
    public GridController grid_controller = new GridController();
    public int CurrentLevel = 1;
    public bool IsGameWon = false;
    public GameObject StartAnim;
    public GameObject WinAnim;
    public UnityEngine.UI.Text Level;

    private void Start()
    {
        LoadGame();
        Level.text = "Jelenlegi szint: " + CurrentLevel.ToString();
    }

    bool HasStartedAnim = false;
    bool HasrefreshedLevel = false;
    private void Update()
    {
        string SaveDataPath = Application.persistentDataPath + "szintek.txt";

        if (!File.Exists(SaveDataPath))
        {
            GenrateLevelFile(grid_controller, SaveDataPath);
        }

        List<List<int[]>> Levels = new List<List<int[]>>(); //lists of lists hahahahahaha

        using (StreamReader sr = File.OpenText(SaveDataPath))
        {
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                if (!s.Contains("#") && s != "") //you can comment stuff in the level editor with a # symbol
                {
                    string[] Leveldatainstrin = s.Split(',').Select(str => str.Trim()).ToArray();
                    List<int[]> LevelData = new List<int[]>();
                    int Arraysize = 0;
                    int[] AddedCordiante = new int[2];
                    foreach (string cordinate in Leveldatainstrin)
                    {
                        if (Arraysize == 0)
                        {
                            AddedCordiante[0] = Convert.ToInt32(cordinate);
                            Arraysize++;
                        }
                        else
                        {
                            AddedCordiante[1] = Convert.ToInt32(cordinate);
                            LevelData.Add(AddedCordiante);
                            Arraysize--;
                            AddedCordiante = new int[2];
                        }
                    }

                    Levels.Add(LevelData);
                }
            }
        }

        if (grid_controller.GetConqueredPieces().Length == 58 && !HasStartedAnim) //checks if all the squares are filled in
        {
            CurrentLevel++;
            SaveLevel();
            Debug.Log("GAME WON!");
            HasStartedAnim = true;
            StartAnim.SetActive(true);
        }


        if (CurrentLevel > Levels.Count)
        {
            WinAnim.SetActive(true);
        }
        else
        {
            grid_controller.SetDisabledPeaces(Levels[CurrentLevel - 1]);
            HasrefreshedLevel = true;
        }
    }


    void SaveLevel()
    {
        PlayerPrefs.SetInt("Level", CurrentLevel);
        PlayerPrefs.Save();
        Debug.Log("Game data saved!");
    }

    void LoadGame()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            CurrentLevel = PlayerPrefs.GetInt("Level");
            Debug.Log("Game data loaded!");
        }
        else
        {
            Debug.LogError("There is no save data!");
        }
    }

    public void ResetLevel()
    {
        HasStartedAnim = true;
        CurrentLevel = 1;
        SaveLevel();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    List<int[]> CreateLevel(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, int x5, int y5, int x6, int y6)
    {
        return new List<int[]>() { new int[2] { x1, y1 }, new int[2] { x2, y2 }, new int[2] { x3, y3 }, new int[2] { x4, y4 }, new int[2] { x5, y5 }, new int[2] { x6, y6 } };
    }

    void GenrateLevelFile(GridController grid_controller, string SaveDataPath)
    {
        if (grid_controller.GridRows[7, 7] != null)
        {
            List<int[]> Level1 = CreateLevel(6, 2, 6, 4, 6, 5, 0, 4, 1, 4, 2, 4);
            List<int[]> Level2 = CreateLevel(2, 2, 3, 2, 1, 6, 1, 7, 2, 6, 3, 6);
            List<int[]> Level3 = CreateLevel(0, 1, 2, 2, 2, 3, 2, 4, 5, 4, 5, 5);
            List<int[]> Level4 = CreateLevel(2, 3, 2, 4, 2, 5, 1, 7, 6, 4, 7, 4);
            List<int[]> Level5 = CreateLevel(2, 4, 2, 5, 3, 7, 6, 4, 6, 5, 6, 6);
            List<int[]> Level6 = CreateLevel(2, 2, 2, 3, 2, 4, 3, 2, 6, 1, 6, 2);
            List<int[]> Level7 = CreateLevel(1, 1, 2, 1, 3, 3, 4, 0, 4, 3, 5, 3);
            List<int[]> Level8 = CreateLevel(3, 4, 3, 5, 5, 5, 6, 5, 6, 6, 6, 7);
            List<int[]> Level9 = CreateLevel(2, 6, 2, 7, 3, 6, 4, 6, 5, 1, 5, 2);
            List<int[]> Level10 = CreateLevel(0, 6, 1, 6, 3, 3, 3, 4, 3, 5, 3, 6);
            List<int[]> Level11 = CreateLevel(4, 0, 4, 1, 5, 0, 6, 0, 6, 6, 7, 6);
            List<int[]> Level12 = CreateLevel(1, 7, 2, 3, 2, 4, 2, 5, 6, 4, 7, 4);
            List<int[]> Level13 = CreateLevel(0, 4, 1, 4, 2, 2, 2, 4, 3, 1, 3, 2);
            List<int[]> Level14 = CreateLevel(4, 1, 4, 4, 4, 5, 4, 6, 5, 4, 5, 5);
            List<int[]> Level15 = CreateLevel(3, 0, 3, 1, 3, 2, 4, 5, 5, 5, 5, 7);
            List<int[]> Level16 = CreateLevel(0, 1, 0, 2, 2, 3, 4, 0, 5, 0, 6, 0);
            List<int[]> Level17 = CreateLevel(0, 1, 0, 2, 0, 3, 0, 5, 1, 5, 5, 3);
            List<int[]> Level18 = CreateLevel(4, 2, 5, 2, 5, 3, 5, 4, 5, 5, 6, 4);
            List<int[]> Level19 = CreateLevel(1, 7, 3, 6, 4, 6, 5, 6, 6, 3, 7, 3);
            List<int[]> Level20 = CreateLevel(1, 2, 1, 3, 1, 4, 3, 0, 3, 1, 3, 2);
            List<int[]> Level21 = CreateLevel(4, 3, 4, 4, 4, 6, 5, 4, 5, 5, 5, 6);
            List<int[]> Level22 = CreateLevel(2, 5, 3, 3, 3, 5, 3, 6, 4, 3, 5, 3);
            List<int[]> Level23 = CreateLevel(2, 2, 2, 4, 3, 2, 3, 5, 3, 6, 3, 7);
            List<int[]> Level24 = CreateLevel(4, 2, 5, 2, 5, 6, 6, 2, 6, 6, 7, 6);
            List<int[]> Level25 = CreateLevel(0, 2, 1, 4, 2, 4, 4, 3, 4, 4, 4, 5);
            List<int[]> Level26 = CreateLevel(0, 5, 0, 6, 5, 1, 5, 4, 6, 4, 7, 4);
            List<int[]> Level27 = CreateLevel(2, 2, 2, 3, 4, 2, 4, 5, 5, 5, 6, 5);
            List<int[]> Level28 = CreateLevel(4, 3, 5, 0, 5, 1, 5, 2, 5, 3, 6, 0);
            List<int[]> Level29 = CreateLevel(1, 3, 2, 3, 3, 4, 3, 5, 3, 6, 4, 4);
            List<int[]> Level30 = CreateLevel(2, 5, 3, 0, 3, 1, 3, 5, 4, 5, 6, 0);
            List<int[]> Level31 = CreateLevel(0, 3, 1, 3, 1, 7, 2, 7, 3, 7, 7, 5);
            List<int[]> Level32 = CreateLevel(3, 2, 3, 6, 4, 2, 4, 5, 5, 2, 5, 5);
            List<int[]> Level33 = CreateLevel(1, 5, 2, 5, 2, 6, 2, 7, 6, 3, 6, 4);
            List<int[]> Level34 = CreateLevel(1, 6, 2, 6, 3, 0, 3, 3, 3, 6, 4, 0);
            List<int[]> Level35 = CreateLevel(0, 2, 1, 2, 2, 2, 3, 6, 4, 1, 4, 6);
            List<int[]> Level36 = CreateLevel(4, 4, 4, 6, 4, 7, 5, 5, 5, 6, 5, 7);
            List<int[]> Level37 = CreateLevel(1, 3, 4, 2, 5, 2, 6, 2, 6, 3, 6, 4);
            List<int[]> Level38 = CreateLevel(4, 2, 5, 2, 5, 3, 5, 5, 6, 5, 7, 5);
            List<int[]> Level39 = CreateLevel(2, 6, 3, 6, 4, 6, 5, 5, 5, 6, 7, 0);
            List<int[]> Level40 = CreateLevel(0, 1, 1, 1, 1, 2, 5, 0, 5, 1, 5, 2);
            List<int[]> Level41 = CreateLevel(0, 0, 0, 5, 1, 0, 1, 5, 2, 5, 6, 7);
            List<int[]> Level42 = CreateLevel(2, 2, 2, 3, 2, 4, 5, 6, 5, 7, 7, 7);
            List<int[]> Level43 = CreateLevel(2, 3, 3, 2, 4, 2, 4, 4, 5, 4, 6, 4);
            List<int[]> Level44 = CreateLevel(0, 4, 1, 4, 2, 4, 5, 6, 5, 7, 7, 7);
            List<int[]> Level45 = CreateLevel(4, 4, 5, 4, 5, 5, 5, 6, 5, 7, 6, 0);
            //not tested levels yet
            List<int[]> Level46 = CreateLevel(2, 3, 3, 3, 3, 4, 3, 5, 4, 5, 4, 6);
            List<int[]> Level47 = CreateLevel(4, 1, 4, 5, 4, 6, 4, 7, 6, 7, 7, 7);
            List<int[]> Level48 = CreateLevel(1, 2, 1, 3, 2, 4, 2, 7, 3, 7, 6, 4);
            List<int[]> Level49 = CreateLevel(1, 4, 1, 5, 2, 5, 3, 5, 6, 0, 6, 1);
            List<int[]> Level50 = CreateLevel(1, 4, 2, 5, 2, 6, 2, 7, 6, 3, 7, 3);
            List<int[]> Level51 = CreateLevel(5, 0, 3, 6, 3, 7, 4, 7, 5, 7, 6, 7);
            List<int[]> Level52 = CreateLevel(2, 2, 2, 3, 2, 4, 3, 4, 3, 5, 4, 1);
            List<int[]> Level53 = CreateLevel(4, 1, 4, 5, 4, 6, 4, 7, 5, 5, 6, 5);
            List<int[]> Level54 = CreateLevel(2, 7, 4, 2, 4, 3, 4, 4, 6, 7, 7, 7);
            List<int[]> Level55 = CreateLevel(1, 5, 4, 4, 4, 5, 4, 6, 4, 7, 5, 4);
            List<int[]> Level56 = CreateLevel(3, 0, 3, 1, 3, 2, 7, 3, 6, 5, 7, 5);
            List<int[]> Level57 = CreateLevel(3, 1, 3, 5, 3, 6, 3, 7, 2, 7, 1, 7);
            List<int[]> Level58 = CreateLevel(0, 1, 0, 2, 0, 3, 5, 0, 5, 1, 3, 7);
            List<int[]> Level59 = CreateLevel(2, 3, 2, 4, 2, 5, 2, 6, 3, 6, 5, 5);
            List<int[]> Level60 = CreateLevel(0, 4, 1, 4, 2, 6, 5, 3, 6, 3, 7, 3);
            List<int[]> Level61 = CreateLevel(3, 1, 2, 4, 3, 4, 5, 7, 6, 7, 7, 7);
            List<int[]> Level62 = CreateLevel(3, 5, 4, 5, 5, 5, 6, 2, 6, 3, 6, 4);
            List<int[]> Level63 = CreateLevel(5, 1, 5, 2, 4, 4, 4, 5, 4, 6, 4, 7);
            List<int[]> Level64 = CreateLevel(3, 2, 3, 3, 2, 7, 6, 2, 6, 3, 6, 4);
            List<int[]> Level65 = CreateLevel(2, 6, 3, 4, 3, 5, 3, 6, 4, 2, 4, 3);
            List<int[]> Level66 = CreateLevel(5, 0, 5, 1, 4, 5, 5, 5, 6, 5, 7, 3);
            List<int[]> Level67 = CreateLevel(0, 7, 3, 3, 4, 2, 4, 3, 5, 2, 6, 2);
            List<int[]> Level68 = CreateLevel(1, 2, 3, 3, 4, 3, 5, 3, 6, 4, 7, 4);
            List<int[]> Level69 = CreateLevel(2, 6, 3, 6, 4, 5, 6, 3, 6, 4, 6, 5);
            List<int[]> Level70 = CreateLevel(0, 1, 1, 1, 2, 1, 3, 3, 3, 4, 5, 2);
            List<int[]> Level71 = CreateLevel(1, 7, 2, 7, 3, 7, 5, 2, 5, 3, 5, 4);
            List<int[]> Level72 = CreateLevel(2, 2, 2, 3, 3, 4, 4, 0, 4, 4, 5, 4);
            List<int[]> Level73 = CreateLevel(2, 2, 2, 3, 3, 4, 4, 0, 4, 4, 5, 4);
            List<int[]> Level74 = CreateLevel(0, 1, 1, 1, 2, 1, 3, 3, 3, 4, 5, 2);
            List<int[]> Level75 = CreateLevel(1, 2, 3, 3, 4, 3, 5, 3, 6, 4, 7, 4);
            List<int[]> Level76 = CreateLevel(0, 4, 1, 0, 1, 4, 2, 0, 3, 0, 3, 1);
            List<int[]> Level77 = CreateLevel(2, 4, 3, 4, 4, 4, 4, 5, 5, 5, 6, 0);
            List<int[]> Level78 = CreateLevel(2, 4, 4, 3, 5, 3, 5, 4, 5, 5, 6, 3);
            List<int[]> Level79 = CreateLevel(3, 7, 4, 2, 4, 7, 5, 2, 6, 2, 6, 3);
            List<int[]> Level80 = CreateLevel(5, 2, 5, 3, 6, 3, 6, 5, 7, 3, 7, 5);
            List<int[]> Level81 = CreateLevel(0, 7, 4, 4, 5, 2, 5, 4, 6, 2, 7, 2);
            List<int[]> Level82 = CreateLevel(0, 5, 0, 6, 4, 4, 4, 5, 4, 6, 5, 4);
            List<int[]> Level83 = CreateLevel(0, 3, 1, 3, 3, 3, 7, 3, 7, 4, 7, 5);
            List<int[]> Level84 = CreateLevel(1, 7, 2, 7, 2, 6, 2, 7, 4, 7, 5, 7);
            List<int[]> Level85 = CreateLevel(0, 5, 1, 0, 1, 5, 2, 0, 3, 0, 5, 7);
            List<int[]> Level86 = CreateLevel(2, 0, 4, 3, 4, 4, 4, 5, 5, 0, 5, 1);
            List<int[]> Level87 = CreateLevel(0, 2, 0, 3, 2, 6, 3, 4, 3, 5, 3, 6);
            List<int[]> Level88 = CreateLevel(1, 4, 1, 5, 4, 5, 5, 3, 6, 3, 7, 3);
            List<int[]> Level89 = CreateLevel(2, 2, 3, 2, 4, 2, 6, 3, 6, 4, 6, 5);
            List<int[]> Level100 = CreateLevel(4, 0, 4, 6, 5, 0, 6, 0, 7, 0, 7, 1);
            List<int[]> Level01 = CreateLevel(2, 4, 2, 5, 3, 1, 3, 2, 3, 3, 5, 0);
            List<int[]> Level02 = CreateLevel(4, 0, 4, 1, 5, 4, 6, 4, 7, 3, 7, 4);
            List<int[]> Level03 = CreateLevel(3, 3, 3, 4, 3, 5, 4, 2, 6, 3, 6, 4);
            List<int[]> Level104 = CreateLevel(2, 3, 2, 4, 2, 5, 5, 6, 6, 6, 7, 6);
            List<int[]> Level105 = CreateLevel(0, 2, 1, 2, 2, 2, 2, 6, 3, 6, 4, 6);
            List<int[]> Level106 = CreateLevel(2, 3, 4, 2, 5, 2, 6, 2, 6, 5, 7, 5);
            List<int[]> Level107 = CreateLevel(3, 2, 3, 3, 4, 3, 4, 5, 5, 3, 5, 5);
            List<int[]> Level108 = CreateLevel(1, 2, 1, 3, 4, 7, 5, 3, 6, 3, 7, 3);
            List<int[]> Level109 = CreateLevel(2, 5, 2, 6, 2, 7, 4, 5, 6, 4, 7, 4);
            List<int[]> Level110 = CreateLevel(0, 4, 1, 0, 1, 1, 1, 4, 2, 4, 5, 2);
            List<int[]> Level111 = CreateLevel(0, 7, 4, 0, 5, 0, 6, 0, 6, 5, 7, 5);
            List<int[]> Levell12 = CreateLevel(3, 3, 3, 4, 3, 5, 4, 4, 6, 2, 7, 2);
            List<int[]> Level113 = CreateLevel(3, 1, 4, 1, 4, 5, 5, 3, 6, 3, 7, 3);
            List<int[]> Level114 = CreateLevel(2, 4, 2, 6, 2, 7, 3, 4, 4, 3, 4, 4);
            List<int[]> Level115 = CreateLevel(0, 5, 0, 6, 3, 2, 5, 2, 6, 2, 7, 2);


            ArrayList Levels = new ArrayList()
            {
            Level1, Level2, Level3, Level4, Level5, Level6, Level7, Level8, Level9, Level10, Level11,
            Level12,Level13,Level14,Level15,Level16,Level17,Level18,Level19,Level20,Level21,Level22,Level23,Level24,Level25,Level26, Level27,
            Level28,Level29,Level30,Level31,Level32,Level33,Level34,Level35,Level36,Level37,Level38,Level39,Level40,Level41,Level42,Level43, Level44, Level45,
            Level46,Level47,Level48,Level49,Level50,Level51,Level52,Level53,Level54,Level55,Level56,Level57,Level58,Level59,Level60,Level61,Level62,Level63,
            Level64,Level65,Level66,Level67,Level68,Level69,Level70,Level71,Level72,
          Level73 ,
            Level74,
         Level75,
   Level76,
           Level77,
            Level78,
            Level79,
            Level80,
            Level81,
            Level82 ,
            Level83 ,
            Level84,
            Level85 ,
            Level86,
            Level87 ,
            Level88,
            Level89,
            Level100,
            Level01 ,
            Level02,
            Level03 ,
            Level104,
            Level105,
            Level106,
            Level107,
            Level108 ,
            Level109,
            Level110 ,
            Level111,
            Levell12,
            Level113 ,
            Level114,
            Level115,

            };
            using (StreamWriter sw = File.CreateText(SaveDataPath))
            {
                sw.WriteLine("#Ebben a file-ban lehet Új pályákat a játékhoz adni:");
                foreach (List<int[]> level in Levels)
                {
                    string LineWritten = "";
                    int isendline = 0; //probably this needs some refactoring later if I am bored. (mikor érnék rá amúgy he?')
                    foreach (int[] cord in level)
                    {
                        int currentIndex = 0;
                        isendline++;
                        foreach (int number in cord)
                        {
                            if (isendline == 6 && currentIndex == 1) //this way writing comas at the end of level data is avoided
                            {
                                LineWritten = LineWritten + cord[currentIndex].ToString();
                                currentIndex++;
                            }
                            else
                            {
                                LineWritten = LineWritten + cord[currentIndex].ToString() + ",";
                                currentIndex++;
                            }
                        }

                    }
                    sw.WriteLine(LineWritten);
                }
            }

        }
    }
    public void Nextevel() 
    {
        HasStartedAnim = true;
        CurrentLevel++;
        SaveLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}