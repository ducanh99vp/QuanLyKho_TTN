using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Forms.Layout;
using DevExpress.XtraEditors;
using System.Data;
using System.Threading;
using System.Data.SqlClient;
using DevExpress.Charts;
using System.Configuration;

namespace QuanLiKho
{
    public partial class FormMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FormMain()
        {
            InitializeComponent();        
        }
        // TAB BỘ PHẬN
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //bo phan

            AddXtraTab(xtrapTPBoPhan);
            gridView10.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

            DataTable temp = con.GetDataTable("select * from tblBoPhan");
            gridControlBoPhan.DataSource = temp;

            //nhat ki
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Bộ Phận',N'Xem','" +
               string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");
        }
        private void btnNewBP_Click(object sender, EventArgs e)
        {
            gridView10.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;

            btnThemBP.Enabled = true;
            btnRefeshBP.Enabled = true;
        }

        private void btnRefeshBP_Click(object sender, EventArgs e)
        {
            DataTable temp = con.GetDataTable("select * from tblBoPhan");
            gridControlBoPhan.DataSource = temp;
        }


    }
}
