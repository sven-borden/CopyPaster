using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;

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
			try
			{
				DataPackageView dataPackageView = Clipboard.GetContent();
				
				if (dataPackageView.Contains(StandardDataFormats.Text))
				{
					string tmp = await dataPackageView.GetTextAsync();
					int index = 0;
					foreach (Cliping c in list.Clipings)
					{
						if (c.Content == tmp)
						{
							list.MoveClipToTop(index);
							return;
						}
						index++;
					}
					list.AddCliping(tmp);
				}
			}
			catch(Exception ex)
			{

			}
		}

		internal void SetTextInClip(string content)
		{
			try
			{
				DataPackage dataPackage = new DataPackage();
				dataPackage.SetText(content);

				Clipboard.SetContent(dataPackage);
			}
			catch (Exception ex)
			{

			}
		}
	}
}
