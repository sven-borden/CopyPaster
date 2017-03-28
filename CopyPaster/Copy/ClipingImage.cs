using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace CopyPaster.Copy
{
	public class ClipingImage : INotifyPropertyChanged
	{
		public ClipingImage(BitmapImage _image)
		{
			Content = _image;
		}

		public BitmapImage Content { get; set; }
		private DateTime _lastModified;
		public DateTime LastModified
		{
			get
			{
				return _lastModified;
			}
			set
			{
				_lastModified = value;
				OnPropertyChanged("LastModified");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		void OnPropertyChanged(string propertyName)
		{
			// the new Null-conditional Operators are thread-safe:
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
