using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace CopyPaster.Copy
{
	public class CP
	{
		public ClipingList list { get; set; }

		public CP(bool autoStart = true)
		{
			if (autoStart)
				Start();
		}

		public void Start()
		{
			Clipboard.ContentChanged += Clipboard_ContentChanged;
		}
		public void Stop()
		{
			Clipboard.ContentChanged -= Clipboard_ContentChanged;
		}

		private async void Clipboard_ContentChanged(object sender, object e)
		{
			DataPackageView dataPackageView = Clipboard.GetContent();
			if (dataPackageView.Contains(StandardDataFormats.Text))
			{
				list.AddCliping(await dataPackageView.GetTextAsync());
			}
		}
	}
}
