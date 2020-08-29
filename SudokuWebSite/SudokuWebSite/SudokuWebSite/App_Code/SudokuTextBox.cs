using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// </summary>
public class SudokuTextBox
{
    //variables
    private System.Web.UI.WebControls.TextBox textBox;
    private bool visible;
    private Number number;

    //constructor
    public SudokuTextBox()
    {
        //create new textbox
        textBox = new System.Web.UI.WebControls.TextBox();
        textBox.CssClass = "sudoku";

        //properties of textbox
        textBox.MaxLength = 1;
        textBox.Width = System.Web.UI.WebControls.Unit.Pixel(35);
        textBox.Height = System.Web.UI.WebControls.Unit.Pixel(35);
        textBox.Font.Size = System.Web.UI.WebControls.FontUnit.Point(18);
        textBox.Font.Name = "Impact";

        number = new Number(); //create new number for the textbox
    }

    public System.Web.UI.WebControls.TextBox getTextBox()
    {
        return textBox; //return the textBox
    }

    public string getTextBoxValue()
    {
        return textBox.Text.ToString(); //return value as a string
    }

    public Number getNumber()
    {
        return number; //return the number in the textBox
    }

    public void setTextBoxValue(int num)
    {
        number.setNumber(num); //set the number
    }

    public bool getVisibility()
    {
        return visible; //return wether visible or hidden
    }

    public void setVisibility(bool vis)
    {
        visible = vis; //update isVisible
        if (visible) //num is visible so show num value and disable text
        {
            textBox.Text = number.getNumber().ToString(); //load number into textbox
            textBox.Enabled = false; //disable textbox
        }
        else //visibility false so display empty textBox and enable it
        {
            textBox.Text = ""; //empty textbox
            textBox.Enabled = true; //set enabled
        }
    }

    public void setWrongNumber()
    {
        textBox.ForeColor = System.Drawing.ColorTranslator.FromHtml("#930000"); //set color to red, invalid
    }

    public void setCorrectNumber()
    {
        textBox.ForeColor = System.Drawing.ColorTranslator.FromHtml("#014b00"); //set color to green, valid
        textBox.Enabled = false; //disable textbox because correct number
    }

    public void setHint()
    {
        textBox.ForeColor = System.Drawing.ColorTranslator.FromHtml("#93004a"); //show turquoise
    }

    public void setDefaultColor()
    {
        textBox.ForeColor = System.Drawing.ColorTranslator.FromHtml("#00264b"); //default off white color
    }

    public void clearInvalidInput()
    {
        textBox.Text = "";
    }
}