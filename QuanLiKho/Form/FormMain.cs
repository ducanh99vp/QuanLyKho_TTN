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
        // TAB NHÂN VIÊN
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //nhan vien 
            AddXtraTab(xtraTPNhanVien);
            gridView11.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

            DataTable temp = con.GetDataTable("select * from tblNhanVien");
            gridControlNhanVien.DataSource = temp;

            //nhat ki
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Nhân Viên',N'Xem','" +
               string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");
        }
        private void btnNewNhanVien_Click(object sender, EventArgs e)
        {
            gridView11.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;

            btnThemNV.Enabled = true;
            btnRefeshNV.Enabled = true;
        }
        private void btnRefeshNV_Click(object sender, EventArgs e)
        {
            DataTable temp = con.GetDataTable("select * from tblNhanVien");
            gridControlNhanVien.DataSource = temp;
        }
        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            if (gridControlNhanVien.Enabled == true)
            {
                string MaNV = gridView11.GetRowCellDisplayText(gridView10.FocusedRowHandle, "NVMa").Trim();

                try
                {
                    con.ThucThiCauLenhSQL("delete from tblNhanVien where NVMa='" + MaNV + "'");
                    XtraMessageBox.Show("Đã xóa");
                    btnRefeshNV_Click(sender, e);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Erro: " + ex.Message);
                }

            }
        }
        private void btnThemNV_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable temp = gridControlNhanVien.DataSource as DataTable;
                SqlDataAdapter cmd = con.GetCmd("select * from tblNhanVien");

                cmd.Update(temp.GetChanges());

                XtraMessageBox.Show("Đã thêm", "Thông Báo");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Erro: " + ex.Message);
            }
        }
        private void MBP_Click(object sender, EventArgs e)
        {
            gridView11.SetRowCellValue(gridView11.FocusedRowHandle, "BPMa", con.GetValue("select BPMa from tblBoPhan where BPTen=N'" + comboBP.Text + "'", 0));

            //MessageBox.Show(con.GetValue("select BPMa from tblBoPhan where BPTen=N'" + comboBP.Text + "'",0));
        }
        private void NVMa_Leave(object sender, EventArgs e)
        {
            gridView11.SetRowCellValue(gridView11.FocusedRowHandle, "BPMa", con.GetValue("select BPMa from tblBoPhan where BPTen=N'" + comboBP.Text + "'", 0));

        }

        private void simpleButton14_Click(object sender, EventArgs e)
        {
            ExportToExcel temp = new ExportToExcel();
            temp.exportFile("*.xls", gridControlNhanVien);
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            ExportToExcel temp = new ExportToExcel();
            temp.exportFile("*.pdf", gridControlNhanVien);
        }


    }
}
