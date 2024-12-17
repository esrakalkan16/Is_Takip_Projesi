using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using İs_Takip_Projesi.Entity;

namespace İs_Takip_Projesi.Formlar
{
    public partial class FrmGorevListesi : Form
    {
        public FrmGorevListesi()
        {
            InitializeComponent();
        }
        DbisTakipEntities db = new DbisTakipEntities();

        private void FrmGorevListesi_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = (from x in db.Gorevler
                                       select new
                                       {
                                           x.Açıklama,
                                       }).ToList();


            LblAktifGorevSayisi.Text = db.Gorevler.Where(x => x.Durum == true).Count().ToString();
            LblPasifGorevSayisi.Text = db.Gorevler.Where(x => x.Durum == false).Count().ToString();
            LblToplamDepartman.Text = db.Departmanlar.Count().ToString();   

            chartControl1.Series["Durum"].Points.AddPoint("Aktif Görevler", int.Parse(LblAktifGorevSayisi.Text));
            chartControl1.Series["Durum"].Points.AddPoint("Pasif Görevler", int.Parse(LblPasifGorevSayisi.Text));

        }

       
    }
}
