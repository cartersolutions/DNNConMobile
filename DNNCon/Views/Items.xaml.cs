using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

using DNNCon.Repositories;

namespace DNNCon
{
	public partial class Items : ContentPage
	{
		public Items ()
		{
			ToolbarItems.Add (new ToolbarItem{
				Text = "Exit",
				Command = new Command((cmd) => {
					App.SetMainPage(new Login(), false);
				})
			});

			InitializeComponent ();

			this.PopulateItems ();
		}

		public void PopulateItems()
		{
			var items = Task.Run (() => {
				return ItemRepository.GetItemsAsync ();
			}).GetAwaiter ().GetResult ();

			if (items.Count > 0) {
				this.itemsView.ItemsSource = items;
			}
		}
	}
}

