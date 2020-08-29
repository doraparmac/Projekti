<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>Sudoku Online Competition</title>
    <meta name="description" content="onlinesudokucompetition.com"/>
    <meta name="keywords" content="sudoku"/>

     <link rel="stylesheet" href="style.css"/>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.0/css/all.css" integrity="sha384-lZN37f5QGtY3VHgisS14W3ExzMWZxybE1SJSEsQp9S+oqd12jhcu+A56Ebc1zFSJ" crossorigin="anonymous">

    <style type="text/css">
        .auto-style1 {
            width: 695px;
            height: 51px;
        }
        .auto-style8 {
            width: 517px;
            height: 51px;
        }
        .auto-style9 {
            width: 410px;
        }
        .auto-style15 {
            width: 286px;
        }
        .auto-style16 {
            width: 321px;
        }
        .auto-style21 {
            width: 931px;
            height: 1108px;
            margin-right: 0px;
        }
        .auto-style22 {
            width: 338px;
        }
        .auto-style23 {
            width: 188px;
        }
        .auto-style24 {
            width: 96%;
        }
        .auto-style25 {
            width: 412px;
        }
        .auto-style26 {
            width: 24px;
        }
        .auto-style27 {
            width: 300px;
        }
        </style>
    
</head>
<body style="width: 62%; height: 943px">
    <form id="form1" runat="server">
    <div class="auto-style21">
        <br />
      <table class="auto-style24">
        <tr>
         <th class="auto-style9"><img src="gornjidio.png" class="auto-style25"></img></th>
         <th>
             <i class="fa fa-user icon"></i> &nbsp
             <asp:HyperLink ID="hyperlinkRegistration" runat="server" NavigateUrl="~/Registration.aspx">Registration</asp:HyperLink>
            </th> 
          <th> 
              <asp:HyperLink ID="hyperlinkLogin" runat="server" NavigateUrl="~/LogInspx.aspx">Log In</asp:HyperLink>
            </th>
            <th>&nbsp;</th>
            <th class="auto-style23"> &nbsp   &nbsp   &nbsp   &nbsp   &nbsp   &nbsp   &nbsp   &nbsp   &nbsp   &nbsp      
                <asp:Button class="btnH" ID="Button1" runat="server"  Text="        "/> 
            </th>
         </tr>  
       </table>

        <br />


        <table class="table1" style="width: 98%">
          <tr>
            <th class="auto-style15"><asp:HyperLink ID="hyperlink3" runat="server" ForeColor="Black" NavigateUrl="~/MainPage.aspx">Home</asp:HyperLink></th>
            <th class="auto-style16"><asp:HyperLink ID="hyperlink4" runat="server" ForeColor="Black" NavigateUrl="~/LetsPlay.aspx">Let&#39;s play!</asp:HyperLink></th> 
            <th><asp:HyperLink ID="hyperlink5" runat="server" ForeColor="Black" NavigateUrl="~/RangLista.aspx">Ranking List</asp:HyperLink> </th>
          </tr>  
        </table>

        <br /> 

        <table class="table2" style="width: 904px; height: 482px">
	    <tr><td class="auto-style26"> </td>
	    <td class="auto-style27"> <br /> 
            &nbsp &nbsp &nbsp
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" Width="326px">
                <Columns>
                    <asp:BoundField DataField="player_id" HeaderText="player_id" SortExpression="player_id" Visible="False" />
                    <asp:BoundField DataField="username" HeaderText="Username" SortExpression="username" >
                    <HeaderStyle BorderStyle="Groove" HorizontalAlign="Center" BorderWidth="2px" />
                    <ItemStyle BorderStyle="Groove" HorizontalAlign="Center" CssClass="grid" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Expr1" HeaderText="Points" ReadOnly="True" SortExpression="Expr1" >
                    <ControlStyle BorderStyle="None" CssClass="grid" />
                    <HeaderStyle BorderStyle="Groove" HorizontalAlign="Center" BorderWidth="2px" />
                    <ItemStyle BorderStyle="Groove" HorizontalAlign="Center" CssClass="grid" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            &nbsp;<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="DataSetTableAdapters.MatchesTableAdapter">
                <SelectParameters>
                    <asp:QueryStringParameter Name="tournament" QueryStringField="tournament" Type="Int64" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <br /> <br />
            <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/RangLista.aspx">Back to the tournaments</asp:HyperLink>
       <td class="auto-style22"> &nbsp;</td> 
	    </tr>
            
        <tr>
	    <td class="auto-style26"> &nbsp &nbsp; </td>
	    <td class="auto-style27"> &nbsp;</td> 
	    </tr>

        </table>

        <br />  

        <table>
        <tr>
            <td class="auto-style8"> <i class="fas fa-copyright icon"></i> 2019 ONLINE SUDOKU COMPETITION </td>  
            <td class="auto-style1"><i class="far fa-address-card icon"></i> &nbsp dora.parmac@gmail.com</td>
        </tr>
        </table>
             </div>
    </form>
</body>
</html>