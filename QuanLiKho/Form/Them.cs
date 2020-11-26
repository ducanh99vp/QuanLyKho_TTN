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
        }



    }
    }