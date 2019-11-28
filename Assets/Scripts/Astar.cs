using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    //Use the manhattan distance calculation as the heuristic function value
    int[,] goalState;
    fiveBox fiveBoxScript;
    int mode;

    private void Start()
    {
        fiveBoxScript = FindObjectOfType<fiveBox>();
    }
    public void SetGoals(int size)
    {
        mode = size;
        goalState = new int[size, size];
        int counter = 1;
        for(int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if(i==size && j == size)
                {
                    goalState[i, j] = 0;
                    continue;
                }
                goalState[i, j] = counter;
            }
        }
    }

    public void NextStep()
    {
        int[,] matrix = fiveBoxScript.GetMatrix();
        int mainActiveX = fiveBoxScript.GetActiveX();
        int mainActiveY = fiveBoxScript.GetActiveY();
        bool[] possibleSteps = fiveBoxScript.GetPossibleStepsArray();
        string temp = "";
        for(int i = 0; i < possibleSteps.Length; i++)
        {
            temp += possibleSteps[i].ToString()+", ";
        }
        print(temp);

        //UDLR operation
        int minVal = 50;            //random value
        int changeValue = 6;        //random value

        for(int i = 0; i < 4; i++)
        {
            if (possibleSteps[i])
            {
                int[,] tempMatrix = new int[mode,mode];   //So that only new matrix is used
                for (int p = 0; p < mode; p++)
                {
                    for (int q = 0; q < mode; q++)
                    {
                        tempMatrix[p,q] = matrix[p, q];
                    }
                }

                int activeX = mainActiveX;
                int activeY = mainActiveY;
                int val = tempMatrix[activeX, activeY];
                print(activeX + "," + activeY+ "="+val);

                if (i == 0)         //Up
                {
                    tempMatrix[activeX, activeY] = tempMatrix[activeX - 1, activeY];
                    tempMatrix[activeX - 1, activeY] = val;
                    print("up");
                }
                else if (i == 1)    //Down
                {
                    tempMatrix[activeX, activeY] = tempMatrix[activeX + 1, activeY];
                    tempMatrix[activeX + 1, activeY] = val;
                    print("down");
                }
                else if (i == 2)    //Left
                {
                    tempMatrix[activeX, activeY] = tempMatrix[activeX, activeY - 1];
                    tempMatrix[activeX, activeY - 1] = val;
                    print("left");
                }
                else                //Right
                {
                    tempMatrix[activeX, activeY] = tempMatrix[activeX, activeY + 1];
                    tempMatrix[activeX, activeY + 1] = val;
                    print("right");
                }

                int manhattan = CalculateManhattan(tempMatrix);
                if (manhattan <= minVal)
                {
                    minVal = manhattan;
                    changeValue = i;
                }
            }
        }
        switch (changeValue)
        {
            case 0:     //UP
                {
                    fiveBoxScript.MoveUp();
                    break;
                }
            case 1:
                {
                    fiveBoxScript.MoveDown();
                    break;
                }
            case 2:
                {
                    fiveBoxScript.MoveLeft();
                    break;
                }
            case 3:
                {
                    fiveBoxScript.MoveRight();
                    break;
                }
            default:
                {
                    break;
                }

        }
    }

    int CalculateManhattan(int[,] mat)
    {
        int currVal;
        int totalManhattan =0;
        for(int i = 0; i < mode; i++)
        {
            for(int j = 0; j < mode; j++)
            {
                currVal = mat[i, j];
                if (currVal != 0)
                {
                    int[] goalValues = FindGoalPosition(currVal);
                    totalManhattan += System.Math.Abs(goalValues[0] - i) + System.Math.Abs(goalValues[1] - j);
                }             
            }
        }
        return totalManhattan;
    }

    int[] FindGoalPosition(int val)
    {
        int[] pos = new int[2];
        for (int i = 0; i < mode; i++)
        {
            for (int j = 0; j < mode; j++)
            {
                if(goalState[i,j] == val)
                {
                    pos[0] = i;
                    pos[1] = j;
                }
            }
        }
        return pos;
    }
}
