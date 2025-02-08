using İngilizceKelimeler.Data;
using İngilizceKelimeler.Models;
using İngilizceKelimeler.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace İngilizceKelimeler.Controllers
{
    public class KelimeTestController : Controller 
    {

        private readonly RandomSayiUret _randomSayiUret;
        private readonly EnglishKelimeTakibiContext _dbContext;
       

        public KelimeTestController(RandomSayiUret randomSayiUret, EnglishKelimeTakibiContext dbcontext)
        {
            _randomSayiUret = randomSayiUret;
            _dbContext = dbcontext;
         

        }


        [HttpGet]
        public IActionResult Index()
        {

            var kelimeler = _dbContext.EnglishKelimelers.Where(k => k.KelimeVisible == true).ToList();
            var sayi = kelimeler.Count;


            //&& k.KelimeBilmeSayisi - k.KelimeBilememeSayisi < 0
            foreach (var kelime in kelimeler)
            {
                if (kelime.KelimeBilmeSayisi - kelime.KelimeBilememeSayisi >= 5)
                {
                    kelime.KelimeVisible = false;
                    _dbContext.SaveChanges();
                }
            }
            int randomNumber = _randomSayiUret.RandomSayiUretme(0, sayi);
            var DogruKelime = kelimeler[randomNumber];

            // Ekstra 3 rastgele kelime seç
            var ekstraKelimeler = kelimeler
                .Where(k => k != DogruKelime) // Ana kelime hariç diğerlerini seç
                .OrderBy(x => Guid.NewGuid()) // Rastgele sırala
                .Take(3) // İlk 3 tanesini al
                .ToList();

            if (ekstraKelimeler == null || ekstraKelimeler.Count < 3)
            {
                return View("Index"); // Hata sayfası göster
            }
            // Ana kelimeyi listeye ekle
            ekstraKelimeler.Add(DogruKelime);
            
            //View a gönderme
            var TumKelimeler=ekstraKelimeler.OrderBy(x => Guid.NewGuid()).ToList();
            ViewBag.DogruKelime = DogruKelime;
            return View(TumKelimeler);
        }
        [HttpPost]
        public IActionResult Index(int Secilen, int DogruKelime)
        {
            var kelime = _dbContext.EnglishKelimelers.FirstOrDefault(k => k.KelimeId == DogruKelime);
            if (kelime != null)
            {
                if (Secilen == DogruKelime)
                {
                    ViewBag.SonucD = "Doğru bildiniz";
                    kelime.KelimeBilmeSayisi++;
                    _dbContext.SaveChanges();
                }
                else
                {
                    ViewBag.SonucY = "Yanlış bildiniz";
                    kelime.KelimeBilememeSayisi++;
                    ViewBag.Dogrusu = kelime.KelimeTur;
                    _dbContext.SaveChanges();
                }

            }
            return View();
        }
    }
}
