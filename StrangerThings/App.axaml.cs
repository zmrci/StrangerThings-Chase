using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using StrangerThings.Model;
using StrangerThings.ViewModels;
using StrangerThings.Views;
using Avalonia.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Splat;
using Avalonia.Threading;

namespace StrangerThings;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private GameModel? _model;
    private GameViewModel? _viewModel;
    private MainWindow? _mainWindow;

    private async void GameOverMessage(object? o, int e) 
    {
        await Dispatcher.UIThread.InvokeAsync(async () =>
        {
            if (_mainWindow != null)
            {
                var box = MessageBoxManager.GetMessageBoxStandard(
                    "Game Over", 
                    $"The game has ended! You have survived for {e} seconds!",
                    ButtonEnum.Ok);
                await box.ShowWindowDialogAsync(_mainWindow);
            }
            NewGame();
        });
    }

    private void NewGame() 
    {
        if(_model is not null) 
        {
            _model.GameOver -= GameOverMessage;
            _model = null;
            _viewModel = null;
        }
        _model = new GameModel();
        _viewModel = new GameViewModel(_model);
        if(_mainWindow is not null) 
        {
            _model.GameOver += GameOverMessage;
            _mainWindow.DataContext = _viewModel;
        }

    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            _model = new GameModel();
            _viewModel = new GameViewModel(_model);
            _mainWindow = new MainWindow();
            _model.GameOver += GameOverMessage;
            _mainWindow.KeyPressed += (sender, key) => _viewModel.HandleKeyPress(key);

            desktop.MainWindow = _mainWindow;
            _mainWindow.DataContext = _viewModel;


        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
