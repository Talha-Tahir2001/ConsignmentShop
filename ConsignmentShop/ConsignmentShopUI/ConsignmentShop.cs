using ConsignmentShopLibrary;



namespace ConsignmentShopUI
{
    public partial class ConsignmentShop : Form
    {
        private Store store = new Store();

        private List<Item> shoppingCartData = new List<Item>();

        BindingSource itemsBinding = new BindingSource();

        BindingSource cartBinding = new BindingSource();

        BindingSource vendorsBinding = new BindingSource();
        private decimal storeProfit = 0;
        public ConsignmentShop()
        {
            InitializeComponent();
            SetupData();
            
            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            itemsListBox.DataSource = itemsBinding;

            itemsListBox.DisplayMember = "Display";
            itemsListBox.ValueMember = "";

            cartBinding.DataSource = shoppingCartData;
            shoppingCartListBox.DataSource = cartBinding;

            shoppingCartListBox.DisplayMember = "Display";
            shoppingCartListBox.ValueMember = "";

            vendorsBinding.DataSource = store.Vendors;
            vendorListBox.DataSource = vendorsBinding;

            vendorListBox.DisplayMember = "Display";
            vendorListBox.ValueMember = "";
        }
        private void SetupData()
        {
            //Vendor demoVendor = new Vendor();
            //demoVendor.FirstName = "Bill";
            //demoVendor.LastName = "Smith";
            //demoVendor.Commision = .5;

            //store.Vendors.Add(demoVendor);

            //demoVendor = new Vendor();
            //demoVendor.FirstName = "Sue";
            //demoVendor.LastName = "Jones";
            //demoVendor.Commision = .5;

            store.Vendors.Add(new Vendor 
            { 
                FirstName = "Bill", 
                LastName = "Smith" 
            });
            
            store.Vendors.Add(new Vendor 
            { 
                FirstName = "Sue", 
                LastName = "Jones" 
            });

            store.Items.Add(new Item
            {
                Title = "Moby Dick",
                Description = "A book about a Whale",
                Price = 4.50M,
                Owner = store.Vendors[0]
            });


            store.Items.Add(new Item
            {
                Title = "A Tale of Two Cities",
                Description = "A book about a Revolution",
                Price = 3.80M,
                Owner = store.Vendors[1]
            });

            store.Items.Add(new Item
            {
                Title = "Harry Potter",
                Description = "A book about a Wizard",
                Price = 5.20M,
                Owner = store.Vendors[1]
            });

            store.Items.Add(new Item
            {
                Title = "The Chronicles of Narnia",
                Description = "A book about a Lion that can talk",
                Price = 6.10M,
                Owner = store.Vendors[0]
            });

            store.Name = "Seconds are Better";
        }

        private void addToCart_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("I have been clicked");
            Item selectedItem = (Item)itemsListBox.SelectedItem;
            //MessageBox.Show(selectedItem.Title + " has been added to your cart");
            shoppingCartData.Add(selectedItem);

            cartBinding.ResetBindings(false);
        }

        private void makePurchase_Click(object sender, EventArgs e)
        {
            foreach (Item item in shoppingCartData)
            {
                item.Sold = true;
                item.Owner.PaymentDue += (decimal)item.Owner.Commision * item.Price; 
                storeProfit += (1 - (decimal)item.Owner.Commision) * item.Price;
            }
            shoppingCartData.Clear();


            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();

            storeProfitText.Text = string.Format("${0}", storeProfit);

            cartBinding.ResetBindings(false);
            itemsBinding.ResetBindings(false);
            vendorsBinding.ResetBindings(false);
        }
    }
}