using System;
using System.Collections.Generic;

using Xamarin.Forms;

using DNNCon.Security;

namespace DNNCon
{
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent ();

			this.btnLogin.Clicked += this.btnLoginClick;
		}

		async void btnLoginClick(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty (this.txtUsername.Text) && !string.IsNullOrEmpty (this.txtPassword.Text)) 
			{
				var token = await Authorization.GetTokenAsync (this.txtUsername.Text, this.txtPassword.Text);

				if (token != null) 
				{
					App.BEARER_TOKEN = token.AccessToken;
				}

				App.SetMainPage (new Items (), true);
			}
		}
	}
}

