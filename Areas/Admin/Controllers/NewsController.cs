using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BazinamSite2.Areas.Admin.ViewModel;
using BazinamSite2.Models;

namespace BazinamSite2.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        // GET: Admin/News
        public async Task<ActionResult> Index()
        {
            using (SystemDbContext context = new SystemDbContext())
            {
                var newsVM = await context.News
                  .Select(x => new BazinamSite2.Areas.Admin.ViewModel.News()
                    {
                     NewsID= x.NewsID,
                    Title = x.Title,
                    Content = x.Content,
                    ReleaseDate = x.ReleaseDate
                     }).ToListAsync();
                return View(newsVM);
            }
        }

        // GET: Admin/News/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/News/Create
        [HttpPost]
        public async Task<ActionResult> Create(BazinamSite2.Areas.Admin.ViewModel.News _new)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    BazinamSite2.Models.News news = new BazinamSite2.Models.News()
                    {
                        Title = _new.Title,
                        Content = _new.Content,
                        ReleaseDate = _new.ReleaseDate
                    };
                    if (ViewBag.NewsFile != null)
                    {
                        var files = (List<string>) ViewBag.NewsFile;
                        foreach (var fileName in files)
                        {
                            byte[] result;
                            using (FileStream SourceStream = System.IO.File.Open(fileName, FileMode.Open))
                            {
                                result = new byte[SourceStream.Length];
                                await SourceStream.ReadAsync(result, 0, (int)SourceStream.Length);
                            }
                            BazinamSite2.Models.Picture pic=new BazinamSite2.Models.Picture()
                            {
                                PicName = fileName,
                                PicSourceBytes = result,
                                PicUrl = "",
                                IsRefrence = false
                            };
                            news.PicturescCollection.Add(pic);
                        }
                        
                    }
                    using (SystemDbContext context=new SystemDbContext())
                    {
                        context.News.Add(news);
                        await context.SaveChangesAsync();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult UploadNewsPic(IEnumerable<HttpPostedFileBase> files)
        {
            // The Name of the Upload component is "files"
            if (files != null)
            {
                foreach (var file in files)
                {
                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    var fileName = Path.GetFileName(file.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Areas/Admin/TempPic"), fileName);

                    // The files are not actually saved in this demo
                     file.SaveAs(physicalPath);
                    if (ViewBag.NewsFile == null)
                    {
                        var picList = new List<string>();
                        ViewBag.NewsFile = picList;
                    }
                    ((List<string>)ViewBag.NewsFile).Add(fileName);
                }
            }
            
            // Return an empty string to signify success
            return Content("");
        }
        public async Task<ActionResult> RemovePic(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Areas/Admin/TempPic"), fileName);

                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        System.IO.File.Delete(physicalPath);
                        if (ViewBag.NewsFile != null)
                        {
                            ((List<string>)ViewBag.NewsFile).Remove(fileName);
                        }
                    
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }
        // GET: Admin/News/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/News/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/News/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
