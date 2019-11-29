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
    public int activeX, activeY;
    public bool[] possibleSteps;
    int mode;
    //X is the row  and Y is the column
    void Start()
    {
        possibleSteps = new bool[4];
        mode = PlayerPrefs.GetInt("Mode");
        mode = 3;
        matrix = new int[mode, mode];

        GeneratePuzzle();
        while (true)
        {
            int invCount = GetInvCount();
            print("In loop");
            if (invCount % 2 == 0)
            {
                break;
            }
            GeneratePuzzle();
        }
  
/*
        matrix[0, 0] = 1;
        matrix[0, 1] = 2;
        matrix[0, 2] = 3;
        matrix[1, 0] = 0;
        matrix[1, 1] = 4;
        matrix[1, 2] = 6;
        matrix[2, 0] = 7;
        matrix[2, 1] = 5;
        matrix[2, 2] = 8;
        activeX = 1;
        activeY = 0;*/
        SetString();

        //For the A* algorithm
        FindObjectOfType<Astar>().SetGoals(mode);
    }

    void GeneratePuzzle()
    {
        List<int> done = new List<int>();
        for (int i = 0; i < mode; i++)
        {
            for (int j = 0; j < mode; j++)
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
                if (matrix[i, j] == 0)
                {
                    activeX = i;
                    activeY = j;
                }
            }
        }
    }

    int GetInvCount()
    {
        int inv_count = 0;
        for (int i = 0; i < mode - 1; i++)
            for (int j = i + 1; j < mode; j++)

                // Value 0 is used for empty space 
                if (matrix[j, i] > 0 && matrix[j, i] > 0 && matrix[j, i] > matrix[i, j])
                    inv_count++;
        return inv_count;
    }

    public int[,] GetMatrix()
    {
        return matrix;
    }

    public int GetActiveX()
    {
        return activeX;
    }

    public int GetActiveY()
    {
        return activeY;
    }

    public bool[] GetPossibleStepsArray()
    {
        GetPossibleSteps();
        return possibleSteps;
    }

    void SetString()
    {
        string temp = "";
        for (int i = 0; i < mode; i++)
        {
            for (int j = 0; j < mode; j++)
            {
                temp += matrix[i, j].ToString() + "\t";
            }
            temp += "\n";
        }
        box.SetText(temp);

        GetPossibleSteps();

        int invCount = GetInvCount();
        if (invCount % 2 != 0)
        {
            print("----------Unsolvable now-----------");
        }
        else
        {
            print("------Can be solved----------");
        }

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
        for(int i = 0; i < 4; i++)
        {
            possibleSteps[i] = true;
        }
        //activeX for the vertical and  activeY for the horizontal
        if (activeX - 1 < 0)
        {
            up.gameObject.SetActive(false);
            possibleSteps[0] = false;
            
        }
        if (activeX + 1 >= mode)
        {
            down.gameObject.SetActive(false);
            possibleSteps[1] = false;
        }
        if (activeY - 1 < 0)
        {
            left.gameObject.SetActive(false);
            possibleSteps[2] = false;
        }
        if (activeY + 1 >= mode)
        {
            right.gameObject.SetActive(false);
            possibleSteps[3] = false;
        }
    }

    bool CheckCorrect()
    {
        int counter = 1;
        for(int i = 0; i < mode; i++)
        {
            for(int j = 0; j < mode; j++)
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
    public void MoveDown()
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
