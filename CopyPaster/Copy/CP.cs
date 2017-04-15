using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace CopyPaster.Copy
{
	public class CP
	{
		public ClipingList listText { get; set; }

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
					foreach (Cliping c in listText.Clipings)
					{
						if (c.Content == tmp)
						{
							listText.MoveClipToTop(index);
							return;
						}
						index++;
					}
					listText.AddCliping(tmp);
					return;
				}
				if(dataPackageView.Contains(StandardDataFormats.Bitmap))
				{
					var tmp = await dataPackageView.GetBitmapAsync();
					IRandomAccessStream tmp2 = await tmp.OpenReadAsync();
					BitmapImage a = new BitmapImage();
					a.SetSource(tmp2);
					int index = 0;
					foreach(ClipingImage c in listText.ClipingsImage)
					{
						listText.AddClipingImage(a);
					}
				}
			}
			catch(Exception ex)
			{
				listText.ErrorText = ex.Message;
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
				listText.ErrorText = ex.Message;
			}
		}
	}
}
