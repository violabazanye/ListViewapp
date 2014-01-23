using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.MobileServices;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ListViewApp
{
    public class Items
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
    
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MobileServiceCollection<Items, Items> itemsCollection;
        private IMobileServiceTable<Items> itemsTable = App.listviewserviceClient.GetTable<Items>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void InsertItemsTable(Items item)
        {
            try
            {
                await itemsTable.InsertAsync(item);
                itemsCollection.Add(item);
            }
            catch (Exception) { }
        }

        private async void ReadItemsTable()
        {
            try
            {
                itemsCollection = await itemsTable.ToCollectionAsync();
                Content.ItemsSource = itemsCollection;
            }
            catch (InvalidOperationException) { }
        }

        private void SubmitBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var item = new Items { Title = title_input.Text , Description = desc_input.Text };
            InsertItemsTable(item);
            ReadItemsTable();
        }
    }
}
