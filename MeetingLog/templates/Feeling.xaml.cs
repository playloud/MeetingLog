using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MeetingLog
{
	public partial class Feeling : ContentView
	{
		public int Order
		{
			get;
			set;
		}

		public Feeling()
		{
			InitializeComponent();
		}

		public Feeling(FeelingData data)
		{
			InitializeComponent();
			this.Order = data.Order;

			int selectedIndex = pickerFeeling.Items.IndexOf(data.Feeling);
			this.pickerFeeling.SelectedIndex = selectedIndex;
		}

		public FeelingData GetData()
		{
			FeelingData data = new FeelingData();
			data.Order = this.Order;
			data.Feeling = this.pickerFeeling.Items[pickerFeeling.SelectedIndex];
			return data;
		}
	}
}
