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
        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Them temp = new Them("HangHoa");
            temp.ShowDialog();
            FormMain_Load(sender, e);
        }
        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Them temp = new Them("NPP");
            temp.ShowDialog();
            FormMain_Load(sender, e);
        }
        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Them temp = new Them("Kho");
            temp.ShowDialog();
            FormMain_Load(sender, e);
        }
        private void navBarItem8_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Them temp = new Them("HangHoa");
            temp.ShowDialog();
            FormMain_Load(sender, e);
        }
        private void navBarItem9_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Them temp = new Them("KhachHang");
            temp.ShowDialog();
            FormMain_Load(sender, e);
        }
        private void navBarItem10_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Them temp = new Them("Kho");
            temp.ShowDialog();
            FormMain_Load(sender, e);
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

        // TAB TỔNG HỢP KHO
        private void barbtnTHK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddXtraTab(BDXK);
            gridHH.Text = "";
            gridHHXK.Text = "";

            btnNow_Click(sender, e);
            btnNowXK_Click(sender, e);
            //chartTK.DataSource = con.GetDataTable("select HHTen,HHSL from tblHangHoa");

            //nhat ki 
            DateTime currentTime = DateTime.Now;
            con.ThucThiCauLenhSQL("insert into tblNhatKi (NKTen,NKTacVu,NKNgay,NKUser) values (N'Tổng Hợp Kho',N'Xem','" +
                string.Format("{0:yyyy/MM/dd HH:mm:ss}", currentTime) + "',N'" + lbNameUser.Text + "')");
        }
        private void btnNow_Click(object sender, EventArgs e)
        {
            //thu 2 tuan nay
            DateTime tempTime = new DateTime();
            tempTime = DateTime.Now.Date;


            if (dateState == "tuan")
            {
                while (tempTime.DayOfWeek != DayOfWeek.Monday) tempTime = tempTime.AddDays(-1);

                start = string.Format("{0:yyyy/MM/dd}", tempTime);
                end = string.Format("{0:yyyy/MM/dd}", tempTime.AddDays(6));

            }
            if (dateState == "thang")
            {
                while (tempTime.Day > 1) tempTime = tempTime.AddDays(-1);

                start = string.Format("{0:yyyy/MM/dd}", tempTime);
                end = string.Format("{0:yyyy/MM/dd}", tempTime.AddDays(DateTime.DaysInMonth(tempTime.Year, tempTime.Month) - 1));

            }

            if (dateState == "quy")
            {
                if (tempTime.Month < 4)
                {
                    start = tempTime.Year + "/1/1";
                    end = tempTime.Year + "/3/31";
                }
                if (tempTime.Month >= 4 && tempTime.Month < 7)
                {
                    start = tempTime.Year + "/4/1";
                    end = tempTime.Year + "/6/30";
                }
                if (tempTime.Month >= 7 && tempTime.Month < 10)
                {
                    start = tempTime.Year + "/7/1";
                    end = tempTime.Year + "/9/30";
                }
                if (tempTime.Month >= 10 && tempTime.Month < 13)
                {
                    start = tempTime.Year + "/10/1";
                    end = tempTime.Year + "/12/31";
                }
            }

            cmdTime = "select HHTen,NKSL,NKNgay from tblNhapKho as a join tblHangHoa as b on a.HHMa=b.HHMa where NKNgay>= '" + start + "' and NKNgay<= '" + end + "'";
            chartNK.DataSource = con.GetDataTable(cmdTime);

            //MessageBox.Show(start+end);
        }
        private void comboxDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridHH.Text = "";

            if (comboxDate.SelectedIndex == 0)
            {
                dateState = "tuan";
                btnNow.Text = "Tuần Này";
                btnYester.Text = "Tuần Trước";

                btnNow_Click(sender, e);
            }
            if (comboxDate.SelectedIndex == 1)
            {
                dateState = "thang";
                btnNow.Text = "Tháng Này";
                btnYester.Text = "Tháng Trước";

                btnNow_Click(sender, e);
            }
            if (comboxDate.SelectedIndex == 2)
            {
                dateState = "quy";
                btnNow.Text = "Quý Này";
                btnYester.Text = "Quý Trước";

                btnNow_Click(sender, e);
            }
        }

        private void btnYester_Click(object sender, EventArgs e)
        {
            //thu 2 tuan truoc
            DateTime tempTime = new DateTime();
            tempTime = DateTime.Now.Date;
            tempTime = tempTime.AddDays(-7);

            if (dateState == "tuan")
            {
                while (tempTime.DayOfWeek != DayOfWeek.Monday) tempTime = tempTime.AddDays(-1);

                start = string.Format("{0:yyyy/MM/dd}", tempTime);
                end = string.Format("{0:yyyy/MM/dd}", tempTime.AddDays(6));

            }
            if (dateState == "thang")
            {
                tempTime = tempTime.AddMonths(-1);
                while (tempTime.Day > 1) tempTime = tempTime.AddDays(-1);

                start = string.Format("{0:yyyy/MM/dd}", tempTime);
                end = string.Format("{0:yyyy/MM/dd}", tempTime.AddDays(DateTime.DaysInMonth(tempTime.Year, tempTime.Month) - 1));

            }

            if (dateState == "quy")
            {
                tempTime = tempTime.AddMonths(-3);
                if (tempTime.Month < 4)
                {
                    start = tempTime.Year + "/1/1";
                    end = tempTime.Year + "/3/31";
                }
                if (tempTime.Month >= 4 && tempTime.Month < 7)
                {
                    start = tempTime.Year + "/4/1";
                    end = tempTime.Year + "/6/30";
                }
                if (tempTime.Month >= 7 && tempTime.Month < 10)
                {
                    start = tempTime.Year + "/7/1";
                    end = tempTime.Year + "/9/30";
                }
                if (tempTime.Month >= 10 && tempTime.Month < 13)
                {
                    start = tempTime.Year + "/10/1";
                    end = tempTime.Year + "/12/31";
                }
            }

            cmdTime = "select HHTen,NKSL,NKNgay from tblNhapKho as a join tblHangHoa as b on a.HHMa=b.HHMa where NKNgay>= '" + start + "' and NKNgay<= '" + end + "'";
            chartNK.DataSource = con.GetDataTable(cmdTime);

            //MessageBox.Show(start+end);
        }
        private void gridHH_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gridHH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string temp = cmdTime;
            if (gridHH.Text != null)
            {
                temp += " and HHTen=N'" + gridHH.Text + "'";
            }
            else temp = cmdTime;
            chartNK.DataSource = con.GetDataTable(temp);


        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            DateTime temp = new DateTime();
            temp = Convert.ToDateTime(end);

            if (dateState == "tuan")
            {
                temp = temp.AddDays(7);
            }
            if (dateState == "thang")
            {
                temp = temp.AddMonths(1);

            }

            if (dateState == "quy")
            {
                temp = temp.AddMonths(3);
            }

            end = string.Format("{0:yyyy/MM/dd}", temp);
            cmdTime = "select HHTen,NKSL,NKNgay from tblNhapKho as a join tblHangHoa as b on a.HHMa=b.HHMa where NKNgay>= '" + start + "' and NKNgay<= '" + end + "'";
            chartNK.DataSource = con.GetDataTable(cmdTime);
            //MessageBox.Show(start+end);
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            DateTime temp = Convert.ToDateTime(start);

            if (dateState == "tuan")
            {
                temp = temp.AddDays(-7);

            }
            if (dateState == "thang")
            {
                temp = temp.AddMonths(-1);

            }

            if (dateState == "quy")
            {
                temp = temp.AddMonths(-3);
            }

            start = string.Format("{0:yyyy/MM/dd}", temp);
            cmdTime = "select HHTen,NKSL,NKNgay from tblNhapKho as a join tblHangHoa as b on a.HHMa=b.HHMa where NKNgay>= '" + start + "' and NKNgay<= '" + end + "'";
            chartNK.DataSource = con.GetDataTable(cmdTime);
            //MessageBox.Show(start+end);
        }

    }
}
