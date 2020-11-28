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

        ///////// TAB KHO HANG
        // TAB NHAP KHO
        private void barBtnNhapKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //them tab
            AddXtraTab(xtraTPNhapKho);

            if (!(txtMaNK.Text == "" || txtDienThoaiNK.Text == "" || comboBoxEditTenNK.Text == "" || txtMaXK.Text == "" || txtDiaChiNK.Text == ""))
            {
                txtMaNPP.Enabled = false;
                txtGhiChuNK.Enabled = false;
                txtDiaChiNK.Enabled = false;
                txtDienThoaiNK.Enabled = false;
                comboBoxEditTenNK.Enabled = false;
                txtMaNK.Enabled = false;
                dateEditNK.Enabled = false;
                txtMSTNK.Enabled = false;

                btnCancelNK.Enabled = false;
                btnOKNK.Enabled = false;
                btnRefeshNK.Enabled = false;
            }


            //nhat ki 
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Nhập Kho',N'Xem','" +
                string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");

            //MessageBox.Show(string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime));
        }
        private void btnNewNK_Click(object sender, EventArgs e)
        {
            txtMaNPP.Enabled = true;
            txtGhiChuNK.Enabled = true;
            txtDiaChiNK.Enabled = true;
            txtDienThoaiNK.Enabled = true;
            comboBoxEditTenNK.Enabled = true;
            txtMaNK.Enabled = true;
            dateEditNK.Enabled = true;
            txtMSTNK.Enabled = true;

            btnRefeshNK.Enabled = true;

            txtMaNPP.Text = "";
            txtGhiChuNK.Text = "";
            txtDiaChiNK.Text = "";
            txtDienThoaiNK.Text = "";
            txtMaNK.Text = "";
            txtMSTNK.Text = "";
            comboBoxEditTenNK.Text = "";

            DataTable temp0 = new DataTable();
            temp0 = con.GetDataTable("select * from tblNPP");
            comboBoxEditTenNK.Properties.DataSource = temp0;


            DataTable temp = new DataTable();
            temp = con.GetDataTable("select * from tblNhapKhoTemp as a join tblHangHoa as b on a.HHMa=b.HHMa");
            gridControlNK.DataSource = temp;

            DataTable temp1 = new DataTable();
            temp1 = con.GetDataTable("select * from tblNhapKho");
            if (temp1.Rows.Count > 0)
            {
                //lay ma nhap kho
                string tempStr = temp1.Rows[temp1.Rows.Count - 1][3].ToString();

                int i = Convert.ToInt32(tempStr.Substring(2));
                i++;
                txtMaNK.Text = "NK" + i.ToString("0000");


            }
            else txtMaNK.Text = "NK0001";

            dateEditNK.Text = DateTime.Today.ToString();
        }
        private void comboBoxEditTenNK_EditValueChanged(object sender, EventArgs e)
        {
            DataTable temp = new DataTable();
            if (comboBoxEditTenNK.Text != "")
            {
                try
                {
                    string cmd = "SELECT * FROM tblNPP WHERE NPPTen Like N'" + comboBoxEditTenNK.Text + "'";
                    temp = con.GetDataTable(cmd);

                    txtDiaChiNK.Text = temp.Rows[0][2].ToString();
                    txtMSTNK.Text = temp.Rows[0][3].ToString();
                    txtDienThoaiNK.Text = temp.Rows[0][4].ToString();
                    txtGhiChuXK.Text = temp.Rows[0][5].ToString();
                    txtMaNPP.Text = temp.Rows[0][0].ToString();


                    gridControlNK.Enabled = true;
                    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "NPPMa", txtMaNPP.Text);

                    DataTable temp1 = new DataTable();
                    temp1 = con.GetDataTable("select HHMa,HHTen from tblHangHoa where NPPMa='" + txtMaNPP.Text + "'");
                    repositoryItemGridLookUpEdit2.DataSource = temp1;
                    repositoryItemGridLookUpEdit2.DisplayMember = "HHMa";
                }
                catch
                {
                    XtraMessageBox.Show("Không có dữ liệu về nhà phân phối!", "Cảnh báo");
                }
            }

        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Them temp = new Them("NPP");
            temp.ShowDialog();
            FormMain_Load(sender, e);


        }
        private void btnOKNK_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveChane("tblNhapKhoTemp"))
                {
                    XtraMessageBox.Show("Đã lưu!", "Thông Báo");
                    btnOKNK.Enabled = false;
                    if (xoaDL == true) btnCancelNK.Enabled = true;

                    con.ThucThiCauLenhSQL("insert into tblNhapKho(HHMa, KMa, DVMa, NKMa, NKNgay, NKSL, NKGia, NKThanhTien, NPPMa) select HHMa, KMa, DVMa, NKMa, NKNgay, NKSL, NKGia, NKThanhTien, NPPMa from tblNhapKhoTemp");
                    con.ThucThiCauLenhSQL("delete from tblNhapKhoTemp");
                    //btnRefeshNK_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Erro: " + ex.Message);
            }

        }
        private void btnRefeshNK_Click(object sender, EventArgs e)
        {
            DataTable temp = new DataTable();
            temp = con.GetDataTable("select * from tblNhapKhoTemp as a join tblHangHoa as b on a.HHMa=b.HHMa");

            gridControlNK.DataSource = temp;


            //FormMain_Load(sender, e);
            btnCancelNK.Enabled = false;
        }
        private void comboBoxEditTenNK_Enter(object sender, EventArgs e)
        {
            comboBoxEditTenNK_EditValueChanged(sender, e);
        }
        private void gridControlNK_Click(object sender, EventArgs e)
        {

        }
        private void gridView2_RowClick_1(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridView2.GetRowCellDisplayText(e.RowHandle, "NKMa").ToString() != "")
            {
                txtMaNK.Text = gridView2.GetRowCellDisplayText(e.RowHandle, "NKMa").ToString();
                dateEditNK.EditValue = gridView2.GetRowCellDisplayText(e.RowHandle, "NKNgay").ToString();
            }
            else
            {
                DataTable temp = new DataTable();
                temp = con.GetDataTable("select * from tblNhapKhoTemp as a join tblHangHoa as b on a.HHMa=b.HHMa");

                DataTable temp1 = new DataTable();
                temp1 = con.GetDataTable("select * from tblNhapKho");
                if (temp1.Rows.Count > 0)
                {
                    //lay ma nhap kho
                    string tempStr = temp1.Rows[temp1.Rows.Count - 1][3].ToString();

                    int i = Convert.ToInt32(tempStr.Substring(2));
                    i++;
                    txtMaNK.Text = "NK" + i.ToString("0000");
                }
                else txtMaNK.Text = "NK0001";
            }

        }
        private void repositoryItemGridLookUpEdit2_Leave_1(object sender, EventArgs e)
        {
            string getHHMa = gridView2.GetRowCellDisplayText(gridView2.FocusedRowHandle, "HHMa");
            DataTable temp = new DataTable();
            try
            {
                temp = con.GetDataTable("select * from tblHangHoa where HHMa=N'" + getHHMa + "'");
            }
            catch
            {
                XtraMessageBox.Show("Mã nhập kho chưa đúng");
                return;
            }

            if (temp.Rows.Count != 0)
            {
                gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "HHTen", temp.Rows[0][1].ToString().Trim());
                gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "NKMa", txtMaNK.Text);
                gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "NKNgay", dateEditNK.Text);
                gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "NPPMa", txtMaNPP.Text);
                gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "KMa", temp.Rows[0][4].ToString().Trim());
                gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "DVMa", temp.Rows[0][3].ToString().Trim());

            }

            if (txtMaNK.Text != "")
            {
                btnOKNK.Enabled = true;

            }
            else btnOKNK.Enabled = false;
        }
        private void gridView2_ValidateRow_1(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            int SL, Gia;
            SL = Convert.ToInt32(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "NKSL"));
            Gia = Convert.ToInt32(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "NKGia"));
            int TT = SL * Gia;
            gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "NKThanhTien", TT.ToString());

            //lay ma nhap kho
            string tempStr = txtMaNK.Text;

            int i = Convert.ToInt32(tempStr.Substring(2));
            i++;
            txtMaNK.Text = "NK" + i.ToString("0000");
        }
        private void NKGiaEdit_Leave(object sender, EventArgs e)
        {
            string gia = gridView2.GetRowCellDisplayText(gridView2.FocusedRowHandle, "NKGia").Trim();
            string soluong = gridView2.GetRowCellDisplayText(gridView2.FocusedRowHandle, "NKSL").Trim();

            if (gia != "" && soluong != "")
            {
                try
                {
                    string tt = (Convert.ToInt32(gia) * Convert.ToInt32(soluong)).ToString();
                    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "NKThanhTien", tt);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Erro: " + ex.Message, "Thông Báo");
                }
            }
        }
        private void btnCancelNK_Click(object sender, EventArgs e)
        {
            if (gridControlNK.Enabled == true)
            {
                string MaNK = gridView2.GetRowCellDisplayText(gridView2.FocusedRowHandle, "NKMa").Trim();

                try
                {
                    con.ThucThiCauLenhSQL("delete from tblNhapKho where NKMa='" + MaNK + "'");
                    XtraMessageBox.Show("Đã xóa");
                    //btnRefeshNK_Click(sender, e);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Erro: " + ex.Message);
                }

            }

        }
        private void btnPrintNK_Click(object sender, EventArgs e)
        {
            ExportToExcel temp = new ExportToExcel();
            temp.exportFile("*.xls", gridControlNK);
        }
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            ExportToExcel temp = new ExportToExcel();
            temp.exportFile("*.pdf", gridControlNK);

        }

    }
}
