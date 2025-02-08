using İngilizceKelimeler.Data;
using İngilizceKelimeler.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace İngilizceKelimeler.Controllers
{
    public class EnglishController : Controller
    {
        private readonly EnglishKelimeTakibiContext dbContext;
        public EnglishController(EnglishKelimeTakibiContext dbContext)
        {
            this.dbContext = dbContext;
        }



        //Kelimeleri ana sayfada listelediğimiz yer
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var kelimeler = await dbContext.EnglishKelimelers.ToListAsync();
            var sayi = kelimeler.Count;
            for (int i = 0; i < sayi; i++)
            {
                if (kelimeler[i].KelimeBilmeSayisi >= 5)
                {
                    kelimeler[i].KelimeVisible = false;
                    dbContext.SaveChanges();
                }
            }
           
            ViewBag.Sayi = sayi;
            return View(kelimeler);
        }
        //Delete
        [HttpPost]
        public async Task<IActionResult> List(int id)
        {
            var kelime= await dbContext.EnglishKelimelers.FindAsync(id);
            if (kelime != null)
            {
                kelime.KelimeVisible = false;
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List");
        }

        ///Search
        [HttpPost]
       public async Task<IActionResult> Search(string aranan)
        {
            if (string.IsNullOrEmpty(aranan))
            {
                return View("Index", new List<EnglishKelimeler>()); // Boş arama olursa boş bir liste döner
            }
            var result=await dbContext.EnglishKelimelers.Where(p=>p.KelimeTur.Contains(aranan)||p.Kelimeİng.Contains(aranan)).ToListAsync();
            return View("List", result);
        }


        //Yeni kelime ekleme
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EnglishKelimeler eng)
        {
            if (eng.KelimeTur == null||eng.Kelimeİng==null||eng.KelimeTur==null&& eng.Kelimeİng==null )
            {
                return RedirectToAction("List");
            }
            else {
                var eng1 = new EnglishKelimeler
                {
                    Kelimeİng = eng.Kelimeİng,
                    KelimeTur = eng.KelimeTur,
                    KelimeBilmeSayisi = 0,
                    KelimeBilememeSayisi=0,
                    KelimeVisible=true
                };

            await dbContext.EnglishKelimelers.AddAsync(eng1);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List");
            }
        }
        //Edit ve Delete işlemleri
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var eng = await dbContext.EnglishKelimelers.FindAsync(id);
            return View(eng);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EnglishKelimeler eng)
        {
           var eng1 = await dbContext.EnglishKelimelers.FindAsync(eng.KelimeId);
            
            eng1.Kelimeİng = eng.Kelimeİng;
            eng1.KelimeTur = eng.KelimeTur;
            eng1.KelimeVisible =eng.KelimeVisible;
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }
        
        // Delete işlemi

        [HttpPost]
        public async Task<IActionResult> Delete(EnglishKelimeler eng)
        {
            var eng1 = await dbContext.EnglishKelimelers.FindAsync(eng.KelimeId);
            if(eng1 != null)
            {
                dbContext.EnglishKelimelers.Remove(eng1);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }


    }
}
