using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Layout;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Media;

namespace X11Interlock
{
    internal class OverlayProgressControl : Grid
    {
        internal OverlayProgressControl(Control targetControl)
        {
            BuildComponents(targetControl);
        }

        internal void ShowProgress(string text)
        {
            SetProgressText(text);
            mOverlayProgressPanel.Show();
        }

        internal void HideProgress()
        {
            mOverlayProgressPanel.Hide();
        }

        internal void SetProgressText(string text)
        {
            mOverlayProgressPanel.SetProgressText(text);
        }

        void BuildComponents(Control targetControl)
        {
            mOverlayProgressPanel = new ProgressPanel();
            mOverlayProgressPanel.Opacity = 0;

            Children.Add(targetControl);
            Children.Add(mOverlayProgressPanel);
        }

        class ProgressPanel : Border
        {
            internal ProgressPanel()
            {
                BuildComponents();
            }

            internal void SetProgressText(string text)
            {
                mTextBlock.Text = text;
            }

            internal void Show()
            {
                Opacity = 1;
            }

            internal void Hide()
            {
                Opacity = 0;
            }

            void BuildComponents()
            {
                Background = new SolidColorBrush() { Color = Color.FromArgb(125, 248, 248, 248) };

                mProgressBar = new ProgressBar();
                mProgressBar.IsIndeterminate = true;
                mTextBlock = new TextBlock();
                mTextBlock.MaxWidth = 300;
                mTextBlock.TextWrapping = TextWrapping.Wrap;
                mTextBlock.TextAlignment = TextAlignment.Center;

                StackPanel contentPanel = new StackPanel();
                contentPanel.VerticalAlignment = VerticalAlignment.Center;
                contentPanel.HorizontalAlignment = HorizontalAlignment.Center;
                contentPanel.Orientation = Orientation.Vertical;

                contentPanel.Children.Add(mProgressBar);
                contentPanel.Children.Add(mTextBlock);

                Child = contentPanel;

                DoubleTransition transition = new DoubleTransition();
                transition.Property = Control.OpacityProperty;
                transition.Duration = TimeSpan.FromMilliseconds(125);
                Transitions = new Transitions();
                Transitions.Add(transition);
            }

            protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change)
            {
                base.OnPropertyChanged(change);

                if (change.Property == OpacityProperty)
                {
                    bool isVisible = change.NewValue.GetValueOrDefault<double>() > 0;
                    // explicitly hide the progress bar so
                    // the Rotator animation is stopped
                    mProgressBar.IsVisible = isVisible;
                    IsVisible = isVisible;
                }
            }

            TextBlock mTextBlock;
            ProgressBar mProgressBar;
        }

        ProgressPanel mOverlayProgressPanel;
    }
}