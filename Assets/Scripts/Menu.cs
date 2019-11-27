using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void EasyMode()
    {
        PlayerPrefs.SetInt("Mode", 3);
        SceneManager.LoadScene("MainGame");
    }
    public void MediumMode()
    {
        PlayerPrefs.SetInt("Mode", 4);
        SceneManager.LoadScene("MainGame");
    }
    public void HardMode()
    {
        PlayerPrefs.SetInt("Mode", 5);
        SceneManager.LoadScene("MainGame");
    }
}
