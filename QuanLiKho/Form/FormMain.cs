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
        // TAB PHÂN QUYỀN
        private void btnPhanQuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddXtraTab(xtraTPPhanQuyen);
            gridControlTTTK.DataSource = con.GetDataTable("select * from tblPhanQuyen");

            //nhat ki
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Phân Quyền',N'Xem','" +
               string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");

        }
        private void btnThemTK_Click(object sender, EventArgs e)
        {

            if (gridView9.OptionsView.NewItemRowPosition == DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None)
            {
                gridView9.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            }
            else
            {
                SqlDataAdapter sql = con.GetCmd("select * from tblPhanQuyen");

                DataTable temp = gridControlTTTK.DataSource as DataTable;

                try
                {
                    sql.Update(temp);

                    XtraMessageBox.Show("                   Đã thiết lập\nNếu thay đổi tài khoản đang hoạt động\n         vui lòng đăng nhập lại", "Thông Báo");
                }
                catch
                {
                    XtraMessageBox.Show("Nhập thiếu");
                    return;
                }
                //nhat ki
                DateTime currentTime = DateTime.Now;
                con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Phân Quyền',N'Sửa','" +
                   string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");
            }
        }
        private void btnSuaTK_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sql = con.GetCmd("select * from tblPhanQuyen");

            DataTable temp = gridControlTTTK.DataSource as DataTable;

            try
            {
                sql.Update(temp);

                XtraMessageBox.Show("                   Đã thiết lập\nNếu thay đổi tài khoản đang hoạt động\n         vui lòng đăng nhập lại", "Thông Báo");
            }
            catch
            {
                XtraMessageBox.Show("Nhập thiếu");
                return;
            }
            //nhat ki
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Phân Quyền',N'Sửa','" +
               string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");
        }
        private void btnXoaTK_Click(object sender, EventArgs e)
        {
            try
            {
                con.ThucThiCauLenhSQL("delete tblPhanQuyen where Username='" + userSelect + "'");

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Erro: " + ex.Message, "Cảnh Báo");
                return;
            }
            XtraMessageBox.Show("Đã xóa", "Thông Báo");
            //nhat ki
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Phân Quyền',N'Xóa','" +
               string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");

            gridControlTTTK.DataSource = con.GetDataTable("select * from tblPhanQuyen");
        }
        private void gridView9_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            userSelect = gridView9.GetRowCellDisplayText(e.RowHandle, "Username").ToString();
        }
    }
}
