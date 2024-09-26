using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Media;

namespace UI
{
    public static class Sounds
    {
        private static readonly Dictionary<string, MediaPlayer> soundPlayers = new();

        static Sounds()
        {
            //Ficheiros estao na pasta bin, por algum motivo n funcionam os da pasta sounds );
            LoadSound("move_self", "move_self.wav");
            LoadSound("move_check", "move_check.wav");
            LoadSound("promotion", "promote.wav");
        }

        private static void LoadSound(string key, string filePath)
        {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(filePath, UriKind.RelativeOrAbsolute)); 
            soundPlayers[key] = player;
        }

        public static void PlaySound(string key)
        {
            if (soundPlayers.TryGetValue(key, out MediaPlayer player))
            {
                player.Stop();
                player.Play(); 

            }
        }
    }
}
