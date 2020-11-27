using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.Data.SqlClient;
using DevExpress.XtraEditors;
namespace QuanLiKho
{
    public partial class Them : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private KetNoiCSDL con = new KetNoiCSDL();

        private string stateEvent;
        private string lbNameUser = "Quyền ";
        public Them()
        {
            InitializeComponent();
            lbNameUser += con.GetValue("select name from tblLuuMK where num='1'", 0);

        }
        public Them(string strInit)
        {
            InitializeComponent();
            stateEvent = strInit;

            init(strInit);
            initPQ();
        }
        private void init(string state)
        {
            //ban dau
            btnThem.Enabled = true;
            btnCancel.Enabled = true;

            btnXoa.Enabled = false;
            btnXoaToanBo.Enabled = false;
            btnCapNhat.Enabled = false;
            btnSua.Enabled = false;

            txtMa.ReadOnly = true;
            //thiet lap 
            if (state == "HangHoa")
            {
                lbMa.Text = "Mã Hàng Hóa";
                lbTen.Text = "Tên Hàng Hóa";
                lbGia.Text = "Giá Hàng Hóa";
                //lbSL.Text = "Số Lượng";
                lbNameXoa.Text = "Hàng Hóa";

                lbGia.Location = lbSL.Location;
                txtGia.Location = txtSoLuong.Location;

                lbGhiChu.Dispose();
                txtGhiChu.Dispose();
                lbSL.Dispose();
                txtSoLuong.Dispose();

                //dữ liệu Xóa
                comDonVi.Dispose();
                comKH.Dispose();
                comKho.Dispose();
                comNhom.Dispose();
                comNPP.Dispose();


                /////////////////////////////////////////////////////////////////////
                DataTable temp1 = new DataTable();
                temp1 = con.GetDataTable("select * from tblDonVi");
                gridlookDonVi.Properties.DataSource = temp1;
                gridlookDonVi.Properties.DisplayMember = "DVTen";

                DataTable temp2 = new DataTable();
                temp2 = con.GetDataTable("select * from tblKho");
                gridlookKho.Properties.DataSource = temp2;
                gridlookKho.Properties.DisplayMember = "KTen";

                DataTable temp3 = new DataTable();
                temp3 = con.GetDataTable("select * from tblNPP");
                gridlookNPP.Properties.DataSource = temp3;
                gridlookNPP.Properties.DisplayMember = "NPPTen";

                DataTable temp4 = new DataTable();
                temp4 = con.GetDataTable("select * from tblNhom");
                gridlookNhom.Properties.DataSource = temp4;
                gridlookNhom.Properties.DisplayMember = "NTen";
                //data

                DataTable temp = new DataTable();
                temp = con.GetDataTable("select * from tblHangHoa");

                gridControlDL.DataSource = temp;

                DataTable tempp = new DataTable();
                tempp = con.GetDataTable("select HHMa,HHTen from tblHangHoa");

                comHangHoa.Properties.DataSource = tempp;
                comHangHoa.Properties.DisplayMember = "HHTen";

                //lay ma 

                if (temp.Rows.Count > 0)
                {
                    string tempStr = temp.Rows[temp.Rows.Count - 1][0].ToString();

                    int i = Convert.ToInt32(tempStr.Substring(2));
                    i++;
                    txtMa.Text = "HH" + i.ToString("0000");
                }
                else txtMa.Text = "HH0001";
            }

        }
        private void XtraTabMain_Click(object sender, EventArgs e)
        {
            //thiet lap ban dau
            if (XtraTabMain.SelectedTabPage == LayerThem)
            {
                btnThem.Enabled = true;
                btnCancel.Enabled = true;

                btnXoa.Enabled = false;
                btnXoaToanBo.Enabled = false;
                btnCapNhat.Enabled = false;
                btnSua.Enabled = false;
            }
            else if (XtraTabMain.SelectedTabPage == LayerXoa)
            {
                btnThem.Enabled = false;
                btnCancel.Enabled = false;

                btnXoa.Enabled = true;
                btnXoaToanBo.Enabled = true;
                btnCapNhat.Enabled = false;
                btnSua.Enabled = false;
            }
            else if (XtraTabMain.SelectedTabPage == LayerCapNhat)
            {
                btnThem.Enabled = false;
                btnCancel.Enabled = false;

                btnXoa.Enabled = false;
                btnXoaToanBo.Enabled = false;
                btnCapNhat.Enabled = true;
                btnSua.Enabled = true;
            }
            //=====================================================
            private void btnThem_ItemClick(object sender, ItemClickEventArgs e)
            {
                ThemDL(stateEvent);
                this.Dispose();
            }
            private void ThemDL(string state)
            {
                if (state == "HangHoa")
                {
                    SQL_tblHangHoa temp = new SQL_tblHangHoa();
                    EC_tblHangHoa value = new EC_tblHangHoa();

                    value.HHMa = txtMa.Text;
                    value.HHTen = txtTen.Text;
                    txtSoLuong.Text = "0";
                    value.HHGia = txtGia.Text;
                    value.HHTonHienTai = txtSoLuong.Text;
                    try
                    {
                        value.KMa = con.GetValue("select KMa from tblKho where KTen like N'" + gridlookKho.Text + "'", 0);
                        value.NMa = con.GetValue("select NMa from tblNhom where NTen like N'" + gridlookNhom.Text + "'", 0);
                        value.DVMa = con.GetValue("select DVMa from tblDonVi where DVTen like N'" + gridlookDonVi.Text + "'", 0);
                        value.NPPMa = con.GetValue("select NPPMa from tblNPP where NPPTen like N'" + gridlookNPP.Text + "'", 0);
                    }
                    catch
                    {
                        XtraMessageBox.Show("Nhập thiếu!");
                    }

                    try
                    {
                        temp.ThemDuLieu(value);
                    }
                    catch
                    {
                        XtraMessageBox.Show("Nhập thiếu hoặc Mã bị trùng");
                        return;
                    }

                    XtraMessageBox.Show("Đã Thêm!");
                    DateTime currentTime = DateTime.Now;
                    con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Hàng Hóa',N'Thêm','" +
                        string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser + "')");
                }
            }




    }
    }