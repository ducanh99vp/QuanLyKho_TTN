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
    public enum state
    {
        StateNK,
        StateXK,
        StateTK
    }
    public partial class FormMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public static state StateManager;
        KetNoiCSDL con = new KetNoiCSDL();

        //dem so luong tab con hien thi
        private int vSoXtraTab = 1;

        //khai bao cac thu tuc xuat kho
        SQL_tblXuatKho sqlXK = new SQL_tblXuatKho();
        EC_tblXuatKho ecXK = new EC_tblXuatKho();



        //khai bao cac thu tuc khach hang
        SQL_tblKhachHang sqlKH = new SQL_tblKhachHang();

        //dang nhap, dang xuat
        private bool active = false;


        //lay dong chon trong bang phan quyen
        private string userSelect;

        //dung cho bo loc bao cao
        private string stateBC;

        //phan quyen
        private bool xoaDL = true;

        //Thoi gian bieu do
        private string start;
        private string end;

        //nhap kho
        private string dateState = "tuan";
        private string cmdTime;

        private string start1;
        private string end1;

        //Xuat kho
        private string cmdTime1;
        private string stateQL;
        public FormMain()
        {
            InitializeComponent();
            //tat tat ca cac xtraTab
            //======================================================================
            for (int i = 0; i < xtraTabMain.TabPages.Count; i++)
            {
                if (xtraTabMain.TabPages[i].Text != "Nhập Kho")
                    xtraTabMain.TabPages.Remove(xtraTabMain.TabPages[i]);
            }
            xtraTabMain.TabPages.Remove(xtraTPTonKho);
            xtraTabMain.TabPages.Remove(BDXK);
            xtraTabMain.TabPages.Remove(xtrapTPBoPhan);
            xtraTabMain.TabPages.Remove(xtraTPNhanVien);
            xtraTabMain.TabPages.Remove(xtraTPPhanQuyen);
            //======================================================================
            init();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {

        }
        private bool SaveChane(string cmd)
        {
            bool res = false;
            SqlDataAdapter sql = con.GetCmd("select * from " + cmd);
            try
            {
                if (cmd == "tblXuatKhoTemp")
                {
                    DataTable temp = gridControlXK.DataSource as DataTable;

                    sql.Update(temp.GetChanges());  //luu tat ca nhung thay doi
                    con.UpdateHangHoa(temp, "XuatKho");
                }
                if (cmd == "tblNhapKhoTemp")
                {
                    DataTable temp = gridControlNK.DataSource as DataTable;

                    sql.Update(temp.GetChanges());  //luu tat ca nhung thay doi
                    con.UpdateHangHoa(temp, "NhapKho");

                }

                res = true;
            }
            catch (Exception e)
            {
                XtraMessageBox.Show(e.Message);
                res = false;
            }
            finally
            {
                con.DongKetNoiMetho();
            }

            return res;
        }

        private void init()
        {
            active = false; //khong dang xuat

            //Nhap kho
            txtMaNPP.Enabled = false;
            txtGhiChuNK.Enabled = false;
            txtDiaChiNK.Enabled = false;
            txtDienThoaiNK.Enabled = false;
            comboBoxEditTenNK.Enabled = false;
            txtMaNK.Enabled = false;
            dateEditNK.Enabled = false;
            txtMSTNK.Enabled = false;
            gridControlNK.Enabled = false;

            btnCancelNK.Enabled = false;
            btnOKNK.Enabled = false;
            btnRefeshNK.Enabled = false;

            //xuat kho
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
            //btnCancelXK.Enabled = false;


            txtMaNK.ReadOnly = true;
            txtMaXK.ReadOnly = true;

            //bo phan
            //btnXoaBP.Enabled = false;
            btnThemBP.Enabled = false;
            btnRefeshBP.Enabled = false;

            //nhan vien
            //btnXoaNV.Enabled = false;
            btnThemNV.Enabled = false;
            btnRefeshNV.Enabled = false;

            //name ủe
            lbNameUser.Text += con.GetValue("select name from tblLuuMK where num='1'", 0);
        }
        //them tab
        private void AddXtraTab(DevExpress.XtraTab.XtraTabPage xtraTabName)
        {

            switch (xtraTabName.Text)
            {
                case "Nhập Kho":
                    StateManager = state.StateNK;
                    break;
                case "Xuất Kho":
                    StateManager = state.StateXK;
                    break;
                case "Tồn Kho":
                    StateManager = state.StateTK;
                    break;
            }

            xtraTabMain.SelectedTabPage = xtraTabName;


            //kiem tra xem tab da co chua
            for (int i = 0; i < xtraTabMain.TabPages.Count; i++)
            {
                if (xtraTabName.Text == xtraTabMain.TabPages[i].Text)
                {
                    return;
                }
            }
            vSoXtraTab++;
            xtraTabMain.TabPages.Add(xtraTabName);
        }

        //xoa tab
        private void xtraTabMain_Close(object sender, EventArgs e)
        {
            DevExpress.XtraTab.XtraTabControl TabControl = (DevExpress.XtraTab.XtraTabControl)sender;

            if (vSoXtraTab != 1)
            {
                xtraTabMain.SelectedTabPageIndex -= 1;
                DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs arg = e as DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs;
                TabControl.TabPages.Remove(arg.Page as DevExpress.XtraTab.XtraTabPage);
                vSoXtraTab--;
            }
            else XtraMessageBox.Show("Bạn không thể tắt tất cả các page!", "Cảnh báo");
        }

    }
}
