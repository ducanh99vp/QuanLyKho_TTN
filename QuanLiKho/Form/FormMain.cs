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
    }
}
