using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace PlayersMVVM.Model
{
    class Players
    {
        List<Player> PlayersList = LoadPlayers(@"players.json");

        public void AddPlayerMethod(Player player) { PlayersList.Add(player); }
        public void RemovePlayerMethod(int index) { PlayersList.RemoveAt(index); }
        public void ModifyPlayerMethod(Player player, int index) { PlayersList[index] = player; }
        public List<Player> GetPlayerList { get => PlayersList; }

        private static List<Player> LoadPlayers(string FileName)

        {
            List<Player> PlayersList = new List<Player>();
            if (File.Exists(FileName))
                PlayersList = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(FileName));
            return PlayersList;
        }

        public void SavePlayers(string FileName)
        {
            string save = JsonConvert.SerializeObject(PlayersList);
            File.WriteAllText(FileName, save);
        }
    }
}
