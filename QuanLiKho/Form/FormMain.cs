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
    }
}
