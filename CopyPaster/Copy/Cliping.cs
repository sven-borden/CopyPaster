using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace CopyPaster.Copy
{
	public class Cliping
	{
		public string Content { get; set; }
		public DateTime LastModified { get; set; }

		public Cliping(string _content)
		{
			LastModified = DateTime.Now;
			Content = _content;
		}
	}

	public class DateToStringConverter : IValueConverter
	{
		#region IValueConverter Members

		// Define the Convert method to change a DateTime object to 
		// a month string.
		public object Convert(object value, Type targetType,
			object parameter, string language)
		{
			// The value parameter is the data from the source object.
			DateTime thisdate = (DateTime)value;

			double second = DateTime.Now.Subtract(thisdate).TotalSeconds;
			if (second < 60)
				return "A few second "+ second;
			double minute = DateTime.Now.Subtract(thisdate).TotalMinutes;
			if (minute < 60)
				return minute + " minutes";
			double hour = DateTime.Now.Subtract(thisdate).TotalHours;
			if (hour < 24)
				return hour + " hours";
			return "More than a day";
			
			// Return the month value to pass to the target.
		}

		// ConvertBack is not implemented for a OneWay binding.
		public object ConvertBack(object value, Type targetType,
			object parameter, string language)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
