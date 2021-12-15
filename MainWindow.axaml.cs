using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace X11Interlock
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            Button showProgressButton = this.Find<Button>("mShowProgressButton");
            showProgressButton.Click += ShowProgressButton_Click;

            Button hideProgressButton = this.Find<Button>("mHideProgressButton");
            hideProgressButton.Click += HideProgressButton_Click;

            DockPanel dockPanel = this.Find<DockPanel>("mContentPanel");

            DockPanel fakePanel = new DockPanel();
            mOverlayProgressPanel = new OverlayProgressControl(fakePanel);

            dockPanel.Children.Add(mOverlayProgressPanel);
        }

        private void HideProgressButton_Click(object? sender, RoutedEventArgs e)
        {
            mOverlayProgressPanel.HideProgress();
        }

        void ShowProgressButton_Click(object? sender, RoutedEventArgs e)
        {
            mOverlayProgressPanel.ShowProgress("Hello World!");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        OverlayProgressControl mOverlayProgressPanel;
    }
}