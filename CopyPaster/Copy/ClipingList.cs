using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace CopyPaster.Copy
{
	public class ClipingList
	{
		private ObservableCollection<Cliping> clipings = new ObservableCollection<Cliping>();
		public ObservableCollection<Cliping> Clipings { get { return this.clipings; } }
		private ObservableCollection<ClipingImage> clipingsImage = new ObservableCollection<ClipingImage>();
		public ObservableCollection<ClipingImage> ClipingsImage { get { return this.clipingsImage; } }

		public ClipingList()
		{

		}

		public void AddClipingImage(BitmapImage _content)
		{
			clipingsImage.Insert(0, new ClipingImage(_content));
		}

		internal void MoveClipImageToTop(int index)
		{
			clipingsImage.Move(index, 0);
			clipingsImage[0].LastModified = DateTime.Now;
		}

		public void AddCliping(string _content)
		{
			clipings.Insert(0,new Cliping(_content));
		}

		internal void MoveClipToTop(int index)
		{
			clipings.Move(index, 0);
			clipings[0].LastModified = DateTime.Now;
		}
	}
}
