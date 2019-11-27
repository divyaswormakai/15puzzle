using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class fiveBox : MonoBehaviour
{
    public TextMeshProUGUI box;
    public Button up, down,left, right;
    int[,] matrix;
    int activeX, activeY;
    //X is the row  and Y is the column
    void Start()
    {
        int mode = PlayerPrefs.GetInt("Mode");
        matrix = new int[mode, mode];

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
                if(matrix[i,j] == 0)
                {
                    activeX = i;
                    activeY = j;
                }
            }
        }

        SetString();
    }

    void SetString()
    {
        string temp = "";
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                temp += matrix[i, j].ToString() + "\t";
            }
            temp += "\n";
        }
        box.SetText(temp);

        GetPossibleSteps();
    }

    void GetPossibleSteps()
    {
        if (CheckCorrect())
        {
            print("Correct");
        }
        left.gameObject.SetActive(true);
        right.gameObject.SetActive(true);
        up.gameObject.SetActive(true);
        down.gameObject.SetActive(true);
        //activeX for the vertical and  activeY for the horizontal
        if (activeX - 1 < 0)
        {
            up.gameObject.SetActive(false);
        }
        if (activeX + 1 > matrix.GetLength(0))
        {
            down.gameObject.SetActive(false);
        }
        if (activeY - 1 < 0)
        {
            left.gameObject.SetActive(false);
        }
        if (activeY + 1 > matrix.GetLength(0))
        {
            right.gameObject.SetActive(false);
        }
        print(activeX + "," + activeY + "=" + matrix[activeX,activeY]);
    }

    bool CheckCorrect()
    {
        int counter = 1;
        for(int i = 0; i < matrix.GetLength(0); i++)
        {
            for(int j = 0; j < matrix.GetLength(1); j++)
            {
                if(matrix[i,j] == counter)
                {
                    counter += 1;
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void MoveUp()
    {
        int temp = matrix[activeX, activeY];
        matrix[activeX, activeY] = matrix[--activeX, activeY];
        matrix[activeX, activeY] = temp;

        SetString();
    }
    public void ModeDown()
    {
        int temp = matrix[activeX, activeY];
        matrix[activeX, activeY] = matrix[++activeX, activeY];
        matrix[activeX, activeY] = temp;

        SetString();
    }
    public void MoveLeft()
    {
        int temp = matrix[activeX, activeY];
        matrix[activeX, activeY] = matrix[activeX, --activeY];
        matrix[activeX, activeY] = temp;

        SetString();

    }
    public void MoveRight()
    {
        int temp = matrix[activeX, activeY];
        matrix[activeX, activeY] = matrix[activeX, ++activeY];
        matrix[activeX, activeY] = temp;

        SetString();
    }

    
}
