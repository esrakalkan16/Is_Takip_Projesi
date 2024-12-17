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
    public partial class FrmPersonelİstatislik : Form
    {
        public FrmPersonelİstatislik()
        {
            InitializeComponent();
        }

        DbisTakipEntities db = new DbisTakipEntities();
        private void FrmPersonelİstatislik_Load(object sender, EventArgs e)
        {
            LblToplamDepartman.Text = db.Departmanlar.Count().ToString();
            LblToplamFirma.Text = db.Firmalar.Count().ToString();
            LblToplamPersonel.Text = db.Personel.Count().ToString();
            LblAktifis.Text = db.Gorevler.Count(x => x.Durum == true).ToString();
            LblPasifis.Text = db.Gorevler.Count(x => x.Durum == false).ToString();
            LblSonGorev.Text = db.Gorevler.OrderByDescending(x =>x.ID).Select(x => x.Açıklama).FirstOrDefault();
            LblSonGorevDetayı.Text = db.Gorevler.OrderByDescending(x => x.ID).Select(x => x.Tarih).FirstOrDefault().ToString();
            LblSehirSayisi.Text = db.Firmalar.Select(x=>x.İl ).Distinct().Count().ToString();
            LblSektor.Text = db.Firmalar.Select(x => x.Sektör).Distinct().Count().ToString(); 
            DateTime bugun = DateTime.Today;
            LblBugunAcilanGorevler.Text = db.Gorevler.Count(x => x.Tarih== bugun).ToString();
           var d1 = db.Gorevler.GroupBy(x => x.GörevAlan).OrderByDescending
                (z=>z.Count()).Select(y =>y.Key).FirstOrDefault();
            LblAyinPersoneli.Text = db.Personel.Where(x => x.ID == d1).
                Select(y => y.Ad + " "+ y.Soyad).FirstOrDefault().ToString();
            LblAyinDepartmani.Text= db.Departmanlar.
                Where(x => x.ID == db.Personel.
                Where(t => t.ID ==d1).Select(z =>z.Departman).FirstOrDefault()).
                Select(y => y.Ad).FirstOrDefault().ToString();


        }
    }
}
