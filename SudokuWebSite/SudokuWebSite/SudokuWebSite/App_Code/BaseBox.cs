using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BaseBox
/// </summary>
public abstract class BaseBox
{
    public BaseBox()
    {
        // Nothing to do here
    }

    public object getItem(int row, int col)
    {
        return new Object(); //abstracted return of object
    }

    public void setItem(Object obj, int row, int col)
    {
        //no code
    }

    public virtual string getItemValue(int row, int col)
    {
        return ""; //return empty string
    }

}