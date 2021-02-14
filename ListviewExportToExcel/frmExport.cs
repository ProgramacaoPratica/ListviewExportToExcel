using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace ListviewExportToExcel
{
    public partial class frmExport : Form
    {
        public frmExport()
        {
            InitializeComponent();
        }

        private void frmExport_Load(object sender, EventArgs e)
        {
            //Exibe detalhes
            lvlContatos.View = View.Details;
            //Permite selecionar a linha toda
            lvlContatos.FullRowSelect = true;
            //Exite as linhas no listview
            lvlContatos.GridLines = true;
            //Permite que o usuário edite o texto
            lvlContatos.LabelEdit = true;

            lvlContatos.Columns.Add("Nome", 300, HorizontalAlignment.Left);
            lvlContatos.Columns.Add("Celular", 150, HorizontalAlignment.Right);
            lvlContatos.Columns.Add("E-mail", 300, HorizontalAlignment.Left);

            txtNome.Select();

        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Você deve informar o nome!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!txtCelular.MaskCompleted)
            {
                MessageBox.Show("Você deve informar o celular!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txtEmail.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Você deve informar o e-mail!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Definir os itens da lista
            ListViewItem lvi = new ListViewItem(txtNome.Text.Trim());
            lvi.SubItems.Add(txtCelular.Text);
            lvi.SubItems.Add(txtEmail.Text.Trim());

            //Adicionar o item criado(linha criada) no listview
            lvlContatos.Items.Add(lvi);

            txtNome.Text = string.Empty;
            txtCelular.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Text = string.Empty;
            txtCelular.Text = string.Empty;
            txtEmail.Text = string.Empty;
            lvlContatos.Items.Clear();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(1);
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet) wb.Worksheets[1];
            int linha = 2, coluna = 1;

            ws.Cells[1, 1] = lvlContatos.Columns[0].Text;
            ws.Cells[1, 2] = lvlContatos.Columns[1].Text;
            ws.Cells[1, 3] = lvlContatos.Columns[2].Text;

            foreach (ListViewItem lvi in lvlContatos.Items)
            {
                coluna = 1;
                foreach (ListViewItem.ListViewSubItem lvs in lvi.SubItems)
                {
                    ws.Cells[linha, coluna] = lvs.Text;
                    coluna++;
                }
                linha++;
            }
        }
    }
}
