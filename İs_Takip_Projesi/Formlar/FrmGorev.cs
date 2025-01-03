﻿using System;
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
    public partial class FrmGorev : Form
    {
        public FrmGorev()
        {
            InitializeComponent();
        }

        private void BtnVazgec_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        DbisTakipEntities db = new DbisTakipEntities();
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            Gorevler t = new Gorevler();
            t.Açıklama = TxtAciklama.Text;
            t.Durum = true;
            t.GörevAlan = int.Parse(lookUpEdit1.EditValue.ToString());
            t.Tarih = DateTime.Parse(TxtTarih.Text);
            t.GörevVeren = int.Parse(TxtGorevVeren.Text);
            db.Gorevler.Add(t);
            db.SaveChanges();
            XtraMessageBox.Show("Görev Başarılı bir şekilde tanımlandı. ",
                "Bilgi ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmGorev_Load(object sender, EventArgs e)
        {
            var degerler = (from x in db.Personel
                                select new
                                {
                                    x.ID,
                                AdSoyad=    x.Ad +" " + x.Soyad
                                }).ToList();

            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "AdSoyad";
            lookUpEdit1.Properties.DataSource = degerler;

        }
    }
}
