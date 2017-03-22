using CopyPaster.Copy;
using System;
using System.Collections.Generic;
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
				await ApplicationView.GetForCurrentView().TryEnterViewModeAsync(ApplicationViewMode.CompactOverlay);
			else
				await ApplicationView.GetForCurrentView().TryEnterViewModeAsync(ApplicationViewMode.Default);
		}

		private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
		{
			if (CheckCompaction())
				CompactionButton.Visibility = Visibility.Visible;
		}

		private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
		{
			CompactionButton.Visibility = Visibility.Collapsed;
		}
	}
}
