using CommunityToolkit.Mvvm.ComponentModel;
using StrangerThings.Model;
using System;
using Avalonia.Input;
using Avalonia;
using System.Collections.ObjectModel;

namespace StrangerThings.ViewModels;

public partial class GameViewModel : ObservableObject
{
    private GameModel _model;
    public int Size { get; set; }
    public ObservableCollection<CellData> Cells { get; set; }

    public GameViewModel(GameModel model) 
    {
        _model = model;
        Size = _model.Size;
        Cells = new ObservableCollection<CellData>();
        MakeCells();
        _model.StepMade += UpdateCells;
    }

    public void HandleKeyPress(Key key)
    {

        int dx = 0, dy = 0;
        bool validKey = false;

        switch (key)
        {
            case Key.Up: dy = -1; validKey = true; break;
            case Key.Down: dy = 1; validKey = true; break;
            case Key.Left: dx = -1; validKey = true; break;
            case Key.Right: dx = 1; validKey = true; break;
        }

        if (validKey)
        {
            _model?.MoveEleven(dy,dx);
        }
    }

    private void UpdateCells(object? o, EventArgs e) 
    {
        int count = 0;
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Cells[count].UpdateCellData(_model.Map[i, j]);
                count++;
            }
        }

    }

    private void MakeCells() 
    {
        for (int i = 0; i < Size; i++) 
        {
            for(int j = 0; j < Size; j++) 
            {
                Cells.Add(new CellData(_model.Map[i,j],i,j));
            }
        }
    }
}
