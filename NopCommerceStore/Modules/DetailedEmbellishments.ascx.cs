using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.BusinessLogic.Products;

namespace NopSolutions.NopCommerce.Web.Modules
{
    public partial class DetailedEmbellishments : BaseNopUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            int number = ProductManager.RecentlyAddedProductsNumber;
            var products = ProductManager.GetRecentlyAddedProducts(number);
            if (ProductManager.RecentlyAddedProductsEnabled && products.Count > 0)
            {
                dlCatalog.DataSource = products;
                dlCatalog.DataBind();
            }
            else
            {
                this.Visible = false;
            }
        }
    }
}