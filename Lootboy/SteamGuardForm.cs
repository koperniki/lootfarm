using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lootboy
{
    public partial class SteamGuardForm : Form
    {
        public string Code { get; set; }

        public SteamGuardForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Code = tbCode.Text;
        }
    }
}
