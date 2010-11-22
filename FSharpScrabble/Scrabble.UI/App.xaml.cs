﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

using Scrabble.Core.Types;

namespace Scrabble.UI
{
    /// <summary>
    /// Kicks off the game and initializes some Windows.
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            PlayerWindows = new LinkedList<GameWindow>();
            //want to launch a menu and get some info about players, but for now just start
            StartGame();            
        }

        private void StartGame()
        {
            //show player windows
            foreach (HumanPlayer p in Game.Instance.HumanPlayers)
            {
                GameWindow w = new GameWindow(p);
                p.Window = w;
                w.Show();
                if (PlayerWindows.Count > 0)
                    PlayerWindows.AddAfter(PlayerWindows.Last, w);
                else
                    PlayerWindows.AddFirst(w);
            }

            //Call this to give each player tiles, and ask the first player for a move.
            Game.Instance.Start();
                        
        }

        //To support more than 2 people - this might already be handled in the f#
        public LinkedList<GameWindow> PlayerWindows { get; set; }
                
    }
}