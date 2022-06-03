using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimStarter : MonoBehaviour
{
    public GameManager gamemanager = new GameManager();
    public void ReloadAnim() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
