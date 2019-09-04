using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeConstructor : MonoBehaviour
{
    public bool showDebug;

    [SerializeField] private Material mazeMat1;
    [SerializeField] private Material mazeMat2;
    [SerializeField] private Material startMat;
    [SerializeField] private Material treasureMat;

    private MazeDataGenerator dataGenerator;
    public int[,] data
    {
        get; private set;
    }

    
    void Awake()
    {
        dataGenerator = new MazeDataGenerator();
        //Default to walls surrounding a single empty cell
        data = new int[,]
        {
            {1,1,1 },
            {1,0,1 },
            {1,1,1}
        };
    }
    public void GenerateNewMaze(int sizeRows, int sizeCols)
    {
        if(sizeRows % 2 == 0 && sizeCols % 2 == 0)
        {
            Debug.LogError("Odd numbers work better for dungeon size");
        }

        data = dataGenerator.FromDimensions(sizeRows,sizeCols);
    }

    private void OnGUI()
    {
        if(!showDebug)
        {
            return;
        }

        int[,] maze = data;
        int rMax = maze.GetUpperBound(0); //row
        int cMax = maze.GetUpperBound(1); //column

        string msg = "";

        for (int i = rMax; i >=0 ; i--)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if(maze[i,j]==0)
                {
                    msg += "....";
                }
                else
                {
                    msg += "==";
                }
            }
            msg += "\n";
        }
        GUI.Label(new Rect(20, 20, 500, 500), msg);
    }
}
