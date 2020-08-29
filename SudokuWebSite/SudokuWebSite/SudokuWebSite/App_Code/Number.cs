using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// </summary>
public class Number
{
    private int num; //private class variables

    public Number()
    {
        num = 0; //initiate value to 0
    }

    public int getNumber()
    {
        return num; //return the number 1 through 9
    }

    public void setNumber(int number)
    {
        num = number; //update number valule
    }
}
