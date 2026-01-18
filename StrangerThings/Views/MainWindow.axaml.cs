using Avalonia.Controls;
using Avalonia.Input;
using Avalonia;
using Avalonia.Media;
using System;
using System.Globalization;
namespace StrangerThings.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }


    public event EventHandler<Key>? KeyPressed;

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        KeyPressed?.Invoke(this, e.Key);
        e.Handled = true;
    }
}

