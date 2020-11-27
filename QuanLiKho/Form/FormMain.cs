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
    }
}
