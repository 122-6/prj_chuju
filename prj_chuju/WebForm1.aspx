<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="prj_chuju.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 2171px;
            height: 1221px;
        }
    </style>
    <link href="style/members.css" rel="stylesheet" />
</head>


<body>


    <form id="form1" runat="server">
        <div>
            <img class="page_top" src="../images/Members/頁首.png" <%--style="background-repeat: no-repeat; background-size: cover; background-attachment: fixed; width: 1440px; height: 128px; left: 0px; top: 0px"--%> />
        </div>
        <div>
            <img class="BACK" src="../images/Members/pexels-linkedin-sales-navigator-1251844.jpg" style="background-repeat: no-repeat; background-size: cover; background-attachment: fixed; width: 1441px; height: 961px; left: -1px; top: 128px" />
        </div>
        <div>
            <asp:Button ID="Button1" runat="server" class="btn1" OnClick="Button1_Click" Text="會員資料" Height="55px" Width="181px" Font-Names="Noto Sans CJK TC;" Font-Size="18px" ForeColor="Black" />
            <asp:Button ID="Button2" runat="server" class="btn2" OnClick="Button2_Click" Text="我的預約" Height="55px" Width="181px" Font-Names="Noto Sans CJK TC;" Font-Size="18px" ForeColor="Black" />
            <asp:Button ID="Button3" runat="server" class="btn3" OnClick="Button3_Click" Text="我的收藏" Height="55px" Width="181px" Font-Names="Noto Sans CJK TC;" Font-Size="18px" ForeColor="Black" />
            <asp:Button ID="Button4" runat="server" class="btn4" OnClick="Button4_Click" Text="瀏覽歷史" Height="55px" Width="181px" Font-Names="Noto Sans CJK TC;" Font-Size="18px" ForeColor="Black" />
            <asp:Button ID="Button5" runat="server" class="btn5" OnClick="Button5_Click" Text="我的優惠券" Height="55px" Width="181px" Font-Names="Noto Sans CJK TC;" Font-Size="18px" ForeColor="Black" />
            <asp:Button ID="Button6" runat="server" class="btn6" OnClick="Button6_Click" Text="通知中心" Height="55px" Width="181px" Font-Names="Noto Sans CJK TC;" Font-Size="18px" ForeColor="Black" />
        </div>


        <asp:Panel class="content0" ID="Panel1" runat="server" BorderWidth="3px" BorderStyle="Double" Height="749px" Width="1078px">
            <meta charset="utf-8" />
            <div>
                <img class="auto-style1" style="width: 100%; height: 100%" src="images/Members/會員資料.png" />
            </div>
        </asp:Panel>
        <asp:Panel class="content0" ID="Panel2" runat="server" BorderWidth="3px" BorderStyle="Double" Height="749px" Width="1078px">
            <meta charset="utf-8" />
            <div>
                <img class="auto-style1" style="width: 100%; height: 100%" src="images/Members/我的預約.png" />
            </div>
        </asp:Panel>

        <asp:Panel class="content0" ID="Panel3" runat="server" BorderWidth="3px" BorderStyle="Double" Height="749px" Width="1078px">
            <meta charset="utf-8" />
            <div>
                <img class="auto-style1" style="width: 100%; height: 100%" src="images/Members/我的收藏.png" />
            </div>
        </asp:Panel>
        <asp:Panel class="content0" ID="Panel4" runat="server" BorderWidth="3px" BorderStyle="Double" Height="749px" Width="1078px">
            <meta charset="utf-8" />
            <div>
                <img class="auto-style1" style="width: 100%; height: 100%" src="images/Members/瀏覽歷史.png" height="100%" width="100%" />
            </div>
        </asp:Panel>
        <asp:Panel class="content0" ID="Panel5" runat="server" BorderWidth="3px" BorderStyle="Double" Height="749px" Width="1078px">
            <meta charset="utf-8" />
            <div>
                <img class="auto-style1" style="width: 100%; height: 100%" src="images/Members/我的優惠券.png" height="100%" width="100%" />
            </div>
        </asp:Panel>
        <asp:Panel class="content0" ID="Panel6" runat="server" BorderWidth="3px" BorderStyle="Double" Height="749px" Width="1078px">
            <meta charset="utf-8" />
            <div>
                <img class="auto-style1" style="width: 100%; height: 100%" src="images/Members/通知中心.png" height="100%" width="100%" />
            </div>
        </asp:Panel>
        <div>
            <img class="page_buttom" src="../images/Members/頁尾.png"
        </div>
    </form>
</body>

</html>
