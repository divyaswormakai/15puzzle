using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    //Use the manhattan distance calculation as the heuristic function value
    int[,] goalState;
    fiveBox fiveBoxScript;
    int mode;

    int previousStep;
    
    //Track only the immediate parent somehow
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
                if(i==size-1 && j == size-1)
                {
                    goalState[i, j] = 0;
                    continue;
                }
                goalState[i, j] = counter;
                counter += 1;
            }
        }
    }

    public void NextStep()
    {
        int[,] matrix = fiveBoxScript.GetMatrix();
        int mainActiveX = fiveBoxScript.GetActiveX();
        int mainActiveY = fiveBoxScript.GetActiveY();
        bool[] possibleSteps = fiveBoxScript.GetPossibleStepsArray();

        //UDLR operation
        int minVal = 50;            //random value
        int changeValue = 4;
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
                //print(activeX + "," + activeY+ "="+val);

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
                print("Manhattan = " + manhattan);
                if (manhattan <= minVal)
                {
                    minVal = manhattan;
                    changeValue = i;
                }
            }
        }
        
        
        if (changeValue == 0)
        {
            fiveBoxScript.MoveUp();
        }
        else if (changeValue == 1)
        {
            fiveBoxScript.MoveDown();
        }
        else if (changeValue == 2)
        {
            fiveBoxScript.MoveLeft();
        }
        else if (changeValue == 3)
        {
            fiveBoxScript.MoveRight();
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
                    int tempManhattan = System.Math.Abs(goalValues[0] - i) + System.Math.Abs(goalValues[1] - j);
                    totalManhattan += tempManhattan;
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
