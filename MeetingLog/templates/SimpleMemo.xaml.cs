using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MeetingLog
{
	public partial class SimpleMemo : ContentView
	{
		SimpleMemoData sdata;

		public int Order
		{
			get;
			set;
		}

		public SimpleMemo()
		{
			InitializeComponent();
		}

		public SimpleMemo(SimpleMemoData sdata)
		{
			InitializeComponent();
			this.sdata = sdata;
			this.Order = sdata.Order;
			this.editor.Text = sdata.Memo;
		}

		public SimpleMemoData GetData()
		{
			SimpleMemoData data = new SimpleMemoData();

			data.Order = this.Order;
			data.Memo = this.editor.Text;

			return data;
		}


	}
}
