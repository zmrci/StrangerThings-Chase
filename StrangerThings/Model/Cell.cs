using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrangerThings.Model
{
    public enum Owner { Eleven , Demogorgon, Portal,None}

    public class Cell
    {
        public int X, Y;
        public Owner Owner;

        public Cell(int x, int y,Owner o) 
        {
            X = x; Y = y;   
            Owner = o;
        }

    }
}
