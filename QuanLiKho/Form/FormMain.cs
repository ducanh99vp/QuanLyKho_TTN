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

        // TAB XUAT KHO
        private void barBtnXuatKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //them tab
            AddXtraTab(xtraTPXuaKho);



            if (!(txtDiaChiXk.Text == "" || txtMaXK.Text != "" || txtGhiChuXK.Text != "" || txtMSTXK.Text != "" || txtDienThoaiXK.Text != "" || txtMaNK.Text != "")) //dieu kien
            {
                //khoa dieu kien, chi mo khi an new
                gridLookUpEditTenXK.Enabled = false;
                gridLookUpEditMaXK.Enabled = false;
                txtDiaChiXk.Enabled = false;
                txtDienThoaiXK.Enabled = false;
                txtGhiChuXK.Enabled = false;
                txtMaXK.Enabled = false;
                txtMSTXK.Enabled = false;
                dateEditNgayXK.Enabled = false;
                gridControlXK.Enabled = false;

                btnOKXK.Enabled = false;
                btnRefeshXK.Enabled = false;
                btnCancelXK.Enabled = false;


            }

            //nhat ki 
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Xuất Kho',N'Xem','" +
                string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");

        }
        private void btnNewXK_Click(object sender, EventArgs e)
        {
            gridLookUpEditTenXK.Enabled = true;
            gridLookUpEditMaXK.Enabled = true;
            txtDiaChiXk.Enabled = true;
            txtDienThoaiXK.Enabled = true;
            txtGhiChuXK.Enabled = true;
            txtMaXK.Enabled = true;
            txtMSTXK.Enabled = true;
            dateEditNgayXK.Enabled = true;


            btnRefeshXK.Enabled = true;

            gridLookUpEditTenXK.Text = "";
            gridLookUpEditMaXK.Text = "";
            txtDiaChiXk.Text = "";
            txtDienThoaiXK.Text = "";
            txtGhiChuXK.Text = "";
            txtMaXK.Text = "";
            txtMSTXK.Text = "";
            dateEditNgayXK.Text = "";
            gridControlXK.Text = "";

            DataTable temp0 = new DataTable();
            temp0 = con.GetDataTable("select * from tblKhachHang");
            gridLookUpEditTenXK.Properties.DataSource = temp0;

            DataTable temp = new DataTable();
            temp = con.GetDataTable("select * from tblXuatKhoTemp as a join tblHangHoa as b on a.HHMa=b.HHMa");
            gridControlXK.DataSource = temp;

            //lay ma xuat kho
            DataTable temp1 = new DataTable();
            temp1 = con.GetDataTable("select * from tblXuatKho");
            if (temp1.Rows.Count > 0)
            {
                string tempStr = temp1.Rows[temp1.Rows.Count - 1][3].ToString();

                int i = Convert.ToInt32(tempStr.Substring(2));
                i++;
                txtMaXK.Text = "XK" + i.ToString("0000");
            }
            else txtMaXK.Text = "XK0001";

            dateEditNgayXK.Text = DateTime.Today.ToString();

        }
        private void gridLookUpEditTenXK_EditValueChanged(object sender, EventArgs e)
        {
            DataTable temp = new DataTable();
            if (gridLookUpEditTenXK.Text != "")
            {
                try
                {
                    string cmd = "SELECT * FROM tblKhachHang WHERE KHTen Like N'" + gridLookUpEditTenXK.Text + "'";
                    temp = con.GetDataTable(cmd);

                    txtDiaChiXk.Text = temp.Rows[0][2].ToString();
                    txtMSTXK.Text = temp.Rows[0][3].ToString();
                    txtDienThoaiXK.Text = temp.Rows[0][4].ToString();
                    //txtGhiChuXK.Text = temp.Rows[0][4].ToString();
                    gridLookUpEditMaXK.Text = temp.Rows[0][0].ToString();

                    gridControlXK.Enabled = true;

                    gridView6.SetRowCellValue(gridView6.FocusedRowHandle, "KHMa", gridLookUpEditMaXK.Text);

                }
                catch
                {
                    XtraMessageBox.Show("Không có dữ liệu về khách hàng!", "Cảnh báo");
                }
                DataTable temp1 = new DataTable();
                temp1 = con.GetDataTable("select HHMa,HHTen,HHTonHienTai from tblHangHoa");
                repositoryItemGridLookUpEdit1.DataSource = temp1;
                repositoryItemGridLookUpEdit1.DisplayMember = "HHMa";
            }

        }
        private void btnRefeshXK_Click(object sender, EventArgs e)
        {
            DataTable temp = new DataTable();
            temp = con.GetDataTable("select * from tblXuatKhoTemp as a join tblHangHoa as b on a.HHMa=b.HHMa");

            gridControlXK.DataSource = temp;

            //FormMain_Load(sender, e);
            btnCancelXK.Enabled = false;
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Them temp = new Them("KhachHang");
            temp.ShowDialog();
            FormMain_Load(sender, e);
        }
        private void btnOKXK_Click(object sender, EventArgs e)
        {

            if (SaveChane("tblXuatKhoTemp"))
            {
                XtraMessageBox.Show("Đã lưu!", "Thông Báo");
                btnOKXK.Enabled = false;
                if (xoaDL == true) btnCancelXK.Enabled = true;

                con.ThucThiCauLenhSQL("insert into tblXuatKho(HHMa, KMa, XKMa, DVMa, XKSL, XKGia, KHMa, XKNgay, XKThanhTien) select HHMa, KMa, XKMa, DVMa, XKSL, XKGia, KHMa, XKNgay, XKThanhTien from tblXuatKhoTemp");
                con.ThucThiCauLenhSQL("delete from tblXuatKhoTemp");
                //btnRefeshXK_Click(sender, e);
            }

        }
        private void gridView6_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

            if (gridView6.GetRowCellDisplayText(e.RowHandle, "XKMa").ToString() != "")
            {
                txtMaXK.Text = gridView6.GetRowCellDisplayText(e.RowHandle, "XKMa").ToString();
                dateEditNgayXK.EditValue = gridView6.GetRowCellDisplayText(e.RowHandle, "XKNgay").ToString();
            }
            else
            {
                DataTable temp = new DataTable();
                temp = con.GetDataTable("select * from tblXuatKhoTemp as a join tblHangHoa as b on a.HHMa=b.HHMa");

                //lay ma xuat kho

                DataTable temp1 = new DataTable();
                temp1 = con.GetDataTable("select * from tblXuatKho");

                if (temp.Rows.Count > 0)
                {
                    string tempStr = temp1.Rows[temp1.Rows.Count - 1][3].ToString();

                    int i = Convert.ToInt32(tempStr.Substring(2));
                    i++;
                    txtMaXK.Text = "XK" + i.ToString("0000");
                }
                else txtMaXK.Text = "XK0001";
            }


        }
        private void gridView6_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            int SL, Gia;
            SL = Convert.ToInt32(gridView6.GetRowCellValue(gridView6.FocusedRowHandle, "XKSL"));
            Gia = Convert.ToInt32(gridView6.GetRowCellValue(gridView6.FocusedRowHandle, "XKGia"));
            int TT = SL * Gia;
            gridView6.SetRowCellValue(gridView6.FocusedRowHandle, "XKThanhTien", TT.ToString());

            //tu dong them ma xuat kho
            string tempStr = txtMaXK.Text;

            int i = Convert.ToInt32(tempStr.Substring(2));
            i++;
            txtMaXK.Text = "XK" + i.ToString("0000");
        }
        private void repositoryItemGridLookUpEdit1_Leave_1(object sender, EventArgs e)
        {
            string getHHMa = gridView6.GetRowCellDisplayText(gridView6.FocusedRowHandle, "HHMa");
            DataTable temp = new DataTable();
            try
            {
                temp = con.GetDataTable("select * from tblHangHoa where HHMa=N'" + getHHMa + "'");
            }
            catch
            {
                XtraMessageBox.Show("Mã xuất kho chưa đúng");
                return;
            }

            if (temp.Rows.Count != 0)
            {
                gridView6.SetRowCellValue(gridView6.FocusedRowHandle, "HHTen", temp.Rows[0][1].ToString().Trim());
                gridView6.SetRowCellValue(gridView6.FocusedRowHandle, "KMa", temp.Rows[0][4].ToString().Trim());
                gridView6.SetRowCellValue(gridView6.FocusedRowHandle, "DVMa", temp.Rows[0][3].ToString().Trim());
                gridView6.SetRowCellValue(gridView6.FocusedRowHandle, "XKMa", txtMaXK.Text);
                gridView6.SetRowCellValue(gridView6.FocusedRowHandle, "XKNgay", dateEditNgayXK.Text);
                gridView6.SetRowCellValue(gridView6.FocusedRowHandle, "KHMa", gridLookUpEditMaXK.Text);
                txtGhiChuXK.Text = temp.Rows[0][4].ToString().Trim();
            }

            if (txtMaXK.Text != "")
            {
                btnOKXK.Enabled = true;

            }
            else
            {
                btnOKXK.Enabled = false;
            }
        }
        private void XKGiaEdit_Leave(object sender, EventArgs e)
        {
            string gia = gridView6.GetRowCellDisplayText(gridView6.FocusedRowHandle, "XKGia").Trim();
            string soluong = gridView6.GetRowCellDisplayText(gridView6.FocusedRowHandle, "XKSL").Trim();

            try
            {
                string tt = (int.Parse(soluong) * float.Parse(gia)).ToString();
                gridView6.SetRowCellValue(gridView6.FocusedRowHandle, "XKThanhTien", tt);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Erro: " + ex.Message, "Thông Báo");
            }
        }

        private void btnCancelXK_Click(object sender, EventArgs e)
        {
            if (gridControlXK.Enabled == true)
            {
                string MaXK = gridView6.GetRowCellDisplayText(gridView6.FocusedRowHandle, "XKMa").Trim();

                try
                {
                    con.ThucThiCauLenhSQL("delete from tblXuatKho where XKMa='" + MaXK + "'");
                    XtraMessageBox.Show("Đã xóa");
                    //btnRefeshXK_Click(sender, e);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Erro: " + ex.Message);
                }
            }

        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ExportToExcel temp = new ExportToExcel();
            temp.exportFile("*.pdf", gridControlXK);
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            ExportToExcel temp = new ExportToExcel();
            temp.exportFile("*.xls", gridControlXK);
        }
        // TAB TON KHO
        private void barBtnTonKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //them tab
            AddXtraTab(xtraTPTonKho);

            DataTable temp = new DataTable();
            temp = con.GetDataTable("select a.HHMa,a.HHTen,a.HHGia,c.DVTen,b.KTen,e.NPPTen,a.HHTonHienTai,d.NTen,a.HHThanhTien " +
                "from tblHangHoa as a join tblKho as b ON a.KMa=b.KMa join tblDonVi as c on a.DVMa=c.DVMa join tblNhom as d ON a.NMa=d.NMa join tblNPP as e " +
                "on a.NPPMa=e.NPPMa ");
            //temp = con.GetDataTable("select * from tblHangHoa as a join tblKho as b on a.KMa=b.KMa join tblDonVi as c on a.DVMa=c.DVMa join tblNPP as d on a.NPPMa=d.NPPMa "+
            //    "join tblNhom as e on a.NMa=e.NMa");

            gridControlTK.DataSource = temp;

            //cac thanh cong cu
            lbName.Hide();
            gridNhom.Hide();
            gridNPP.Hide();
            gridHangHoa.Hide();
            gridKho.Hide();

            //nhat ki 
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Tồn Kho',N'Xem','" +
                string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");
        }
        private void navBarItem15_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            lbName.Hide();
            gridNhom.Hide();
            gridNPP.Hide();
            gridHangHoa.Hide();
            gridKho.Hide();

            gridView8.ActiveFilterString = null;
        }

        private void navBarGroup5_ItemChanged(object sender, EventArgs e)
        {
            //gridNhom.Hide();
            //gridNPP.Hide();
            //gridHangHoa.Hide();
            //gridKho.Hide();
        }

        private void navBarItem17_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            lbName.Show();
            lbName.Text = "Hàng Hóa";
            gridNhom.Hide();
            gridNPP.Hide();
            gridHangHoa.Show();
            gridKho.Hide();


        }

        private void navBarItem18_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            lbName.Show();
            lbName.Text = "NPP";
            gridNhom.Hide();
            gridNPP.Show();
            gridHangHoa.Hide();
            gridKho.Hide();

            gridNPP.Location = gridHangHoa.Location;
        }

        private void navBarItem19_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            lbName.Show();
            lbName.Text = "Nhóm Hàng";
            gridNhom.Show();
            gridNPP.Hide();
            gridHangHoa.Hide();
            gridKho.Hide();

            gridNhom.Location = gridHangHoa.Location;
        }

        private void navBarItem20_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            lbName.Show();
            lbName.Text = "Kho";
            gridNhom.Hide();
            gridNPP.Hide();
            gridHangHoa.Hide();
            gridKho.Show();

            gridKho.Location = gridHangHoa.Location;
        }
        private void simpleButton9_Click(object sender, EventArgs e)
        {
            ExportToExcel temp = new ExportToExcel();
            temp.exportFile("*.xls", gridControlTK);
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            ExportToExcel temp = new ExportToExcel();
            temp.exportFile("*.pdf", gridControlTK);
        }
        // TAB BAO CAO
        private void barBtnBaoCao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //them tab
            AddXtraTab(xtraTPBaoCao);

            //hien nhap kho
            navBarGroup2_ItemChanged(sender, e);
            gridView12.ActiveFilterString = null;

            //nhat ki 
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Báo Cáo',N'Xem','" +
                string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");
        }
        private void navBarGroup1_ItemChanged(object sender, EventArgs e)
        {
            DataTable temp = new DataTable();
            temp = con.GetDataTable("select * from tblHangHoa");
            gridControlBaoCao.MainView = gridView12;
            gridControlBaoCao.DataSource = temp;

        }
        private void navBarGroup2_ItemChanged(object sender, EventArgs e)
        {
            gridView12.Columns.Clear();

            gridControlBaoCao.DataSource = con.GetDataTable("select a.HHMa,b.HHTen,c.KTen,d.DVTen,a.NKMa,a.NKNgay,a.NKSL,a.NKGia,a.NKThanhTien,e.NPPTen " +
                "from tblNhapKho as a join tblHangHoa as b on a.HHMa=b.HHMa" +
                  " join tblKho as c on a.KMa=c.KMa" +
                " join tblDonVi as d on a.DVMa=d.DVMa join tblNPP as e on a.NPPMa=e.NPPMa");

            stateBC = "NK";

        }
        private void gridHangHoa_EnabledChanged(object sender, EventArgs e)
        {

        }

        private void gridHangHoa_EditValueChanged(object sender, EventArgs e)
        {

            gridView8.ActiveFilterString = "[HHTen] LIKE '" + gridHangHoa.Text.ToString() + "'";
        }

        private void gridNPP_EditValueChanged(object sender, EventArgs e)
        {
            gridView8.ActiveFilterString = "[NPPTen] LIKE '" + gridNPP.Text.ToString() + "'";
        }

        private void gridNhom_EditValueChanged(object sender, EventArgs e)
        {
            gridView8.ActiveFilterString = "[NTen] LIKE '" + gridNhom.Text.ToString() + "'";
        }

        private void gridKho_EditValueChanged(object sender, EventArgs e)
        {
            gridView8.ActiveFilterString = "[KTen] LIKE '" + gridKho.Text.ToString() + "'";
        }
        private void navBarGroup3_ItemChanged(object sender, EventArgs e)
        {
            gridView12.Columns.Clear();

            gridControlBaoCao.DataSource = con.GetDataTable("select a.HHMa,b.HHTen,c.KTen,d.DVTen,a.XKMa,a.XKNgay,a.XKSL,a.XKGia,a.XKThanhTien,e.KHTen " +
                "from tblXuatKho as a join tblHangHoa as b on a.HHMa=b.HHMa" +
                " join tblKho as c on a.KMa=c.KMa" +
                " join tblDonVi as d on a.DVMa=d.DVMa join tblKhachHang as e on a.KHMa=e.KHMa");

            stateBC = "XK";
        }

        private void gridLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (gridView12.ActiveFilterString != null) gridView12.ActiveFilterString += " and ";
            gridView12.ActiveFilterString += "[HHTen] LIKE '" + gridHHBC.Text.ToString() + "'";
        }

        private void dateEditTu_EditValueChanged(object sender, EventArgs e)
        {


        }

        private void navNhapTH_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            dateEditDen.Text = "";
            dateEditTu.Text = "";
            gridHHBC.Text = "";

            gridView12.ActiveFilterString = null;

        }

        private void navXuatTH_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridView12.ActiveFilterString = null;
            dateEditDen.Text = "";
            dateEditTu.Text = "";
            gridHHBC.Text = "";
        }

        private void dateEditDen_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dateEditTu_Leave(object sender, EventArgs e)
        {
            if (gridView12.ActiveFilterString != null) gridView12.ActiveFilterString += " and ";
            if (stateBC == "NK")
            {
                //MessageBox.Show("click");
                if (dateEditTu.Text != "") gridView12.ActiveFilterString += "[NKNgay] >= '" + dateEditTu.Text + "'";
                else gridView12.ActiveFilterString = null;

                if (dateEditDen.Text != "" && dateEditTu.Text != "")
                {
                    if (dateEditTu.DateTime.CompareTo(dateEditDen.DateTime) == 1)
                    {
                        XtraMessageBox.Show("Ngày sau không được nhỏ hơn ngày trước", "Thông Báo");
                        return;
                    }
                    else gridView12.ActiveFilterString += "and [NKngay] <= '" + dateEditDen.Text + "'";
                }
            }

            if (stateBC == "XK")
            {
                if (dateEditTu.Text != "") gridView12.ActiveFilterString += "[XKNgay] >= '" + dateEditTu.Text + "'";
                else gridView12.ActiveFilterString = null;

                if (dateEditDen.Text != "" && dateEditTu.Text != "")
                {
                    if (dateEditTu.DateTime.CompareTo(dateEditDen.DateTime) == 1)
                    {
                        XtraMessageBox.Show("Ngày sau không được nhỏ hơn ngày trước", "Thông Báo");
                        return;
                    }
                    else gridView12.ActiveFilterString += "and [XKngay] <= '" + dateEditDen.Text + "'";
                }
            }
        }

        private void dateEditDen_Leave(object sender, EventArgs e)
        {
            if (gridView12.ActiveFilterString != null) gridView12.ActiveFilterString += " and ";
            if (stateBC == "NK")
            {
                //MessageBox.Show("click");
                if (dateEditDen.Text != "") gridView12.ActiveFilterString += "[NKNgay] <= '" + dateEditDen.Text + "'";
                else gridView12.ActiveFilterString = null;

                if (dateEditTu.Text != "" && dateEditTu.Text != "")
                {
                    if (dateEditTu.DateTime.CompareTo(dateEditDen.DateTime) == 1)
                    {
                        XtraMessageBox.Show("Ngày sau không được nhỏ hơn ngày trước", "Thông Báo");
                        return;
                    }
                    else gridView12.ActiveFilterString += "and [NKngay] >= '" + dateEditTu.Text + "'";
                }
            }

            if (stateBC == "XK")
            {
                if (dateEditDen.Text != "") gridView12.ActiveFilterString += "[XKNgay] <= '" + dateEditDen.Text + "'";
                else gridView12.ActiveFilterString = null;

                if (dateEditTu.Text != "" && dateEditTu.Text != "")
                {
                    if (dateEditTu.DateTime.CompareTo(dateEditDen.DateTime) == 1)
                    {
                        XtraMessageBox.Show("Ngày sau không được nhỏ hơn ngày trước", "Thông Báo");
                        return;
                    }
                    else gridView12.ActiveFilterString += "and [XKngay] >= '" + dateEditTu.Text + "'";
                }
            }
        }



        private void navBarItem6_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddXtraTab(xtraTPBaoCao);
            navBarGroup2_ItemChanged(sender, e);
            navNhapNgay_LinkClicked(sender, e);
        }

        private void navNhapNgay_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridView12.ShowCustomFilterDialog(gridView12.Columns[5]);
        }

        private void navBarItem7_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddXtraTab(xtraTPBaoCao);
            navBarGroup2_ItemChanged(sender, e);
            navNhapHH_LinkClicked(sender, e);
        }

        private void navNhapHH_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridView12.ShowCustomFilterDialog(gridView12.Columns[1]);
        }

        private void navBarItem11_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddXtraTab(xtraTPBaoCao);
            navBarGroup3_ItemChanged(sender, e);
            navXuatNgay_LinkClicked(sender, e);
        }

        private void navXuatNgay_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridView12.ShowCustomFilterDialog(gridView12.Columns[5]);
        }

        private void navBarItem12_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddXtraTab(xtraTPBaoCao);
            navBarGroup3_ItemChanged(sender, e);
            navXuatHH_LinkClicked(sender, e);
        }

        private void navXuatHH_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridView12.ShowCustomFilterDialog(gridView12.Columns[1]);
        }
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            ExportToExcel temp = new ExportToExcel();
            temp.exportFile("*.pdf", gridControlBaoCao);
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            ExportToExcel temp = new ExportToExcel();
            temp.exportFile("*.xls", gridControlBaoCao);
        }
        private void btnThemKK_Click(object sender, EventArgs e)
        {
            DialogResult temp = XtraMessageBox.Show("Bạn có muốn cập nhật lại số lượng Hàng Hóa?", "Thông Báo", MessageBoxButtons.YesNo);
            if (SaveChane("tblKiemKeTemp"))
            {
                XtraMessageBox.Show("Đã lưu!", "Thông Báo");

                con.ThucThiCauLenhSQL("insert into tblKiemKe(KKMa, KKNgay, HHMa, HHTen, NMa, KMa, KKNguoi, KKSL, HHSL) select KKMa, KKNgay, HHMa, HHTen, NMa, KMa, KKNguoi, KKSL, HHSL from tblKiemKeTemp");

                //btnRefeshXK_Click(sender, e);
            }
            if (temp == DialogResult.Yes)
            {
                DataTable temp1 = con.GetDataTable("select * from tblKiemKeTemp");

                for (int i = 0; i < temp1.Rows.Count; i++)
                {
                    //XtraMessageBox.Show(temp1.Rows[i][2].ToString() + temp1.Rows[i][7].ToString());
                    con.ThucThiCauLenhSQL("update tblHangHoa set HHTonHienTai=" + temp1.Rows[i][7].ToString() + " where HHMa='" + temp1.Rows[i][2].ToString() + "'");
                }
            }
            else
            {

            }
            con.ThucThiCauLenhSQL("delete from tblKiemKeTemp");

        }

        private void navBarItem16_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridView12.ActiveFilterString = null;
            dateEditDen.Dispose();
            dateEditTu.Dispose();
            gridHHBC.Text = "";
        }

        private void navBarGroup1_ItemChanged_1(object sender, EventArgs e)
        {
            gridView12.Columns.Clear();

            gridControlBaoCao.DataSource = con.GetDataTable("select * from tblKiemKe");

            stateBC = "KK";
        }
    }
}
