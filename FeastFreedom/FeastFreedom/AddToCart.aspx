<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddToCart.aspx.cs" Inherits="FeastFreedom.AddToCart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>--%>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using WingtipToys.Logic;

namespace AddingToCart
{
  public partial class AddToCart : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      string rawId = Request.QueryString["MenuItemID"];
      int productId;
      if (!String.IsNullOrEmpty(rawId) && int.TryParse(rawId, out MenuItemID))
      {
        using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
        {
          usersShoppingCart.AddToCart(Convert.ToInt16(rawId));
        }

      }
      else
      {
        Debug.Fail("ERROR : We should never get to AddToCart.aspx without a MenuItemID.");
        throw new Exception("ERROR : It is illegal to load AddToCart.aspx without setting a ProductId.");
      }
      Response.Redirect("ShoppingCart.aspx");
    }
  }
}
