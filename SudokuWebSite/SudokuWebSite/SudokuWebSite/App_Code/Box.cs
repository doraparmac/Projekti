using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// </summary>
public class Box : BaseBox
{
    private SudokuTextBox[,] sBox;
    public Box()
    {
        sBox = new SudokuTextBox[3, 3]; //intiate new sudoku textbox darray
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                sBox[i, j] = new SudokuTextBox();
            }
        }
    }

    public new SudokuTextBox getItem(int row, int col)
    {
        return sBox[row, col]; //return the sudoku text box at row col
    }

    public void setItem(SudokuTextBox tb, int row, int col)
    {
        sBox[row, col] = tb; //set new sudokuTextbox
    }

    public override string getItemValue(int row, int col)
    {
        return sBox[row, col].ToString(); //return the sudokuTextBox at row col
    }
}