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
      
        public Them()
        {
            InitializeComponent();

            
        }
        public Them(string strInit)
        {
            InitializeComponent();
            stateEvent = strInit;

            init(strInit);
            initPQ();
        }

       
        
    }
}