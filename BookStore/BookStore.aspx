<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookStore.aspx.cs" Inherits="BookStore" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Book Store</title>
    <link href="App_Themes/SiteStyles.css" rel="stylesheet" />
</head>
<body>
    <h1>Algonquin College Online Book Store</h1>

    <asp:Panel runat="server" ID="pnlBookSelectionView">
        <form id="form1" runat="server">
            <div class="leftSidepPan" >
                    <asp:DropDownList ID="BookList" runat="server" CssClass="dropdown" AutoPostBack="True" OnSelectedIndexChanged="BookList_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvTransferer"
                                                            runat="server"
                                                            ErrorMessage="Book Required"
                                                            Display="Dynamic"
                                                            ControlToValidate="BookList"
                                                            InitialValue="-1"
                                                            CssClass="error"/>
                <fieldset>
                    <legend>Description</legend>
                   
                    <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label>
                   
                </fieldset>
            </div>
            <div class="rightSidePan">
                <div class="priceAndQuantityPan">
                    Buy today to get free delivery to your classroom!<br /><br />
                    <span class="distinct">Price: </span>
                    <asp:Label ID="lblPrice" runat="server" Text=""></asp:Label>
                    <br />
                    <br />
                    <span class="distinct">Quantity: </span>
                    <asp:TextBox ID="txtQauntity" runat="server" CssClass="input"></asp:TextBox>
                    <br /> 
                                                    <asp:RequiredFieldValidator ID="rfvQauntity"
                                                            runat="server"
                                                            ErrorMessage="Qauntity required"
                                                            Display="Dynamic"
                                                            ControlToValidate="txtQauntity"
                                                            CssClass="error"/>
                                                    <br />
                                                    <asp:RangeValidator ID="rngvQauntity"
                                                    runat="server"
                                                    ErrorMessage="Invalid Qauntity"
                                                    Display="Dynamic"
                                                    ControlToValidate="txtQauntity"
                                                    CssClass="error"
                                                        MinimumValue="1"
                                                        MaximumValue="10"
                                                        Type="Integer"/>
                    <br />
                    <hr />
                    <br />
                    <div class="center">
                        <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CssClass="button" OnClick="btnAddToCart_Click" />
                    </div>
                </div>
                <asp:Button ID="btnViewCart" runat="server" Text="View Cart" CssClass="ViewCartButton" OnClick="btnViewCart_Click" CausesValidation="False"/>
            </div>
        </form>
    </asp:Panel>
    <asp:Panel ID="pnlShoppingCartView" runat="server" Visible="false">
        <p>Please review your shopping cart below.</p>
        <asp:Table ID="cartTable" runat="server" CssClass="table">
    <asp:TableHeaderRow>
        <asp:TableHeaderCell>Title</asp:TableHeaderCell>
        <asp:TableHeaderCell>Qauntity</asp:TableHeaderCell>
        <asp:TableHeaderCell>Subtotal</asp:TableHeaderCell>
    </asp:TableHeaderRow>
</asp:Table>    
    </asp:Panel>
</body>
</html>

