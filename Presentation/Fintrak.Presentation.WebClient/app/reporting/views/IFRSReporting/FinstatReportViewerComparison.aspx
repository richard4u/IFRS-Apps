<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinstatReportViewerComparison.aspx.cs" Inherits="IFRSReporting.FinstatReportViewerComparison" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            font-size: small;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <table style="width: 100%; background-color: #008000;">
  <%--  <td style="width: 189px"><asp:Label ID="SelectedPath" runat="server" ></asp:Label>   </td>--%>
<%--    <td style="width: 216px" ="right"> </td>--%>
        <tr>

          

<%--            <td>
                <table>
                    <caption class="auto-style1" style="color: #FFFFFF">
                        Company</table>         
          <asp:DropDownList ID="Company" runat="server"  DataTextField ="Company" DataValueField ="CompanyCode" Height="23px" Width="153px" ForeColor="#009933"></asp:DropDownList>
           
            </td>--%>

   
<%--            <td>
                <table>
                    <caption class="auto-style1" style="color: #FFFFFF">
                        Currency</table> 
               <asp:DropDownList ID="Currency" runat="server" DataTextField ="Currency" DataValueField ="CurrencyCode" ForeColor="#009933" Height="16px" style="margin-left: 0px" Width="97px"></asp:DropDownList>
             
            </td> --%>                                        

<%--            <td>
                <table>
                    <caption class="auto-style1" style="color: #FFFFFF">
                        CurrentDate</table> 
                 
                <asp:DropDownList ID="RunDate" runat="server" DataTextField ="Date" DataValueField ="Date" ForeColor="#009933" Height="16px" style="margin-left: 12px" Width="90px"></asp:DropDownList>
             
            </td> --%>
        
<%--             <td>
                  <table>
                    <caption class="auto-style1" style="color: #FFFFFF">
                        PreviousDate</table> 
            <%--     <asp:DropDownList ID="PreviousDate" runat="server" DataTextField ="Date" DataValueField ="Date"></asp:DropDownList>--%>
           <%-- <asp:DropDownList ID="PreviousDate" runat="server" DataTextField ="Date" DataValueField ="Date" ForeColor="#009933" Height="16px" style="margin-left: 12px" Width="90px"></asp:DropDownList>
             
             </td>--%>

         <%--   <td>
            <asp:DropDownList ID="PreviousPreviousDate" runat="server" DataTextField ="Date" DataValueField ="Date"></asp:DropDownList>
             
            </td> --%>
            

            
            <td>  
                  <table>
                    <caption class="auto-style1" style="color: #FFFFFF">
                        ReportType</table>
                 <asp:DropDownList id="ReportType" runat="server" ForeColor="#009933">

                  <asp:ListItem Value="BS"> Balancesheet </asp:ListItem>
                  <asp:ListItem Value="PL"> Income </asp:ListItem>
                
               </asp:DropDownList>
      
           </td> 
          
    
            <td>  
                <table>
                    <caption class="auto-style1" style="color: #FFFFFF">
                        BudgetType</table>
                 <asp:DropDownList id="Budget" runat="server" ForeColor="#009933">

                  <asp:ListItem Value="0"> Stretch Budget </asp:ListItem>
                  <asp:ListItem Value="1"> Board Budget </asp:ListItem>
                
               </asp:DropDownList>
      
           </td> 


            
            <td>  
                <table>
                    <caption class="auto-style1" style="color: #FFFFFF">
                        Adjustment</table>
                 <asp:DropDownList id="Adjustment" runat="server" ForeColor="#009933">

                  <asp:ListItem Value="1"> With Adjustment </asp:ListItem>
                  <asp:ListItem Value="2"> No Adjustment </asp:ListItem>
                
               </asp:DropDownList>
      
           </td> 

               
            <td>
                <asp:Button ID="Button1" runat="server" Text="Display Report"  Skin="Sunset" ForeColor="Black" BackColor="White" Width="125px" Height="37px"  />
            </td>
            </tr>
        
      </table>
      
       
      
      
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
      
       
      
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="1500px" Width="1510px">
        </rsweb:ReportViewer>
    
      
       
      
        <br />
    
    </div>
        
    </form>
</body>
</html>
