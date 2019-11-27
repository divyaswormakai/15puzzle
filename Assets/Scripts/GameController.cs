using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    void Start()
    {
        int mode = PlayerPrefs.GetInt("Mode");
        print(mode);

        int [,]matrix = new int[mode,mode];

        List<int> done = new List<int>();
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                bool complete = true;
                while (complete)
                {
                    int rand = Random.Range(0, mode * mode);
                    if (done.Contains(rand))
                    {
                        continue;
                    }
                    else
                    {
                        matrix[i, j] = rand;
                        done.Add(rand);
                        complete = false;
                    }
                }
            }
        }
        print(mode);
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                print(matrix[i,j]);
            }
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        } 
    }
}
