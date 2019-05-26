using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MesajCore.Models;
using MesajCore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MesajCore.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _db;
        UserManager<Person> _mng;

        public HomeController(ApplicationDbContext disaridanGelen, UserManager<Person> disaridanManager)
        {
            _db = disaridanGelen;
            _mng = disaridanManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult GelenKutusu()
        //{
        //    var uid = _mng.GetUserId(User);
        //    var sorguBizAliciysak = from m in _db.Mesajlar
        //                orderby m.Tarih descending
        //                where m.AliciId == uid
        //                join p in _db.Users
        //                on m.GonderenId equals p.Id
        //                group m by p into conv
        //                select new {
        //                    Kisi = conv.Key,
        //                    Mesaj = conv.FirstOrDefault()
        //                };

        //    var sorguBizGonderensek = from m in _db.Mesajlar
        //                            orderby m.Tarih descending
        //                            where m.GonderenId == uid
        //                            join p in _db.Users
        //                            on m.AliciId equals p.Id
        //                            group m by p into conv
        //                            select new
        //                            {
        //                                Kisi = conv.Key,
        //                                Mesaj = conv.FirstOrDefault()
        //                            };
        //    var liste = sorguBizAliciysak.ToList();
        //    var liste_2 = sorguBizGonderensek.ToList();
        //    liste.AddRange(liste_2);

        //    var sonliste = from l in liste 
        //                   group l by l.Kisi into conv
        //                   select new
        //                   {
        //                       Kisi = conv.Key,
        //                       Mesaj = conv.FirstOrDefault()
        //                   };

        //    return View(sonliste.ToList());
        //}

        public IActionResult GelenKutusu()
        {
            var uid = _mng.GetUserId(User);
            var liste = _db.Mesajlar
                .Where(x => x.AliciId == uid)
                .Include("Gonderen")
                .OrderByDescending(x => x.Tarih)
                .ToList();

            ViewBag.Kisiler = _db.Users
                .Where(x=>x.Id != uid)
                .ToList();

            return View(liste);
        }

        public IActionResult GidenKutusu()
        {
            var uid = _mng.GetUserId(User);
            var liste = _db.Mesajlar
                .Where(x => x.GonderenId == uid)
                .Include("Alici")
                .OrderByDescending(x => x.Tarih)
                .ToList();

            ViewBag.Kisiler = _db.Users
                .Where(x => x.Id != uid)
                .ToList();

            return View(liste);
        }

        [HttpPost]
        public IActionResult MesajGonder(Mesaj m)
        {
            m.GonderenId = _mng.GetUserId(User);
            m.Tarih = DateTime.Now;

            _db.Mesajlar.Add(m);
            _db.SaveChanges();

            return RedirectToAction("GidenKutusu");
        }
    }
}
