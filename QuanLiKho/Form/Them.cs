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
            if (state == "DonVi")
            {
                lbMa.Text = "Mã Đơn vị";
                lbTen.Text = "Tên Đơn vị";
                lbSL.Text = "Ghi chú";
                lbGia.Dispose();
                txtGia.Dispose();
                lbGhiChu.Dispose();
                txtGhiChu.Dispose();
                lbNameXoa.Text = "Đơn Vị";

                grNPP.Enabled = false;
                grKho.Enabled = false;
                grNhom.Enabled = false;
                grDonVi.Enabled = false;

                //dữ liệu Xóa
                comHangHoa.Dispose();
                comKH.Dispose();
                comKho.Dispose();
                comNhom.Dispose();
                comNPP.Dispose();

                comDonVi.Location = comHangHoa.Location;

                //data

                DataTable temp = new DataTable();
                temp = con.GetDataTable("select * from tblDonVi");

                gridControlDL.DataSource = temp;

                DataTable tempp = new DataTable();
                tempp = con.GetDataTable("select DVMa,DVTen from tblDonVi");

                comDonVi.Properties.DataSource = tempp;
                comDonVi.Properties.DisplayMember = "DVTen";


                if (temp.Rows.Count > 0)
                {
                    string tempStr = temp.Rows[temp.Rows.Count - 1][0].ToString();

                    int i = Convert.ToInt32(tempStr.Substring(2));
                    i++;
                    txtMa.Text = "DV" + i.ToString("0000");
                }
                else txtMa.Text = "DV0001";
            }
            if (state == "KhachHang")
            {
                lbMa.Text = "Mã Khách Hàng";
                lbTen.Text = "Tên Khách Hàng";
                lbSL.Text = "Địa chỉ";
                lbGia.Text = "Số Điện Thoại";
                lbGhiChu.Text = "MST";
                lbNameXoa.Text = "Khách Hàng";

                grNPP.Enabled = false;
                grKho.Enabled = false;
                grNhom.Enabled = false;
                grDonVi.Enabled = false;

                //dữ liệu Xóa
                comHangHoa.Dispose();
                comDonVi.Dispose();
                comKho.Dispose();
                comNhom.Dispose();
                comNPP.Dispose();

                comKH.Location = comHangHoa.Location;

                //data

                DataTable temp = new DataTable();
                temp = con.GetDataTable("select * from tblKhachHang");

                gridControlDL.DataSource = temp;

                DataTable tempp = new DataTable();
                tempp = con.GetDataTable("select KHMa,KHTen from tblKhachHang");

                comKH.Properties.DataSource = tempp;
                comHangHoa.Properties.DisplayMember = "KHTen";

                if (temp.Rows.Count > 0)
                {
                    string tempStr = temp.Rows[temp.Rows.Count - 1][0].ToString();

                    int i = Convert.ToInt32(tempStr.Substring(2));
                    i++;
                    txtMa.Text = "KH" + i.ToString("0000");
                }
                else txtMa.Text = "KH0001";
            }
            if (state == "Kho")
            {
                lbTen.Text = "Tên Kho";
                lbMa.Text = "Mã Kho";
                lbSL.Text = "Người Liên Hệ";
                lbGia.Text = "Địa chỉ";
                lbGhiChu.Text = "Ghi chú";
                lbNameXoa.Text = "Kho";

                grNPP.Enabled = false;
                grKho.Enabled = false;
                grNhom.Enabled = false;
                grDonVi.Enabled = false;

                //dữ liệu Xóa
                comHangHoa.Dispose();
                comDonVi.Dispose();
                comKH.Dispose();
                comNhom.Dispose();
                comNPP.Dispose();

                comKho.Location = comHangHoa.Location;

                //data

                DataTable temp = new DataTable();
                temp = con.GetDataTable("select * from tblKho");

                gridControlDL.DataSource = temp;

                DataTable tempp = new DataTable();
                tempp = con.GetDataTable("select KMa,KTen from tblKho");

                comKho.Properties.DataSource = tempp;
                comKho.Properties.DisplayMember = "KTen";


                if (temp.Rows.Count > 0)
                {
                    string tempStr = temp.Rows[temp.Rows.Count - 1][0].ToString();

                    int i = Convert.ToInt32(tempStr.Substring(2));
                    i++;
                    txtMa.Text = "K" + i.ToString("0000");
                }
                else txtMa.Text = "K0001";
            }
            if (state == "Nhom")
            {
                lbMa.Text = "Mã Nhóm";
                lbTen.Text = "Tên Nhóm";
                lbSL.Text = "Ghi Chú";
                lbGia.Dispose();
                lbGhiChu.Dispose();
                txtGhiChu.Dispose();
                txtGia.Dispose();
                lbNameXoa.Text = "Nhóm";

                grNPP.Enabled = false;
                grNhom.Enabled = false;
                grDonVi.Enabled = false;

                //dữ liệu Xóa
                comHangHoa.Dispose();
                comDonVi.Dispose();
                comKH.Dispose();
                comKho.Dispose();
                comNPP.Dispose();

                comNhom.Location = comHangHoa.Location;

                //data

                DataTable temp1 = new DataTable();
                temp1 = con.GetDataTable("select * from tblKho");
                gridlookKho.Properties.DataSource = temp1;
                gridlookKho.Properties.DisplayMember = "KTen";


                DataTable temp = new DataTable();
                temp = con.GetDataTable("select a.NMa,a.NTen,a.NGhiChu,a.KMa,b.KTen,b.KDiaChi,b.KNguoiLienHe,b.KDienThoai from tblNhom as a join tblKho as b on a.KMa=b.KMa");

                gridControlDL.DataSource = temp;

                DataTable tempp = new DataTable();
                tempp = con.GetDataTable("select NMa,NTen from tblNhom");

                comNhom.Properties.DataSource = tempp;
                comNhom.Properties.DisplayMember = "NTen";

                if (temp.Rows.Count > 0)
                {
                    string tempStr = temp.Rows[temp.Rows.Count - 1][0].ToString();

                    int i = Convert.ToInt32(tempStr.Substring(2));
                    i++;
                    txtMa.Text = "N" + i.ToString("0000");
                }
                else txtMa.Text = "N0001";
            }

            if (state == "NPP")
            {
                lbMa.Text = "Mã NPP";
                lbTen.Text = "Tên NPP";
                lbSL.Text = "Địa chỉ";
                lbGia.Text = "Điện thoại";
                lbGhiChu.Text = "MST";
                lbNameXoa.Text = "Nhà Phân Phối";

                grNPP.Enabled = false;
                grKho.Enabled = false;
                grNhom.Enabled = false;
                grDonVi.Enabled = false;

                //dữ liệu Xóa
                comHangHoa.Dispose();
                comDonVi.Dispose();
                comKH.Dispose();
                comNhom.Dispose();
                comKho.Dispose();

                comNPP.Location = comHangHoa.Location;

                //data

                DataTable temp = new DataTable();
                temp = con.GetDataTable("select * from tblNPP");

                gridControlDL.DataSource = temp;

                DataTable tempp = new DataTable();
                tempp = con.GetDataTable("select NPPMa,NPPTen from tblNPP");

                comNPP.Properties.DataSource = tempp;
                comNPP.Properties.DisplayMember = "NPPTen";

                if (temp.Rows.Count > 0)
                {
                    string tempStr = temp.Rows[temp.Rows.Count - 1][0].ToString();

                    int i = Convert.ToInt32(tempStr.Substring(3));
                    i++;
                    txtMa.Text = "NPP" + i.ToString("0000");
                }
                else txtMa.Text = "NPP0001";
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
                else if (state == "DonVi")
                {
                    SQL_tblDonVi temp = new SQL_tblDonVi();
                    EC_tblDonVi value = new EC_tblDonVi();

                    value.DVMa = txtMa.Text;
                    value.DVTen = txtTen.Text;
                    value.DVGhiChu = txtSoLuong.Text;

                    try
                    {
                        temp.ThemDuLieu(value);
                    }
                    catch
                    {
                        XtraMessageBox.Show("Nhập thiếu!");
                        return;
                    }
                    XtraMessageBox.Show("Đã thêm!");
                    DateTime currentTime = DateTime.Now;

                }
                else if (state == "KhachHang")
                {
                    SQL_tblKhachHang temp = new SQL_tblKhachHang();
                    EC_tblKhachHang value = new EC_tblKhachHang();

                    value.KHMa = txtMa.Text;
                    value.KHTen = txtTen.Text;
                    value.KHDiaChi = txtSoLuong.Text;
                    value.KHDienThoai = txtGia.Text;
                    value.KHMaSoThue = txtGhiChu.Text;

                    try
                    {
                        temp.ThemDuLieu(value);

                    }
                    catch
                    {
                        XtraMessageBox.Show("Nhập thiếu!");
                        return;
                    }

                    XtraMessageBox.Show("Đã thêm");

                    DateTime currentTime = DateTime.Now;
                    con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Khách Hàng',N'Thêm','" +
                        string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser + "')");

                }
                else if (state == "Kho")
                {
                    SQL_Kho temp = new SQL_Kho();
                    EC_tblKho value = new EC_tblKho();

                    value.KMa = txtMa.Text;
                    value.KTen = txtTen.Text;
                    value.KNguoiLienHe = value.KNguoiQuanLi = txtSoLuong.Text;
                    value.KDiaChi = txtGia.Text;

                    try
                    {
                        temp.ThemDuLieu(value);
                    }
                    catch
                    {
                        XtraMessageBox.Show("Nhập thiếu!");
                        return;
                    }
                    XtraMessageBox.Show("Đã thêm!");

                    DateTime currentTime = DateTime.Now;
                    con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Kho',N'Thêm','" +
                        string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser + "')");
                }
                else if (state == "Nhom")
                {
                    SQL_Nhom temp = new SQL_Nhom();
                    EC_tblNhom value = new EC_tblNhom();

                    value.NMa = txtMa.Text;
                    value.NTen = txtTen.Text;
                    value.NGhiChu = txtSoLuong.Text;
                    try
                    {
                        value.KMa = con.GetValue("select KMa from tblKho where KTen like N'" + gridlookKho.Text + "'", 0);
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
                        XtraMessageBox.Show("Nhập thiếu");

                        return;
                    }

                    XtraMessageBox.Show("Đã thêm!");

                    DateTime currentTime = DateTime.Now;
                    con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Nhóm',N'Thêm','" +
                        string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser + "')");
                }
                else if (state == "NPP")
                {
                    SQL_NPP temp = new SQL_NPP();
                    EC_tblNPP value = new EC_tblNPP();

                    value.NPPMa = txtMa.Text;
                    value.NPPTen = txtTen.Text;
                    value.NPPDiaChi = txtSoLuong.Text;
                    value.NPPMaSoThue = txtGhiChu.Text;
                    value.DienThoai = txtGia.Text;

                    try
                    {
                        temp.ThemDuLieu(value);
                    }
                    catch
                    {
                        XtraMessageBox.Show("Nhập thiếu!");
                        return;
                    }

                    XtraMessageBox.Show("Đã thêm!");

                    DateTime currentTime = DateTime.Now;
                    con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Nhà Phân Phối',N'Thêm','" +
                        string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser + "')");
                }
            }
            private void btnCancel_ItemClick(object sender, ItemClickEventArgs e)
            {
                txtGhiChu.Text = "";
                txtGia.Text = "";
                txtMa.Text = "";
                txtSoLuong.Text = "";
                txtTen.Text = "";

                gridlookDonVi.EditValue = "";
                gridlookKho.EditValue = "";
                gridlookNhom.EditValue = "";
                gridlookNPP.EditValue = "";
            }
            private void btnXoa_ItemClick(object sender, ItemClickEventArgs e)
            {
                if (comDonVi.Text != "" || comHangHoa.Text != "" || comKH.Text != "" || comKho.Text != "" || comNhom.Text != "" || comNPP.Text != "")
                {
                    if (stateEvent == "HangHoa")
                    {
                        SQL_tblHangHoa temp = new SQL_tblHangHoa();
                        EC_tblHangHoa value = new EC_tblHangHoa();
                        value.HHMa = con.GetValue("select HHMa from tblHangHoa where HHTen like N'" + comHangHoa.Text + "'", 0);

                        temp.XoaDuLieu(value);

                        DateTime currentTime = DateTime.Now;
                        con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Hàng Hóa',N'Xóa','" +
                            string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser + "')");
                    }
                    else if (stateEvent == "DonVi")
                    {
                        SQL_tblDonVi temp = new SQL_tblDonVi();
                        EC_tblDonVi value = new EC_tblDonVi();
                        value.DVTen = comDonVi.Text;

                        temp.XoaDuLieu(value);

                        DateTime currentTime = DateTime.Now;
                        con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Đơn Vị',N'Xóa','" +
                            string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser + "')");
                    }
                    else if (stateEvent == "KhachHang")
                    {
                        SQL_tblKhachHang temp = new SQL_tblKhachHang();
                        EC_tblKhachHang value = new EC_tblKhachHang();
                        value.KHTen = comKH.Text;

                        temp.XoaDuLieu(value);

                        DateTime currentTime = DateTime.Now;
                        con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Khách Hàng',N'Xóa','" +
                            string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser + "')");
                    }
                    else if (stateEvent == "Kho")
                    {
                        SQL_Kho temp = new SQL_Kho();
                        EC_tblKho value = new EC_tblKho();
                        value.KTen = comKho.Text;

                        temp.XoaDuLieu(value);

                        DateTime currentTime = DateTime.Now;
                        con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Kho',N'Xóa','" +
                            string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser + "')");
                    }

                    XtraMessageBox.Show("Đã Xóa!", "Thông báo");
                    Them_Load(sender, e);
                }
                else MessageBox.Show("Chưa chọn", "Thông báo");
            }
            private void btnXoaToanBo_ItemClick(object sender, ItemClickEventArgs e)
            {
                DialogResult LuaChon = XtraMessageBox.Show("Bạn chắc chắn muốn xóa hết dữ liệu?", "Cảnh Báo", MessageBoxButtons.YesNo);

                if (LuaChon == DialogResult.Yes)
                {
                    if (stateEvent == "HangHoa")
                    {
                        con.ThucThiCauLenhSQL("DELETE FROM tblHangHoa");
                        XtraMessageBox.Show("Đã Xóa!", "Thông báo");

                        DateTime currentTime = DateTime.Now;
                        con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Hàng Hóa',N'Xóa Toàn Bộ','" +
                            string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser + "')");
                    }
                    else if (stateEvent == "DonVi")
                    {
                        con.ThucThiCauLenhSQL("DELETE FROM tblDonVi");
                        MessageBox.Show("Đã Xóa!", "Thông báo");

                        DateTime currentTime = DateTime.Now;
                        con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Đơn Vị',N'Xóa Toàn Bộ','" +
                            string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser + "')");
                    }
                    else if (stateEvent == "KhachHang")
                    {
                        con.ThucThiCauLenhSQL("DELETE FROM tblKhachHang");
                        XtraMessageBox.Show("Đã Xóa!", "Thông báo");

                        DateTime currentTime = DateTime.Now;
                        con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Khách Hàng',N'Xóa Toàn Bộ','" +
                            string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser + "')");
                    }
                    else if (stateEvent == "Kho")
                    {
                        con.ThucThiCauLenhSQL("DELETE FROM tblKho");
                        MessageBox.Show("Đã Xóa!", "Thông báo");

                        DateTime currentTime = DateTime.Now;
                        con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Kho',N'Xóa Toàn Bộ','" +
                            string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser + "')");
                    }
                }
                else if (LuaChon == DialogResult.Cancel || LuaChon == DialogResult.No || LuaChon == DialogResult.None)
                {
                    return;
                }

            }
            private void btnSua_ItemClick(object sender, ItemClickEventArgs e)
            {
                gridView6.ResetCursor();
                DialogResult LuaChon = XtraMessageBox.Show("Bạn chắc chắn muốn sửa dữ liệu?", "Cảnh Báo", MessageBoxButtons.YesNo);

                if (LuaChon == DialogResult.Yes)
                {
                    if (stateEvent == "HangHoa")
                    {
                        if (SaveChane("tblHangHoa"))
                        {
                            XtraMessageBox.Show("Đã sửa");
                            btnSua.Enabled = false;

                            DateTime currentTime = DateTime.Now;
                            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Hàng Hóa',N'Cập nhật','" +
                                string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser + "')");
                        }
                        else MessageBox.Show("Nhập thiếu!");

                    }
                    else if (stateEvent == "DonVi")
                    {
                        if (SaveChane("tblDonVi"))
                        {
                            XtraMessageBox.Show("Đã sửa");
                            btnSua.Enabled = false;

                            DateTime currentTime = DateTime.Now;
                            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Đơn Vị',N'Cập nhật','" +
                                string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser + "')");
                        }
                        else MessageBox.Show("Nhập thiếu!");
                    }
                    else if (stateEvent == "KhachHang")
                    {
                        if (SaveChane("tblKhachHang"))
                        {
                            XtraMessageBox.Show("Đã sửa");
                            btnSua.Enabled = false;

                            DateTime currentTime = DateTime.Now;
                            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Khách Hàng',N'Cập nhật','" +
                                string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser + "')");
                        }
                        else MessageBox.Show("Nhập thiếu!");
                    }
                    else if (stateEvent == "Kho")
                    {
                        if (SaveChane("tblKho"))
                        {
                            XtraMessageBox.Show("Đã sửa");
                            btnSua.Enabled = false;

                            DateTime currentTime = DateTime.Now;
                            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Kho',N'Cập nhật','" +
                                string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser + "')");
                        }
                        else MessageBox.Show("Nhập thiếu!");
                    }
                }
                else if (LuaChon == DialogResult.Cancel || LuaChon == DialogResult.No || LuaChon == DialogResult.None)
                {
                    return;
                }
            }
            private void btnCapNhat_ItemClick(object sender, ItemClickEventArgs e)
            {
                if (stateEvent == "HangHoa")
                {
                    gridControlDL.DataSource = con.GetDataTable("select * from tblHangHoa");
                }
                else if (stateEvent == "DonVi")
                {
                    gridControlDL.DataSource = con.GetDataTable("select * from tblDonVi");
                }
                else if (stateEvent == "KhachHang")
                {
                    gridControlDL.DataSource = con.GetDataTable("select * from tblKhachHang");
                }
                else if (stateEvent == "Kho")
                {
                    gridControlDL.DataSource = con.GetDataTable("select * from tblKho");
                }

                btnSua.Enabled = true;
            }