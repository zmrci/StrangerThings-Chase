using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace StrangerThings.Model
{
    public class GameModel
    {
        public int Size;
        public Cell[,] Map;
        private Random rand;
        public event EventHandler? StepMade;
        public event EventHandler<int>? GameOver;


        private int ElevenX;
        private int ElevenY;

        private int DemX;
        private int DemY;

        private List<(int, int)> portals;

        private System.Timers.Timer DemogorgonSpawn;
        private System.Timers.Timer GameTimer;

        private System.Timers.Timer DemStep;
        private System.Timers.Timer ChaseTime;
        private int time = 0;

        public GameModel() 
        {
            Size = 15;
            Map = new Cell[Size,Size];
            rand = new Random();
            portals = new List<(int, int)>() { };
            DemogorgonSpawn = new System.Timers.Timer();
            GameTimer = new System.Timers.Timer();
            GameTimer.Interval = 1000;
            GameTimer.Elapsed += (o, e) => { time++; };
            GameTimer.Start();

            DemogorgonSpawn.Interval = 5000;
            DemogorgonSpawn.Elapsed += SpawnDemogorgon;
            DemogorgonSpawn.Start();
            DemStep = new Timer(); 
            ChaseTime = new System.Timers.Timer();



            MakeMap();
        }

        private void DemogorgonMove(object? O, ElapsedEventArgs e) 
        {
            Map[DemX, DemY].Owner = Owner.None;
            if (DemX < ElevenX)
            {
                DemX++;
            }
            else if (DemX > ElevenX)
            {
                DemX--;
            }
            else if (DemY < ElevenY)
            {
                DemY++;
            }
            else if (DemY > ElevenY) 
            {
                DemY--;
            }
            else
            {
                GameOver?.Invoke(this, time);
                DemStep.Stop();
                DemogorgonSpawn.Stop(); 
                return;
            }

            Map[DemX, DemY].Owner = Owner.Demogorgon;
            StepMade?.Invoke(this,EventArgs.Empty);
        }

        private void SpawnDemogorgon(object? o,ElapsedEventArgs e) 
        {

            DemStep.Start();
            DemStep.Interval = 500;
            DemStep.Elapsed += DemogorgonMove;

            ChaseTime.Interval = 6000;
            ChaseTime.Elapsed += RemoveDem;
            ChaseTime.Start();

            int x;
            int y;
            (x, y) = portals[rand.Next(0, 4)];


            DemogorgonSpawn.Interval = 11000;



            Map[x, y].Owner = Owner.Demogorgon;
            DemX = x;
            DemY = y;
            StepMade?.Invoke(this, EventArgs.Empty);

        }

        public void MoveEleven(int x,int y) 
        {
            if(ElevenX + x < Size && ElevenX + x >= 0 && ElevenY + y < Size && ElevenY + y>= 0) 
            {
                Map[ElevenX, ElevenY].Owner = Owner.None;
                ElevenX += x;
                ElevenY += y;
                Map[ElevenX, ElevenY].Owner = Owner.Eleven;

                foreach (var i in portals)
                {
                    Map[i.Item1, i.Item2].Owner = Owner.Portal;
                }

                StepMade?.Invoke(this, EventArgs.Empty);
            }
            else 
            {
                return;
            }
        }

        private void RemoveDem(object? o,ElapsedEventArgs e) 
        {
            DemStep.Stop();
            ChaseTime.Stop();

            Map[DemX, DemY].Owner = Owner.None;
            foreach (var i in portals)
            {
                Map[i.Item1, i.Item2].Owner = Owner.Portal;
            }
        }

        private void MakeMap() 
        {
            for(int i = 0; i < Size;i++) 
            {
                for(int j = 0; j < Size; j++)
                {
                    Map[i, j] = new Cell(i, j, Owner.None);
                }
            }

            ElevenX = 7;
            ElevenY = 7;
            Map[7, 7].Owner = Owner.Eleven;

            int valX = rand.Next(0, 7);
            int valY = rand.Next(0, 7);
            Map[valX, valY].Owner = Owner.Portal;
            portals.Add((valX, valY));

            valX = rand.Next(8, Size);
            valY = rand.Next(0,7);

            Map[valX, valY].Owner = Owner.Portal;
            portals.Add((valX, valY));

            valX = rand.Next(0,7);
            valY = rand.Next(8,Size);


            Map[valX, valY].Owner = Owner.Portal;
            portals.Add((valX, valY));


            valX = rand.Next(8, Size);
            valY = rand.Next(8, Size);


            Map[valX, valY].Owner = Owner.Portal;
            portals.Add((valX, valY));

            foreach(var i in portals) 
            {
                Map[i.Item1,i.Item2].Owner = Owner.Portal;
            }



        }
    }
}
