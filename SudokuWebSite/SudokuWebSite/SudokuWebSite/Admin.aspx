<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>

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
        .auto-style19 {
            width: 384px;
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
             <asp:HyperLink ID="hyperlinkRegistration" runat="server" NavigateUrl="~/Register.aspx">Registration</asp:HyperLink>
            </th> 
          <th> 
              <asp:HyperLink ID="hyperlinkLogin" runat="server" NavigateUrl="~/SingIn.aspx">Log In</asp:HyperLink>
            </th>
            <th>&nbsp;</th>
            <th class="auto-style23">&nbsp   &nbsp   &nbsp   &nbsp   &nbsp   &nbsp   &nbsp   &nbsp   &nbsp   &nbsp
                <asp:Button class="btnH" ID="btnHrvReg" runat="server" Text="        "/>
            </th>
         </tr> 
       </table>

        <br />


        <table class="table1" style="width: 98%">
          <tr>
            <th class="auto-style15"><asp:HyperLink ID="hyperlinkHome" runat="server" ForeColor="Black" NavigateUrl="~/MainPage.aspx">Home</asp:HyperLink></th>
            <th class="auto-style16"><asp:HyperLink ID="hyperlinkPlay" runat="server" ForeColor="Black" NavigateUrl="~/LetsPlay.aspx">Let&#39;s play!</asp:HyperLink></th> 
            <th><asp:HyperLink ID="hyperlinkTurniriUTijeku" runat="server" ForeColor="Black" NavigateUrl="~/RangLista.aspx">Rang Lista</asp:HyperLink> </th>
          </tr>  
        </table>

        <br /> 

        <table class="table2" style="width: 904px; height: 482px">
           
	    <tr><td class="auto-style19"> <br /> <br /> <br /> <br />
        <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label> &nbsp &nbsp
        <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblEndDate" runat="server" Text="End Date (2019-06-04 12:06)"></asp:Label> &nbsp &nbsp
        <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblCountry" runat="server" Text="Country"></asp:Label> &nbsp &nbsp
        <asp:DropDownList ID="ddlCountry" runat="server">
        </asp:DropDownList>
        <br /> <br />
        <asp:Button class="btn" ID="btnCreate" runat="server" Text="Create" OnClick="btnCreate_Click" />

     </td>
	    <td class="auto-style22"> &nbsp;</td> 
	    </tr>

        <tr>
	    <td class="auto-style19"> &nbsp &nbsp; </td>
	    <td class="auto-style22"> &nbsp;</td> 
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
