using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Drawing;

public partial class Match : System.Web.UI.Page
{
    private Box box;
    private SudokuTextBox sudokutb;
    private Puzzle puzzle;
    private SudokuTextBox[,] stBox;
    private int[,] solution;
    private int easy = 32; //number of numbers visible for level easy
    private int medium = 30; //number of number visible for level medium
    private int hard = 28; //number of numbers visible for level hard

    string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;


    string tournament_id;
    string playerID = "NULL";
    string isLogged = "NULL";
    int a = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        checkSession(); //checks the session
        DisplayAll(); //displays all values
        string cookieValue = "0";

        HttpCookie cookie = new HttpCookie("hints", cookieValue);

        Response.Cookies.Add(cookie);
        string username;
        if (Request.Cookies["logged"] != null)
        {
            isLogged = Request.Cookies["logged"].Value.ToString();
        }

        if (Request.Cookies["logged_id"] != null)
        {
            playerID = Request.Cookies["logged_id"].Value.ToString();
        }
        if (isLogged != "NULL")
        {
            using (SqlConnection connection = new SqlConnection(DatabaseConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Players where player_id=" + playerID;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            username = reader[1].ToString();
                            hyperlinkRegistration.Text = username;
                        }
                    }
                }
                
            }
            
            hyperlinkRegistration.Enabled = false;
            hyperlinkRegistration.NavigateUrl = "~/Profile.aspx";
            hyperlinkLogin.Text = "Log out";
            hyperlinkLogin.NavigateUrl = "~/SingOut.aspx";
        }
        else
        {
            Response.Redirect("MainPage.aspx", true);
        }

        if (Request.Cookies["logged_id"] != null)
        {
            tournament_id = Request.Cookies["tournament_id"].Value.ToString();
        }
        DateTime myDateTime = DateTime.Now;
        string sqlFormattedDate = myDateTime.ToString("HH:mm:ss");
        HttpCookie cookie2 = new HttpCookie("start_time", sqlFormattedDate);

        Response.Cookies.Add(cookie2);
    }

    protected int[,] createSolution()
    {
        
        
        Random r = new Random(); //random number generator
        int num = r.Next(1, 12);
        switch (num)
        {
            case 1:
                return new int[9, 9] {
                {1,3,2,5,6,7,9,4,8},
                {5,4,6,3,8,9,2,1,7},
                {9,7,8,2,4,1,6,3,5},
                {2,6,4,9,1,8,7,5,3},
                {7,1,5,6,3,2,8,9,4},
                {3,8,9,4,7,5,1,2,6},
                {8,5,7,1,2,3,4,6,9},
                {6,9,1,7,5,4,3,8,2},
                {4,2,3,8,9,6,5,7,1}
                };

            case 2:
                return new int[9, 9] {
                {1,2,3,4,5,6,7,8,9},
                {4,5,6,7,8,9,1,2,3},
                {7,8,9,1,2,3,4,5,6},
                {2,3,4,5,6,7,8,9,1},
                {5,6,7,8,9,1,2,3,4},
                {8,9,1,2,3,4,5,6,7},
                {3,4,5,6,7,8,9,1,2},
                {6,7,8,9,1,2,3,4,5},
                {9,1,2,3,4,5,6,7,8}
                };

            case 3:
                return new int[9, 9] {
                { 1,9,8,5,2,6,3,4,7},
                {7,2,5,3,4,1,6,9,8},
                {3,4,6,9,7,8,2,1,5},
                {9,8,1,2,5,7,4,6,3},
                {5,6,4,1,3,9,8,7,2},
                {2,3,7,6,8,4,1,5,9},
                {4,7,3,8,1,5,9,2,6},
                {8,1,9,7,6,2,5,3,4},
                {6,5,2,4,9,3,7,8,1}
                };

            case 4:
                return new int[9, 9] {
                {4,5,2,3,9,1,8,7,6},
                {3,1,8,6,7,5,2,9,4},
                {6,7,9,4,2,8,3,1,5},
                {8,3,1,5,6,4,7,2,9},
                {2,4,5,9,8,7,1,6,3},
                {9,6,7,2,1,3,5,4,8},
                {7,9,6,8,5,2,4,3,1},
                {1,8,3,7,4,9,6,5,2},
                {5,2,4,1,3,6,9,8,7}
                };

            case 5:
                return new int[9, 9] {
                {2,3,4,6,8,5,1,9,7},
                {9,1,6,2,4,7,8,3,5},
                {8,7,5,9,1,3,6,4,2},
                {1,9,8,5,2,6,4,7,3},
                {4,2,7,8,3,9,5,6,1},
                {6,5,3,1,7,4,2,8,9},
                {7,6,9,4,5,1,3,2,8},
                {3,8,1,7,6,2,9,5,4},
                {5,4,2,3,9,8,7,1,6}
                };

            case 6:
                return new int[9, 9] {
                {9,7,3,2,8,4,5,1,6},
                {1,4,6,3,5,9,7,2,8},
                {2,5,8,6,1,7,9,3,4},
                {4,6,8,7,2,5,1,8,3},
                {8,1,5,9,6,3,2,4,7},
                {7,3,2,8,4,1,6,9,5},
                {3,8,7,1,9,6,4,5,2},
                {6,9,4,5,3,2,8,7,1},
                {5,2,1,4,7,8,3,6,9}
                };

            case 7:
                return new int[9, 9] {
                {6,9,3,8,7,1,2,5,4},
                {4,7,8,2,6,5,1,9,3},
                {1,2,5,3,4,9,6,8,7},
                {3,6,9,7,2,4,8,1,5},
                {2,5,4,6,1,8,3,7,9},
                {7,8,1,5,9,3,4,6,2},
                {9,4,7,1,8,2,5,3,6},
                {8,3,2,9,5,6,7,4,1},
                {5,1,6,4,3,7,9,2,8}
                };

            case 8:
                return new int[9, 9] {
                {6,3,2,5,4,7,1,9,8},
                {8,9,1,6,2,3,7,4,5},
                {4,7,5,8,1,9,3,2,6},
                {3,4,8,7,5,2,6,1,9},
                {5,1,9,3,8,6,2,7,4},
                {7,2,6,1,9,4,8,5,3},
                {1,8,3,9,7,5,4,6,2},
                {2,5,7,4,6,8,9,3,1},
                {9,6,4,2,3,1,5,8,7}
                };

            case 9:
                return new int[9, 9] {
                {8,5,1,6,3,4,7,9,2},
                {9,7,2,1,8,5,4,3,6},
                {4,6,3,7,2,9,8,1,5},
                {7,9,8,4,5,2,3,6,1},
                {1,3,4,9,7,6,2,5,8},
                {6,2,5,8,1,3,9,7,4},
                {2,8,6,5,9,7,1,4,3},
                {3,4,9,2,6,1,5,8,7},
                {5,1,7,3,4,8,6,2,9}
                };

            case 10:
                return new int[9, 9] {
                {4,7,1,6,9,2,5,3,8},
                {8,9,2,4,5,3,1,6,7},
                {3,6,5,1,7,8,2,4,9},
                {7,5,8,9,3,6,4,1,2},
                {6,3,4,8,2,1,9,7,5},
                {2,1,9,7,4,5,3,8,6},
                {9,2,7,3,8,4,6,5,1},
                {1,8,3,5,6,9,7,2,4},
                {5,4,6,2,1,7,8,9,3}
                };

            default:
                return new int[9, 9] {
                {4,7,1,6,9,2,5,3,8},
                {8,9,2,4,5,3,1,6,7},
                {3,6,5,1,7,8,2,4,9},
                {7,5,8,9,3,6,4,1,2},
                {6,3,4,8,2,1,9,7,5},
                {2,1,9,7,4,5,3,8,6},
                {9,2,7,3,8,4,6,5,1},
                {1,8,3,5,6,9,7,2,4},
                {5,4,6,2,1,7,8,9,3}
                };
        }
    } //end create solution

    protected void checkSession()
    {
        if (Session["gameInProgress"] == null || Session["gameInProgress"].Equals(false)) //check if game in progress
        {
            Session["gameInProgress"] = true; //game is now in session
            solution = createSolution(); //create a solution to use
            Session["solution"] = solution; //assign new solution to solution
        }
        else //if the game is in progress copy the current solution
        {
            solution = (int[,])Session["solution"];
        }
    }

    private void DisplayAll()
    {
        stBox = new SudokuTextBox[9, 9]; //setup a new array
        puzzle = new Puzzle(); //create new puzzle object

        //variables for tablerow, tablecell, and table
        TableRow tr;
        TableCell tc;
        Table table = new Table();

        //variables for inner tablerow, tablecell, and table
        Table innerTable;
        TableRow innerRow;
        TableCell innerCell;

        //add table to sudokuPanel on .aspx page
        sudokuPanel.Controls.Add(table);

        for (int k = 0; k < 3; k++) //for loop for 3x3 table of rows
        {
            tr = new TableRow(); //new row
            table.Controls.Add(tr);
            for (int m = 0; m < 3; m++) //for loop for 3x3 table cells
            {
                box = puzzle.getItem(k, m);
                tc = new TableCell();
                tr.Controls.Add(tc);
                innerTable = new Table();
                tc.Controls.Add(innerTable);

                for (int i = 0; i < 3; i++) //for loop for inner rows
                {
                    innerRow = new TableRow(); //new inner row
                    innerTable.Controls.Add(innerRow); //add inner row to table
                    for (int j = 0; j < 3; j++) //for loop inner cells
                    {
                        innerCell = new TableCell(); //create a new cell
                        innerRow.Controls.Add(innerCell); //add cell to row
                        sudokutb = box.getItem(i, j); //sudoku text box object
                        innerCell.Controls.Add(sudokutb.getTextBox());

                        //add to sudokutextbox to array
                        stBox[k + i + (2 * k), m + j + (2 * m)] = sudokutb;

                    } //end inner cell loop
                } //end inner row loop
            } //end outer cell loop
        } //end outer row loop
    }

    //method to create a new random sudoku puzzle
    private void newPuzzle()
    {
        puzzle = new Puzzle();

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                //reset all the textbox attributes
                stBox[i, j].setTextBoxValue(solution[i, j]);
                stBox[i, j].setVisibility(false);
                stBox[i, j].setDefaultColor();
                

            }
        }
    }

    //method to check user input to real solution
    protected void checkPuzzle(object sender, EventArgs e)
    {
        bool isComplete = true;
        int a = 0;

        for (int i = 0; i < 3; i++) //for loop for 3x3 table of rows
        {
            for (int j = 0; j < 3; j++) //for loop for 3x3 table cells
            {
                for (int k = 0; k < 3; k++) //for loop for inner rows
                {
                    for (int m = 0; m < 3; m++) //for loop inner cells
                    {
                        string textBoxVal = stBox[i + k + (i * 2), j + m + (j * 2)].getTextBoxValue(); //grab current textBox value
                        if (textBoxVal != "" && char.IsNumber(textBoxVal.ToCharArray()[0]))//check to make sure not empty and that it is a number
                        {
                            int arg1 = Int32.Parse(textBoxVal); //convert text string to int
                            int arg2 = solution[i + k + (i * 2), j + m + (j * 2)];
                            if (arg1 == arg2 && stBox[i + k + (i * 2), j + m + (j * 2)].getVisibility() == false) //compare and make sure textbox is not a disabled dispalyed box
                            {
                                stBox[i + k + (i * 2), j + m + (j * 2)].setCorrectNumber(); //correct value set correct color
                            }
                            else //if it's a wrong entry
                            {
                                stBox[i + k + (i * 2), j + m + (j * 2)].setWrongNumber(); //wrong value set wrong color
                                isComplete = false;
                                a += 1;

                                if (a > 2) {                   
                                    LblObavijest.Text="3 mistakes are maximum";
                                    sudokuPanel.Enabled = false;

                                    
                                }
                                
                                

                            }
                            //System.Threading.Thread.Sleep(5000);
                            // Response.Redirect("RangLista.aspx");
                        }
                        else //if puzzle is not completely filled in
                        {
                            isComplete = false;
                            if (textBoxVal != "" && !char.IsNumber(textBoxVal.ToCharArray()[0])) //check to see if invalid character
                            {
                                stBox[i + k + (i * 2), j + m + (j * 2)].clearInvalidInput(); //clear the invalid character
                            }
                        }
                    } //end inner cell loop
                } //end inner row loop
            } //end outer cell loop
        } //end outer row loop

        if (isComplete)
        {
            gameComplete(); //GAME IS WON!
        }
    }

    protected void showSolution(object sender, EventArgs e)
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (stBox[row, col].getVisibility() == false) //grab textbox that isn't already shown or has a value
                {
                    int num = solution[row, col]; //grab number
                    stBox[row, col].setTextBoxValue(num); //assign correct number and display
                    stBox[row, col].setVisibility(true); //set visible to show number
                    stBox[row, col].setCorrectNumber(); //show the hint color
                }
            }
        }
        /*ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Congratulations, you won! But by cheating... ')");*/
        LblObavijest.Text = "Congratulations, you won! But by cheating... "; //show user won buy by using cheats*/
    }

    private void gameComplete()
    {
        //winning screen
        string startt = "";
        if (Request.Cookies["start_time"] != null)
        {
            startt = Request.Cookies["start_time"].Value.ToString();
        }
        DateTime endt = DateTime.Now;
        string sqlFormattedDate = endt.ToString("HH:mm:ss");
        DateTime sDate = Convert.ToDateTime(startt);
        DateTime eDate = Convert.ToDateTime(sqlFormattedDate);
        TimeSpan difference = eDate.Subtract(sDate);
        //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Congratulations, you won in  + difference.TotalSeconds.ToString()+ seconds, ')");

        LblObavijest.Text="Congratulations, you won in " + difference.TotalSeconds.ToString()+" seconds,";

        using (SqlConnection connection = new SqlConnection(DatabaseConnectionString))
        {
            connection.Open();

            string query = string.Format("INSERT INTO Matches(player_id,tournament_id,points) VALUES ('{0}','{1}','{2}')",
               playerID, tournament_id, difference.TotalSeconds.ToString());

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }

            /*   string query = string.Format("INSERT INTO Matches(player_id,tournament_id,points) VALUES ('{0}','{1}','{2}')",
                playerID, tournament_id, );
           
                OVO RADI KAD SE IZBACI tournament_id???????????????????????????????'
            }*/
        }
        Response.Redirect("RangLista.aspx", true);

    }

    protected void hint(object sender, EventArgs e) //display a hint number
    {
        Random r = new Random(); //create new random object for display
        int row, col; //new ints to hold random values
        int counter = 0; //counter to make sure we don't enter an infinite loop
        string hints;
        int hintsNum = 0;
        if (Request.Cookies["hints"] != null)
        {
            hints = Request.Cookies["hints"].Value.ToString();
            hintsNum = int.Parse(hints);
        }
        else
        {
            string cookieValue = "0";

            HttpCookie cookie = new HttpCookie("hints", cookieValue);

            Response.Cookies.Add(cookie);
            hintsNum = 0;
        }
        if (hintsNum >= 2)
        {
            LblObavijest.Text = "3 hints maximum";
            Hint.Enabled = false;
        }

        while (true) //itrerate until valid 
        {
            //create random numbers for row and col
            row = r.Next(0, 9);
            col = r.Next(0, 9);
            //a++;
            if (stBox[row, col].getVisibility() == false && stBox[row, col].getTextBoxValue() == "") //grab tb that isn't already shown or has a value
            {

                int num = solution[row, col]; //grab num
                stBox[row, col].setTextBoxValue(num); //assign correct num and display
                stBox[row, col].setVisibility(true); //set visible to show number
                stBox[row, col].setHint(); //show the hint color
                //a++;
                break; //we found a num to show, break out of loop

            }
           // a++;
            counter++; //int counter
            if (counter >= 1000)
                break;

        }
        hintsNum++;
        string cookieValue2 = hintsNum.ToString();

        HttpCookie cookie2 = new HttpCookie("hints", cookieValue2);

        Response.Cookies.Add(cookie2);
    }

    protected void playSymmetrical(object sender, EventArgs e)
    {
        //create a new game
        playHard(sender, e);

        //make it symmetrical
        for (int i = 0; i < 6; i++) //loop through top 6 rows
        {
            for (int j = 0; j < 9; j++) //loop through all 9 cols
            {
                //copy the top half down to bottom half while iterating forwards & backwards
                stBox[8 - i, j].setVisibility(stBox[i, j].getVisibility());
            }
        }
    }

    private void playDifficulty(int difficulty)
    {
        //create a new Sudoku Puzzle
        solution = createSolution(); //grab new solution
        Session["solution"] = solution; //update session variable
        newPuzzle(); //create new puzzle

        int numVisibleLeft = difficulty;
        Random ran = new Random();
        int r; //r holds random number
        bool doBreak = false; //break out and dance
        int numVisPerBox = 0; //curent num of visible textboxes per box
        int maxVisPerBox = 4; //maximum number of visible textboxes per box

        while (numVisibleLeft > 0)
        {
            for (int i = 0; i < 3; i++) //for loop for 3x3 table of rows
            {
                for (int j = 0; j < 3; j++) //for loop for 3x3 table cells
                {
                    numVisPerBox = 0; //reset number visibility because new box
                    for (int k = 0; k < 3; k++) //for loop for inner rows
                    {
                        for (int m = 0; m < 3; m++) //for loop inner cells
                        {
                            r = ran.Next(0, 2);
                            if (r == 1 && stBox[i + k + (i * 2), j + m + (j * 2)].getVisibility() == false && numVisibleLeft > 0) //is visible
                            {
                                numVisPerBox++; //increment num visible for this box
                                numVisibleLeft--; //decrement total numVisible left
                                stBox[i + k + (i * 2), j + m + (j * 2)].setVisibility(true);
                            }
                            if (numVisPerBox == maxVisPerBox || numVisibleLeft == 0)
                            {
                                doBreak = true;
                                break;
                            }
                        } //end inner cell loop

                        if (doBreak)
                        {
                            doBreak = false; //reset bool
                            break; //break out of loop
                        }
                    } //end inner row loop
                } //end outer cell loop
            } //end outer row loop
        }
    }

    protected void playEasy(object sender, EventArgs e)
    {
        playDifficulty(easy);
        Medium.Enabled = false;
        Hard.Enabled = false;
    }

    protected void playMedium(object sender, EventArgs e)
    {
        playDifficulty(medium);
        Easy.Enabled = false;
        Hard.Enabled = false;
    }

    protected void playHard(object sender, EventArgs e)
    {
        playDifficulty(hard);
        Easy.Enabled = false;
        Medium.Enabled = false;
    }

}