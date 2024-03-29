﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Collections;
using Terraria;
using TShockAPI;
using TShockAPI.DB;
using static MonoMod.InlineRT.MonoModRule;
using static System.Net.Mime.MediaTypeNames;

namespace History.Commands
{
    public class SaveCommand : HCommand
    {
        private Action[] actions;

        public SaveCommand(Action[] actions)
        : base(null)
        {
            this.actions = actions;
        }
        
        public override void Execute()
        {
            foreach (Action a in actions)
            {
                History.Database.Query("INSERT INTO History(Time, Account, Action, XY, Data, Style, Paint, WorldID, Text, Alternate, Random, Direction) VALUES(@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11)",
                    a.time,a.account,a.action, (a.x << 16) + a.y, a.data,a.style,a.paint,Main.worldID,a.text,a.alt,a.random,a.direction ? 1 : -1);
            }

            //History.Database.Query("INSERT INTO History(Time, Account, Action, XY, Data, Style, Paint, WorldID, Text, Alternate, Random, Direction) VALUES(@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11)")
            //using (IDbConnection db = History.Database.CloneEx())
            //{
            //    db.Open();
            //    using (SqliteTransaction transaction = (SqliteTransaction)db.BeginTransaction())
            //    {
            //        using (SqliteCommand command = (SqliteCommand)db.CreateCommand())
            //        {
            //            command.CommandText = "INSERT INTO History (Time, Account, Action, XY, Data, Style, Paint, WorldID, Text, Alternate, Random, Direction) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11)";
            //            for (int i = 0; i < 12; i++)
            //            {
            //                command.AddParameter("@" + i, DBNull.Value);
            //            }
            //            command.Parameters[7].Value = Main.worldID;

            //            foreach (Action a in actions)
            //            {
            //                command.Parameters[0].Value = a.time;
            //                command.Parameters[1].Value = a.account;
            //                command.Parameters[2].Value = a.action;
            //                command.Parameters[3].Value = (a.x << 16) + a.y;
            //                command.Parameters[4].Value = a.data;
            //                command.Parameters[5].Value = a.style;
            //                command.Parameters[6].Value = a.paint;
            //                command.Parameters[8].Value = a.text;
            //                command.Parameters[9].Value = a.alt;
            //                command.Parameters[10].Value = a.random;
            //                command.Parameters[11].Value = a.direction ? 1 : -1;
            //                command.ExecuteNonQuery();
            //            }
            //        }
            //        transaction.Commit();
            //    }
            //}

        }
    }
}
