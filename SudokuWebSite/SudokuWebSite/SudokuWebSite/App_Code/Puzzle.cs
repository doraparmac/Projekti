using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// </summary>
public class Puzzle : Box
{
    private Box[,] boxArray; //sudoku boxes
    public Puzzle()
    {
        boxArray = new Box[3, 3]; //initiation of boxArray
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                boxArray[i, j] = new Box();
            }
        }
    }

    public new Box getItem(int row, int col)
    {
        return boxArray[row, col]; //return item at boxArray[row, col]
    }

    public void setItem(Box box, int row, int col)
    {
        boxArray[row, col] = box; //set boxArray[row, col] equal to user input
    }

    public override string getItemValue(int row, int col) //public override string getItemValue(int row, int col)
    {
        return boxArray[row, col].ToString(); //return the string value
    }

}