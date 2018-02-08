using LyroundMVCIntegrationMert.Models;
using LyroundMVCIntegrationMert.Models.Managers;
using LyroundMVCIntegrationMert.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LyroundMVCIntegrationMert.Controllers
{
    public class HomeController : Controller
    {
        public static string durum="";
     
        [HttpPost]
        public ActionResult ProfilDuzenle(SarkiEkleViewModel model)
        {
            string sarkiYolu = "";
            if (model.ImageFile != null) {
            string dosyaAdi = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
            string extensions = Path.GetExtension(model.ImageFile.FileName);
            dosyaAdi = dosyaAdi + extensions;

            sarkiYolu = "/UploadImages/" + dosyaAdi;
            dosyaAdi = Path.Combine(Server.MapPath("/UploadImages/"), dosyaAdi);
            model.ImageFile.SaveAs(dosyaAdi);

            }

            string kullaniciAdiCek = Session["kullaniciAdi"].ToString();
           
           
            using (var db = new DatabaseContext())
            {
                Uye u = db.Uyeler.Where(x => x.KullaniciAdi == kullaniciAdiCek).FirstOrDefault();
                u.Ad = model.Uyeler.Ad;
                u.Soyad = model.Uyeler.Soyad;
                u.Sifre = model.Uyeler.Sifre;

                db.Entry(u).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                UyeResim uyeResim = db.UyeResimleri.Where(x => x.UyeId == u.UyeId).FirstOrDefault();

                if(uyeResim != null && model.ImageFile!=null) {
                    uyeResim.UyeResimYolu = sarkiYolu;
                    db.Entry(uyeResim).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else if(uyeResim == null && model.ImageFile != null)
                {
                    UyeResim uyeResim2 = new UyeResim();
                    uyeResim2.UyeId = u.UyeId;
                    uyeResim2.UyeResimYolu = sarkiYolu;
                    db.UyeResimleri.Add(uyeResim2);
                    db.SaveChanges();
                }
               

            }
            return RedirectToAction("ProfilDuzenle");
        }


        public ActionResult ProfilDuzenle()
        {
            SarkiEkleViewModel profil = new SarkiEkleViewModel();
            using (var db = new DatabaseContext())
            {
                string kullaniciAdiCek = Session["kullaniciAdi"].ToString();
                Uye u = db.Uyeler.Where(x => x.KullaniciAdi == kullaniciAdiCek).FirstOrDefault();
                profil.Uyeler = u;
                profil.UyeResim = db.UyeResimleri.Where(x => x.UyeId == u.UyeId).FirstOrDefault();
            }
            return View(profil);
        }

        public ActionResult Profilim()
        {
            SarkiEkleViewModel profil = new SarkiEkleViewModel();
            using (var db = new DatabaseContext())
            {
                string kullaniciAdiCek = Session["kullaniciAdi"].ToString();
                Uye u = db.Uyeler.Where(x => x.KullaniciAdi == kullaniciAdiCek).FirstOrDefault();
                profil.Uyeler = u;
                profil.ArkadasList = db.Arkadaslar.Where(x => x.UyeId == u.UyeId).ToList();
                profil.PaylasilanTextList = db.PaylasilanTextler.Where(x => x.UyeId == u.UyeId).ToList();
               
            }
            return View(profil);
        }


        [HttpPost]
        public ActionResult Profil(int id,SarkiEkleViewModel model)
        {
            DateTime date = DateTime.Now;
            using (var db = new DatabaseContext())
            {
                var kullaniciAdi = Session["kullaniciAdi"].ToString();
                Uye u = db.Uyeler.Where(x => x.KullaniciAdi == kullaniciAdi).FirstOrDefault();
                Arkadas arkadas = new Arkadas();
                arkadas.UyeId = u.UyeId;
                arkadas.UyeArkadasId = id;
                arkadas.ArkadaslikTarihi = date;
                db.Arkadaslar.Add(arkadas);
                db.SaveChanges();
                
            }
            return RedirectToAction("Profil");
        }


        public ActionResult Profil(int id)
        {
            SarkiEkleViewModel sarki = new SarkiEkleViewModel();
            using (var db = new DatabaseContext())
            {
                Uye u = db.Uyeler.Where(x => x.UyeId == id).FirstOrDefault();
                sarki.Uyeler = u;
                sarki.ArkadasList  = db.Arkadaslar.Where(x => x.UyeId == u.UyeId).ToList();
                sarki.PaylasilanTextList = db.PaylasilanTextler.Where(x => x.UyeId == u.UyeId).ToList();
            }
            return View(sarki);
        }





        public ActionResult CikisYap()
        {
            Session.Clear();
            return RedirectToAction("GirisYap");
        }

        public ActionResult Welcome()
        {
            DatabaseContext dbContext = new DatabaseContext();
            if (Session["kullaniciAdi"] != null)
            {
                return RedirectToAction("GirisYap");
            }

            else {
                //Database yaratılıyor. Eğer oluşturulmuşsa database hiç bir iş yapmaz.

                return View();
            }


        }


        public ActionResult GirisYap()
        {
            if (Session["kullaniciAdi"] != null)
            {
                return RedirectToAction("HomePage");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult GirisYap(GirisYapViewModel model)
        {
            if (ModelState.IsValid) { 
            using (var context = new DatabaseContext())
            {
                var query = context.Uyeler.Where(x => x.KullaniciAdi == model.KullaniciAdi && x.Sifre == model.Sifre).ToList();
                foreach (var item in query)
                {
                    Session["kullaniciAdi"] = model.KullaniciAdi;
                    return RedirectToAction("HomePage");
                }
                ViewData["Error"] = "Kullanıcı adı ve şifre kontrol ediniz.";
            }
            }
            return View();
        }


        [HttpPost]
        public ActionResult KayitOl(UyeOlViewModel model)
        {
            if (ModelState.IsValid) { 
            DatabaseContext dbContext = new DatabaseContext();
            DateTime suAnkiTarih = DateTime.Now;
            Uye uye = new Uye(model.KullaniciAdi, model.Sifre, model.EMail, model.Ad, model.Soyad,
                suAnkiTarih, 1);


            dbContext.Uyeler.Add(uye);
            dbContext.SaveChanges();
            return RedirectToAction("HomePage");
            }
            return View();
        }


        public ActionResult KayitOl()
        {

            return View(new UyeOlViewModel());
        }



        public ActionResult HomePage()
        {
            if (Session["kullaniciAdi"] != null) { 
            using (var db = new DatabaseContext())
            {
                    SarkiEkleViewModel sarki = new SarkiEkleViewModel();
                    if (Session["durum"] == null)
                    {
                        sarki.SarkilarList = db.Sarkilar.ToList();
                    }
                    else
                    {

                    
                    if (Session["durum"].ToString()== "Tamamlanmis")
                    {
                        sarki.SarkilarList = db.Sarkilar.Where(x => x.TamamlandiMi == true).ToList();
                    }
                    else if (Session["durum"].ToString() == "Tamamlanmamis")
                    {
                        sarki.SarkilarList = db.Sarkilar.Where(x => x.TamamlandiMi == false).ToList();
                    }
                    else
                    {
                        sarki.SarkilarList = db.Sarkilar.ToList();
                    }

                    }
                    return View(sarki);
            }
            }
            else
            {
                return RedirectToAction("GirisYap");
            }
        }

        public ActionResult Hepsi()
        {
            Session["durum"] = "Hepsi";
            return RedirectToAction("HomePage");
        }

        public ActionResult Tamamlanmis()
        {
            Session["durum"]  = "Tamamlanmis";
            return RedirectToAction("HomePage");
        }

        public ActionResult Tamamlanmamis()
        {
            Session["durum"] = "Tamamlanmamis";
            return RedirectToAction("HomePage");
        }
        

        [HttpPost]
        public ActionResult ArkadasAra(SarkiEkleViewModel model)
        {
            SarkiEkleViewModel m = new SarkiEkleViewModel();
            using (var db = new DatabaseContext())
            {
                m.UyelerList= db.Uyeler.Where(x => ((x.Ad.StartsWith(model.Uyeler.Ad)) || (x.Soyad.StartsWith(model.Uyeler.Ad))
                || x.EMail.StartsWith(model.Uyeler.Ad))).ToList();
            }
            TempData["Uyeler"] = m;
            return RedirectToAction("ArkadasGoster","Home",m);
        }

        public ActionResult ArkadasGoster(SarkiEkleViewModel model)
        {
            var errMsg = TempData["Uyeler"] as SarkiEkleViewModel;
            return View(errMsg);
        }



        [HttpPost]
        public ActionResult HomePage(SarkiEkleViewModel model)
        {
            

            string dosyaAdi = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
            string extensions = Path.GetExtension(model.ImageFile.FileName);
            dosyaAdi = dosyaAdi + extensions;

            string sarkiYolu = "/UploadImages/" + dosyaAdi;
            dosyaAdi = Path.Combine(Server.MapPath("/UploadImages/"), dosyaAdi);
            model.ImageFile.SaveAs(dosyaAdi);

            SarkiResim sarkiResmi = new SarkiResim();
            sarkiResmi.SarkiResimYolu = sarkiYolu;
            
            //sarkiModeli.ImageFile = model.ImageFile;
            //sarkiModeli.HashTagler = model.HashTagler;
            //sarkiModeli.PaylasilanTextler = model.PaylasilanTextler;

            //sarkiModeli.Sarkilar.Baslik = model.Sarkilar.Baslik;

            //sarkiModeli.SarkiResimler.SarkiId = sarkiModeli.Sarkilar.SarkiId;
          

            // if(ModelState.IsValid)
            // {
            DateTime suAnkiTarih = DateTime.Now;
            model.Sarkilar.TamamlandiMi = false;
            model.Sarkilar.OlusturulmaTarihi = suAnkiTarih;
                
            using (var context = new DatabaseContext())
                {
                var kullaniciAdi = Session["kullaniciAdi"].ToString();
                var query = context.Uyeler.Where(x => x.KullaniciAdi == kullaniciAdi).ToList();
                foreach (var item in query)
                {
                    model.Sarkilar.UyeId = item.UyeId;
                }
                try
                    {
                        context.Sarkilar.Add(model.Sarkilar);
                        context.SaveChanges();
                    sarkiResmi.SarkiId = model.Sarkilar.SarkiId;
                    context.SarkiResimler.Add(sarkiResmi);
                    context.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Response.Write(string.Format("Entity türü \"{0}\" şu hatalara sahip \"{1}\" Geçerlilik hataları:", eve.Entry.Entity.GetType().Name, eve.Entry.State));
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Response.Write(string.Format("- Özellik: \"{0}\", Hata: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                            }
                            Response.End();
                        }
                    }
                }

                model.HashTagler.SarkiId = model.Sarkilar.SarkiId;
           
                String[] hashTagDizisi = model.HashTagler.HashTagIcerik.ToString().Split('#');
                using (var context = new DatabaseContext())
                {
                    for (int i = 0; i < hashTagDizisi.Length - 1; i++)
                    {
                    model.HashTagler.HashTagIcerik = hashTagDizisi[i + 1];
                        context.HashTagler.Add(model.HashTagler);
                        context.SaveChanges();
                    }
                model.PaylasilanTextler.UyeId = model.Sarkilar.UyeId;
                model.PaylasilanTextler.SarkiId = model.Sarkilar.SarkiId;
                    context.PaylasilanTextler.Add(model.PaylasilanTextler);
                    context.SaveChanges();
                    
                    //Buraya paylasılan text e alanları giricem gelince halledicem
                }
           // }
            
            return RedirectToAction("HomePage");
        }



        public int HashTagIdDondur(HashTag hashTag)
        {
            int id = 0;
            using (var context = new DatabaseContext())
            {
                var query = (from p in context.HashTagler
                             where p.HashTagIcerik == hashTag.HashTagIcerik
                             select p).ToList();
                foreach (var professor in query)
                {
                    id = professor.HashTagId;
                }
            }
            return id;
        }


        public ActionResult SarkiDetayi()
        {
            return View();
        }
        

        [HttpPost]
        public ActionResult SarkiyaEklemeIslemiYap(int id,SarkiEkleViewModel model)
        {

            String kullaniciAdiCek;
            if (model.PaylasilanTextler!= null) {
                if (model.PaylasilanTextler.TextIcerik != null) { 
                  
            using (var db = new DatabaseContext())
            {
                model.PaylasilanTextler.SarkiId = id;
                kullaniciAdiCek= Session["kullaniciAdi"].ToString();
                Uye u= db.Uyeler.Where(x =>x.KullaniciAdi== kullaniciAdiCek).FirstOrDefault();
                model.PaylasilanTextler.UyeId = u.UyeId ;
                db.PaylasilanTextler.Add(model.PaylasilanTextler);
                db.SaveChanges();

                if (db.PaylasilanTextler.Where(x => x.SarkiId == id).Count() >= 10)
                {
                   Sarki sarki = db.Sarkilar.Where(x => x.SarkiId == id).FirstOrDefault();
                   sarki.TamamlandiMi = true;
                   db.Entry(sarki).State = System.Data.Entity.EntityState.Modified;
                   db.SaveChanges();
               }

            }
                }
            }
            if(model.Yorum!=null)
            {
                if (model.Yorum.YorumIcerik != null) { 
                using (var db = new DatabaseContext())
                {
                    DateTime tarih = DateTime.Now;
                    model.Yorum.SarkiId = id;
                    kullaniciAdiCek = Session["kullaniciAdi"].ToString();
                    Uye u = db.Uyeler.Where(x => x.KullaniciAdi == kullaniciAdiCek).FirstOrDefault();
                    model.Yorum.UyeId = u.UyeId;
                    model.Yorum.YorumTarihi = tarih;
                    db.Yorumlar.Add(model.Yorum);
                    db.SaveChanges();
                }
                }
            }

            return RedirectToAction("SarkiyaEklemeIslemiYap/" + id);
        }



        public ActionResult SarkiyaEklemeIslemiYap(int id)
        {
            using (var db = new DatabaseContext())
            {
                SarkiEkleViewModel model = new SarkiEkleViewModel();
                model.Sarkilar = db.Sarkilar.Where(x => x.SarkiId == id).FirstOrDefault();
                model.HashTagList = db.HashTagler.Where(x => x.SarkiId == id).ToList();
                model.Uyeler = db.Uyeler.Where(x => x.UyeId == model.Sarkilar.UyeId).FirstOrDefault();
                model.SarkiResimler = db.SarkiResimler.Where(x => x.SarkiId == id).FirstOrDefault();
                model.YorumList = db.Yorumlar.Where(x => x.SarkiId == model.Sarkilar.SarkiId).ToList();
                
                model.PaylasilanTextList = db.PaylasilanTextler.Where(x => x.SarkiId == model.Sarkilar.SarkiId).ToList();
                return View(model);
            }
        }

    }
}