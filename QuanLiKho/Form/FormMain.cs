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

        private void btnXoaBP_Click(object sender, EventArgs e)
        {
            if (gridControlBoPhan.Enabled == true)
            {
                string MaBP = gridView10.GetRowCellDisplayText(gridView10.FocusedRowHandle, "BPMa").Trim();

                try
                {
                    con.ThucThiCauLenhSQL("delete from tblBoPhan where BPMa='" + MaBP + "'");
                    XtraMessageBox.Show("Đã xóa");
                    btnRefeshBP_Click(sender, e);

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


        private void btnThemBP_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable temp = gridControlBoPhan.DataSource as DataTable;
                SqlDataAdapter cmd = con.GetCmd("select * from tblBoPhan");

                cmd.Update(temp.GetChanges());

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


            FormMain_Load(sender, e);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBP.Text != "")
            {
                DataTable temp = con.GetDataTable("select * from tblBoPhan" + " where BPMa='" + comboBP.Text + "'");
                gridControlBoPhan.DataSource = temp;
            }

        }

        ///////// TAB DANH MỤC
        // KhachHang
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Them temp = new Them("KhachHang");
            temp.ShowDialog();
            FormMain_Load(sender, e);

            //nhat ki
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Khách Hàng',N'Xem','" +
               string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");
        }

        // Nha Phan Phoi
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Them temp = new Them("NPP");
            temp.ShowDialog();
            FormMain_Load(sender, e);

            //nhat ki
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Nhà Phân Phối',N'Xem','" +
               string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");
        }

        // Kho
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Them temp = new Them("Kho");
            temp.ShowDialog();
            FormMain_Load(sender, e);

            //nhat ki
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Kho Hàng',N'Xem','" +
               string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");
        }

        // Don Vi
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Them temp = new Them("DonVi");
            temp.ShowDialog();
            FormMain_Load(sender, e);

            //nhat ki
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Đơn Vị',N'Xem','" +
               string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");
        }

        // Nhom
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Them temp = new Them("Nhom");
            temp.ShowDialog();
            FormMain_Load(sender, e);

            //nhat ki
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Nhóm',N'Xem','" +
               string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");
        }

        // Hang Hoa
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Them temp = new Them("HangHoa");
            temp.ShowDialog();
            FormMain_Load(sender, e);

            //nhat ki
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Hàng Hóa',N'Xem','" +
               string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");
        }
        // Don Vi
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Them temp = new Them("DonVi");
            temp.ShowDialog();
            FormMain_Load(sender, e);

            //nhat ki
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Đơn Vị',N'Xem','" +
               string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");
        }

        // Nhom
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Them temp = new Them("Nhom");
            temp.ShowDialog();
            FormMain_Load(sender, e);

            //nhat ki
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Nhóm',N'Xem','" +
               string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");
        }

        // Hang Hoa
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Them temp = new Them("HangHoa");
            temp.ShowDialog();
            FormMain_Load(sender, e);

            //nhat ki
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Hàng Hóa',N'Xem','" +

        ///////// TAB HỆ THỐNG
        // Dang Xuat
        private void barBtnDangXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            active = true;

            this.Close();

            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Đăng Xuất',N'Xem','" +
                string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");

            lbNameUser.Text = "Quyền ";
        }
        // Thoat he thong
        private void barBtnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            active = false;
            Application.Exit();
        }
        public bool DangXuat()
        {
            return active; //true la dang xuat, false la thoat
        }
        // Thong tin
        private void barBtnThongTin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Thông Tin',N'Xem','" +
                string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");
        }
        // Doi MK
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoiMK temp = new DoiMK();
            temp.ShowDialog();

            //nhat ki
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Đổi Mật Khẩu',N'Xem','" +

               string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");
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




        // TAB NHẬT KÝ HỆ THỐNG
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //nhat ki
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Nhật Kí HT',N'Xem','" +
               string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");


            AddXtraTab(xtraTPNhatKi);

            gridControlNhatKi.DataSource = con.GetDataTable("select * from tblNhatKi");


        }
        private void btnXoaNhatKi_Click(object sender, EventArgs e)
        {
            con.ThucThiCauLenhSQL("delete from tblNhatKi");

            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Nhật Kí HT',N'Xóa','" +
                string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");

            gridControlNhatKi.DataSource = ("select * from tblNhatKi");
        }






        private void xtraTPNhapKho_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void HHTenEdit_Click_1(object sender, EventArgs e)
        {

        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

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
