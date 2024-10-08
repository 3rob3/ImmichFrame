using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.PanAndZoom;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using ImmichFrame.Helpers;
using ImmichFrame.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ImmichFrame.Views;

public partial class MainView : BaseView
{
    private MainViewModel? _viewModel;
    private ZoomBorder? _zoomBorder;
    public MainView()
    {
        InitializeComponent();
        this.AttachedToVisualTree += OnAttachedToVisualTree;
    }
    private async void OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        if (PlatformDetector.IsAndroid())
        {
            var insetsManager = TopLevel.GetTopLevel(this)?.InsetsManager;
            if (insetsManager != null)
            {
                insetsManager.DisplayEdgeToEdge = true;
                insetsManager.IsSystemBarVisible = false;
            }
        }

        _viewModel = (this.DataContext as MainViewModel)!;
        await InitializeViewModelAsync();
        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }
    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MainViewModel.Images))
        {
            ApplyZoom();
        }
    }
    private async Task InitializeViewModelAsync()
    {
        try
        {
            await _viewModel!.InitializeAsync();
        }
        catch (Exception ex)
        {
            _viewModel!.Navigate(new ErrorViewModel(ex));
        }
    }   

    public override void Dispose()
    {
        if (_viewModel != null)
            _viewModel.TimerEnabled = false;

        base.Dispose();
    }
    //private void OnImagesChanged(object? sender, EventArgs e)
    //{
    //    ApplyZoom(); 
    //}
    private void ApplyZoom()
    {
        if (_zoomBorder != null)
        {
            var zoomImage = _zoomBorder.GetVisualDescendants().OfType<Image>().FirstOrDefault();
            if (zoomImage != null)
            {
                zoomImage.PropertyChanged += ZoomImage_PropertyChanged;
            }
        }
    }
    private async void ZoomImage_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property.Name == nameof(Image.Source))
        {
            var zoomImage = sender as Image;
            if (zoomImage != null && zoomImage.Source != null)
            {

                if (_zoomBorder != null)
                {
                    _zoomBorder.AutoFit();
                    // Get the actual size of the image and the container
                    //var imageSize = zoomImage.Bounds;
                    //var containerSize = _zoomBorder.Bounds;
                    var imageSize = new Size(zoomImage.Bounds.Width, zoomImage.Bounds.Height);
                    var containerSize = new Size(_zoomBorder.Bounds.Width, _zoomBorder.Bounds.Height);

                    // Calculate if the image is smaller than the container
                    double horizontalRatio = containerSize.Width / imageSize.Width;
                    double verticalRatio = containerSize.Height / imageSize.Height;

                    // Take the smallest ratio to keep the aspect ratio intact
                    double targetZoom = Math.Max(horizontalRatio, verticalRatio);

                    if (targetZoom > 1)
                    {
                        // Image is smaller than the container, so slowly zoom in
                        double centerX = containerSize.Width / 2;
                        double centerY = containerSize.Height / 2;

                        await SlowZoomIn(targetZoom, centerX, centerY);
                    }
                }
            }
        }
    }
    private async void ZoomBorder_Loaded(object? sender, RoutedEventArgs e)
    {
        _zoomBorder = sender as ZoomBorder;
    }
    private async Task SlowZoomIn(double targetZoom, double centerX, double centerY)
    {
        double currentZoom = 1.0; // Assuming the default zoom level is 1
        double zoomStep = 0.5;   // Adjust this value for faster/slower zoom

        // Gradually zoom in
        while (currentZoom < targetZoom)
        {
            currentZoom += zoomStep;
            _zoomBorder.Zoom(currentZoom, centerX, centerY, skipTransitions: true); // Apply zoom at the center
            await Task.Delay(50);         // Delay to make the zoom smooth
        }

        _zoomBorder.Zoom(targetZoom, centerX, centerY, skipTransitions: true);
    }
}
