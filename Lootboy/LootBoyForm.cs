using System;
using System.Windows.Forms;
using LootBoyFarm;
using LootBoyStandart;

namespace Lootboy
{
    public partial class LootBoyForm : Form
    {

        private LootBoyHandler _handler;

        public LootBoyForm()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;

            var handler = new LoginHandler(tbName.Text, tbPass.Text, () =>
            {
                var form = new SteamGuardForm();
                form.ShowDialog(this);
                return form.Code;
            });

            token.Text = handler.Token;

            init();
            button1.Visible = true;

        }


        private void init()
        {
            _handler = new LootBoyHandler(token.Text);

            var gc = _handler.GetGemCount();
            gemLbl.Text = "Кристаллов: " + gc.gem;
            coinLbl.Text = "Монет: " + gc.coin;

        }

        private void TokenBtn_Click(object sender, EventArgs e)
        {
            init();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _handler.Purchases("5c33584337e585001c459491");

            var gc = _handler.GetGemCount();
            gemLbl.Text = "Кристаллов: " + gc.gem;
            coinLbl.Text = "Монет: " + gc.coin;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            openFileDialog.ShowDialog(this);
            _handler?.Cards(openFileDialog.FileName);
            var gc = _handler?.GetGemCount();
            gemLbl.Text = "Кристаллов: " + gc?.gem;
            coinLbl.Text = "Монет: " + gc?.coin;
        }

        private void buyAny_Click(object sender, EventArgs e)
        {
            _handler.Purchases(purTb.Text);

            var gc = _handler.GetGemCount();
            gemLbl.Text = "Кристаллов: " + gc.gem;
            coinLbl.Text = "Монет: " + gc.coin;
        }
    }
}
