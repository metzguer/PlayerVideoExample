using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace PlayerVideo
{
    public partial class Player : Form
    {
        private static string path = string.Empty;
        private static string[] files;
        public Player()
        {
            InitializeComponent();
            mediaPlayer.uiMode = "none";
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (searchFiles.ShowDialog() == DialogResult.OK) {

                int filesSelected = searchFiles.FileNames.Length;
                if (filesSelected <= 0)
                {
                    MessageBox.Show("Error en la lectura de los archivos");
                } 
                else { 
                    files = new string[filesSelected];

                    IWMPPlaylist playlst = mediaPlayer.playlistCollection.newPlaylist("test");

                    files = searchFiles.FileNames;

                    foreach (string file in files) {
                        path = file;
                        IWMPMedia media = mediaPlayer.newMedia(path);

                        playlst.appendItem(media);

                    }
                    IWMPMedia media2 = mediaPlayer.newMedia(path);
                    mediaPlayer.settings.setMode("loop", true);
                    mediaPlayer.currentPlaylist = playlst;
                    mediaPlayer.Ctlcontrols.play(); 
                    mediaPlayer.settings.volume = trVol.Value;
                }
            }
        
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            mediaPlayer.URL = path;
            mediaPlayer.Ctlcontrols.play();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            mediaPlayer.Ctlcontrols.pause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            mediaPlayer.Ctlcontrols.stop();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            mediaPlayer.Ctlcontrols.previous();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            mediaPlayer.Ctlcontrols.next();
        }

        private void trVol_ValueChanged(object sender, EventArgs e)
        {
            int volMedia = trVol.Value;
            mediaPlayer.settings.volume = volMedia;
        }

        private void SelectPlayItem() {

            mediaPlayer.Ctlcontrols.playItem( mediaPlayer.currentPlaylist.Item[1] );
        }
    }
}
