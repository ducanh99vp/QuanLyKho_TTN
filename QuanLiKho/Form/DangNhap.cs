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
using DevExpress.XtraEditors;




namespace QuanLiKho
{
    
    public partial class DangNhap : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        KetNoiCSDL con = new KetNoiCSDL();

        
        //name ủe
        private string lbNameUser = "Quyền ";

        public DangNhap()
        {
            InitializeComponent();

            

          
        }

      

 
    }
}