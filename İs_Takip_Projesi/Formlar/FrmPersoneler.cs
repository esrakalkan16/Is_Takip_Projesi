using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using İs_Takip_Projesi.Entity;

namespace İs_Takip_Projesi.Formlar
{
    public partial class FrmPersoneler : Form
    {
        public FrmPersoneler()
        {
            InitializeComponent();
        }

        DbisTakipEntities db = new DbisTakipEntities();
        void personeller()
        {
            var degerler = (from x in db.Personel
                            select new
                            {
                                x.ID,
                                x.Ad,
                                x.Soyad,
                                x.Mail,
                                x.Departman,
                            });
            gridControl1.DataSource = degerler.ToList();
        }

        private void FrmPersoneler_Load(object sender, EventArgs e)
        {
         personeller();

            var departmanlar = (from x in db.Departmanlar select new {
                x.ID,
                x.Ad, 
            }).ToList();

            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "Ad";
            lookUpEdit1.Properties.DataSource = departmanlar;   
           
        }
        private void BtnListele_Click(object sender, EventArgs e)
        {
            personeller();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            Personel t = new Personel();
            t.Ad = TxtAd.Text;
            t.Soyad= TxtSoyad.Text;
            t.Mail = TxtMail.Text;
            t.Görsel=TxtGorsel.Text;
            t.Departman=int.Parse (lookUpEdit1.EditValue.ToString());
            db.Personel.Add(t);
            db.SaveChanges();
            XtraMessageBox.Show("Yeni profil kaydı başarılı bir şekilde gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            personeller();

        }
    }
}
