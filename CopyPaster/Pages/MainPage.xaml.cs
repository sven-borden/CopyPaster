using CopyPaster.Copy;
using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CopyPaster
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
		ClipingList clipList;
		CP cp = new CP(false);
		bool isCompacted = false;

		public MainPage()
        {
			clipList = new ClipingList();
			SetList();
			this.InitializeComponent();
			CheckCompaction();
			SetTimer();
        }

		private void SetTimer()
		{
			DispatcherTimer timer = new DispatcherTimer();
			timer.Interval = new TimeSpan(0, 0, 01);
			timer.Tick += (e, t) =>
			{
				foreach(Cliping c in clipList.Clipings)
				{
					c.LastModified = c.LastModified.Add(new TimeSpan(10)) ;
				}
			};

			timer.Start();
		}

		private void SetList()
		{
			clipList = new ClipingList();
			cp.list = clipList;
			cp.Start();
		}

		private bool CheckCompaction()
		{
			if (ApplicationView.GetForCurrentView().IsViewModeSupported(ApplicationViewMode.CompactOverlay))
				return true;
			else
				return false;
		}

		private void Ellipse_Tapped(object sender, TappedRoutedEventArgs e)
		{
			isCompacted = !isCompacted;
			SetCompaction();
		}

		private async void SetCompaction()
		{
			if (isCompacted)
			{
				StateIcon.Glyph = "\uE2B3";
				await ApplicationView.GetForCurrentView().TryEnterViewModeAsync(ApplicationViewMode.CompactOverlay);
				await UpPanel.Fade(0).StartAsync();
				UpPanel.Visibility = Visibility.Collapsed;
			}
			else
			{
				UpPanel.Visibility = Visibility.Visible;
				await UpPanel.Fade(1).StartAsync();
				StateIcon.Glyph = "\uE2B4";
				await ApplicationView.GetForCurrentView().TryEnterViewModeAsync(ApplicationViewMode.Default);
			}
		}

		private async void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
		{
			if (!isCompacted)
				return;
			if (CheckCompaction())
			{
				CompactionButton.Visibility = Visibility.Visible;
				await CompactionButton.Fade(1).StartAsync();
			}
		}

		private async void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
		{
			if (!isCompacted)
				return;
			await CompactionButton.Fade(0).StartAsync();
			CompactionButton.Visibility = Visibility.Collapsed;
		}

		private void ListView_ItemClick(object sender, SelectionChangedEventArgs e)
		{
			cp.SetTextInClip(clipList.Clipings[(sender as ListView).SelectedIndex].Content);
		}
	}
}
