using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.Common.Utils;

namespace NopSolutions.NopCommerce.Web
{
    public partial class InspiredBaking : BaseNopPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ProductManager.RecentlyAddedProductsEnabled)
            {
                Response.Redirect(CommonHelper.GetStoreLocation());
            }

            string title = GetLocaleResourceString("PageTitle.Baking");
            SEOHelper.RenderTitle(this, title, true);
        }

        public override PageSslProtectionEnum SslProtected
        {
            get
            {
                return PageSslProtectionEnum.No;
            }
        }
    }
}