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

    }
}
