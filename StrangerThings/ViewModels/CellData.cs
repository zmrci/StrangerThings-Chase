using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrangerThings.Model;

namespace StrangerThings.ViewModels
{
    public partial class CellData : ObservableObject
    {
        private Cell c;

        public int X, Y;

        [ObservableProperty]
        private string getOwner;

        public CellData(Cell c,int x, int y) 
        {
            this.c = c;
            this.X = x;
            this.Y = y;

            GetOwner = c.Owner.ToString();
        }

        public void UpdateCellData(Cell c) 
        {
            this.c = c;

            GetOwner = c.Owner.ToString();
        }
    }
}
