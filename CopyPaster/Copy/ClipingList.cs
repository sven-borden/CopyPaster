using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyPaster.Copy
{
	public class ClipingList
	{
		private ObservableCollection<Cliping> clipings = new ObservableCollection<Cliping>();
		public ObservableCollection<Cliping> Clipings { get { return this.clipings; } }

		public ClipingList()
		{

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
