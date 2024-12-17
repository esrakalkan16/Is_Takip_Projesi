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
                                Departman = x.Departmanlar.Ad,
                                x.Durum,
                            });
            gridControl1.DataSource = degerler.Where(x => x.Durum == true).ToList();
        }

        private void FrmPersoneler_Load(object sender, EventArgs e)
        {
            personeller();

            var departmanlar = (from x in db.Departmanlar
                                select new
                                {
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
            t.Soyad = TxtSoyad.Text;
            t.Mail = TxtMail.Text;
            t.Görsel = TxtGorsel.Text;
            t.Departman = int.Parse(lookUpEdit1.EditValue.ToString());
            db.Personel.Add(t);
            db.SaveChanges();
            XtraMessageBox.Show("Yeni profil kaydı başarılı bir şekilde gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            personeller();

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            var x = int.Parse(TxtID.Text);
            var deger =db.Personel.Find(x);
            deger.Durum = false;
            db.SaveChanges();
            XtraMessageBox.Show("Personel başarılı bir şekilde silindi, silinen personeller" +
                " listesinden tüm silinmiş personel bilgilerine ulaşabilirsiniz... ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            personeller() ;
        }

        private void gridView1_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TxtID.Text =gridView1.GetFocusedRowCellValue("ID").ToString();
            TxtAd.Text = gridView1.GetFocusedRowCellValue("Ad").ToString();
            TxtSoyad.Text = gridView1.GetFocusedRowCellValue("Soyad").ToString();
            TxtMail.Text = gridView1.GetFocusedRowCellValue("Mail").ToString();
          //  TxtGorsel.Text = gridView1.GetFocusedRowCellValue("Görsel").ToString();
           lookUpEdit1.Text = gridView1.GetFocusedRowCellValue("Departman").ToString();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int x = int.Parse(TxtID.Text);
            var deger = db.Personel.Find(x);
            deger.Ad = TxtAd.Text;
            deger.Soyad = TxtSoyad.Text;
            deger.Mail = TxtMail.Text;
            deger.Görsel = TxtGorsel.Text;
            deger.Departman = int.Parse( lookUpEdit1.EditValue.ToString());
            db.SaveChanges();
            XtraMessageBox.Show("Personel başarılı bir şekilde güncellendi. ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question);

            personeller();
        }
    }
}
