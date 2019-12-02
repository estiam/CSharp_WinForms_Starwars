using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using StarWarsAPI.Models;

namespace StarWarsAPI
{
    public partial class Form1 : Form
    {
        List<Character> characters = new List<Character>();
        public Form1()
        {
            InitializeComponent();
            lb_characters.Enabled = false;
            tb_characterName.Enabled = false;
        }
        

        private async void Form1_Load(object sender, EventArgs e)
        {
            lb_status.Text = "Loading...";
            WebClient wc = new WebClient();
            byte[] data = await wc.DownloadDataTaskAsync(new Uri("https://swapi.co/api/people/?limit=100"));
            string json = Encoding.UTF8.GetString(data);
            CharacterAPIResult result = JsonConvert.DeserializeObject<CharacterAPIResult>(json);

            lb_characters.DataSource = result.Results;
            characters = result.Results;

            lb_characters.Enabled = true;
            //textBox1.Text = json;
            lb_status.Text = "Finished!";
  
        }

        private void lb_characters_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox temp = (ListBox) sender;

            if (characters.Count > 0)
                tb_characterName.Text = characters[temp.SelectedIndex].Name;
        }
    }
}
