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
    public partial class FrmDepartmanlar : Form
    {
        public FrmDepartmanlar()
        {
            InitializeComponent();
        }
        DbisTakipEntities db = new DbisTakipEntities();
        void Listele()
        {

            var degerler = (from x in db.Departmanlar
                            select new
                            {
                                x.ID,
                                x.Ad
                            }).ToList();

            gridControl1.DataSource = degerler;
        }


        private void BtnListele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            Departmanlar t = new Departmanlar();
            t.Ad = TxtAd.Text;
            db.Departmanlar.Add(t);
            db.SaveChanges();
            XtraMessageBox.Show("Departman başarılı bir şekilde sisteme kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            int x = int.Parse(TxtID.Text);
            var deger = db.Departmanlar.Find(x);
            db.Departmanlar.Remove(deger);
            db.SaveChanges();
            XtraMessageBox.Show("Departman silme işi başarıyla gerçekleşti.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            Listele();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int x = int.Parse(TxtID.Text);
            var deger = db.Departmanlar.Find(x);
            deger.Ad = TxtAd.Text;
            db.SaveChanges();
            XtraMessageBox.Show("Departman güncelleme işi başarıyla gerçekleşti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Listele();
        }
    }
}
