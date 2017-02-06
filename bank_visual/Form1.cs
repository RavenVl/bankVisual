using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bank_visual
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            DataViewManager dvm = this.bankDataSet.DefaultViewManager;
            dataGrid1.DataSource = dvm;
            dataGrid1.DataMember = "Debetors";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bankDataSet.Payments". При необходимости она может быть перемещена или удалена.
            this.paymentsTableAdapter.Fill(this.bankDataSet.Payments);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bankDataSet.Credits". При необходимости она может быть перемещена или удалена.
            this.creditsTableAdapter.Fill(this.bankDataSet.Credits);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bankDataSet.Debetors". При необходимости она может быть перемещена или удалена.
            this.debetorsTableAdapter.Fill(this.bankDataSet.Debetors);

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            DataRowCollection dr_debetors = this.bankDataSet.Debetors.Rows;
            DataRow dr_debetor = dr_debetors.Find(dataGridView1.CurrentRow.Cells[0].Value);

            DataRow[] dr_credits = dr_debetor.GetChildRows(bankDataSet.Relations["FK_Credits_Debetors"]);
            
            foreach (DataRow dr_credit in dr_credits)
            {
                DataRow[] dr_payments =dr_credit.GetChildRows(bankDataSet.Relations["FK_Payments_Credits"]);
                foreach (DataRow item in dr_payments)
                {
                    item.Delete();
                }
                dr_credit.Delete();
            }
            dr_debetor.Delete();

        }

        private void сохранитьToolStripButton_Click(object sender, EventArgs e)
        {
            this.paymentsTableAdapter.Update(this.bankDataSet.Payments);
            this.creditsTableAdapter.Update(this.bankDataSet.Credits);
            this.debetorsTableAdapter.Update(this.bankDataSet.Debetors);
        }
    }
}
