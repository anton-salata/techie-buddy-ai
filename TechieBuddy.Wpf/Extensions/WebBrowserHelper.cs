using System.Windows;
using System.Windows.Controls;

namespace TechieBuddy.Wpf.Extensions
{
    public static class WebBrowserHelper
    {
        public static readonly DependencyProperty BindableSourceProperty =
            DependencyProperty.RegisterAttached(
                "BindableSource",
                typeof(string),
                typeof(WebBrowserHelper),
                new UIPropertyMetadata(null, BindableSourcePropertyChanged));

        public static string GetBindableSource(DependencyObject obj)
        {
            return (string)obj.GetValue(BindableSourceProperty);
        }

        public static void SetBindableSource(DependencyObject obj, string value)
        {
            obj.SetValue(BindableSourceProperty, value);
        }

        public static void BindableSourcePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is WebBrowser webBrowser)
            {
                var html = e.NewValue as string;
                webBrowser.NavigateToString(html ?? "<html><body></body></html>");
            }
        }
    }
}
