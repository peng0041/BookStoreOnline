using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class BookStore : System.Web.UI.Page
{
    protected ShoppingCart cart = ShoppingCart.RetrieveShoppingCart();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cart.EmptyShoppingCart();

            //get all books in the catalog.
            ArrayList books = BookCatalogDataAccess.GetAllBooks();

            BookList.DataSource = books;
            BookList.DataTextField = "Title";
            BookList.DataValueField = "Id";
            BookList.DataBind();
            BookList.Items.Insert(0, new ListItem("Select a Book ...", "-1"));
        }
        btnViewCart.Text = "View Cart (" + cart.NumOfItems + " items)";
    }

    protected void btnAddToCart_Click(object sender, EventArgs e)
    {
        Validate();
        if (Page.IsValid)
        {
            Book book = BookCatalogDataAccess.GetBookById(BookList.SelectedValue);
            int quantity = Int16.Parse(txtQauntity.Text);

            BookOrder order = new BookOrder(book, quantity);
            cart.AddBookOrder(order);
            BookList.Items.RemoveAt(BookList.SelectedIndex);
            string amount = quantity > 1 ? "copies" : "copy";
            lblDescription.Text = quantity + " " + amount + " of " + book.Title + " have been added to your cart";
            btnViewCart.Text = "View Cart (" + cart.NumOfItems + " items)";
            BookList.ClearSelection();
            txtQauntity.Text = "";
        }
    }
    protected void BookList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (BookList.SelectedValue != "-1")
        {
            Book book = BookCatalogDataAccess.GetBookById(BookList.SelectedValue);
            lblPrice.Text = String.Format("{0:C}", book.Price);
            lblDescription.Text = book.Description;
        }
        else
        {
            lblPrice.Text = "";
            lblDescription.Text = "";
        }
    }
    protected void btnViewCart_Click(object sender, EventArgs e)
    {
        var orders = cart.BookOrders;
        double total = 0;

        pnlBookSelectionView.Visible = false;
        pnlShoppingCartView.Visible = true;
        if (orders.Count > 0)
        {
            foreach (BookOrder order in orders)
            {

                var row = new TableRow();
                var titleCell = new TableCell {Text = order.Book.Title};
                var qaunCell = new TableCell
                {
                    Text = order.NumOfCopies.ToString()

                };

                var subCell = new TableCell
                {
                    Text = String.Format("{0:C}", order.Book.Price*order.NumOfCopies)
                };
                total += order.Book.Price*order.NumOfCopies;

                row.Cells.Add(titleCell);
                row.Cells.Add(qaunCell);
                row.Cells.Add(subCell);
                cartTable.Rows.Add(row);
            }
            var totalRow = new TableRow();
            var totalCell = new TableCell
            {
                Text = "Total: " + String.Format("{0:C}", total),
            };
            var bufferCell = new TableCell { ColumnSpan = 2 };
            totalRow.Cells.Add(bufferCell);
            totalRow.Cells.Add(totalCell);
            cartTable.Rows.Add(totalRow);
        }
        else
        {
            var emptyRow = new TableRow
            {
                ForeColor = Color.Red,
                HorizontalAlign = HorizontalAlign.Center
            };
            var emptyCell = new TableCell
            {
                Text = "Your shopping cart is empty",
                ColumnSpan = 3,
                ForeColor = Color.Red,
                HorizontalAlign = HorizontalAlign.Center,
                
            };

            emptyRow.Cells.Add(emptyCell);
            cartTable.Rows.Add(emptyRow);
        }

    }
}