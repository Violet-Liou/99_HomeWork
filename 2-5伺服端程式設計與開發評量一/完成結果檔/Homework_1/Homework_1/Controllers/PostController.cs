using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Homework_1.Models;

namespace Homework_1.Controllers
{
    public class PostController : Controller
    {
        private readonly MessageBoardContext _context;

        public PostController(MessageBoardContext context)
        {
            _context = context;
        }

        // GET: Post
        public async Task<IActionResult> Index()
        {
            var result = await _context.MainContent.OrderByDescending(s => s.CreatedDate).ToListAsync();

            return View(result);
        }

        // GET: Post/Details/5
        public async Task<IActionResult> Display(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainContent = await _context.MainContent
                .FirstOrDefaultAsync(m => m.MainID == id);
            if (mainContent == null)
            {
                return NotFound();
            }

            return View(mainContent);
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MainID,MTitle,MContent,MPhoto,MPhotoType,NAuthor,CreatedDate")] MainContent mainContent, IFormFile? newPhoto)
        {
            mainContent.CreatedDate = DateTime.Now; //設定建立時間為目前時間

            if (newPhoto != null && newPhoto.Length != 0)
            {
                //只允許上傳圖片
                if (newPhoto.ContentType != "image/jpeg" && newPhoto.ContentType != "image/png")
                {
                    ViewData["ErrMessage"] = "只允許上傳.jpg或.png的圖片檔案!!";
                    return View();
                }


                //取得檔案名稱
                string fileName = mainContent.MainID + Path.GetExtension(newPhoto.FileName);

                //取得檔案的完整路徑
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BookPhotos", fileName);
                // /wwwroot/Photos/xxx.jpg

                //將檔案上傳並儲存於指定的路徑

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    newPhoto.CopyTo(fs);
                }

                mainContent.MPhoto = fileName;

            }

            if (ModelState.IsValid)
            {
                _context.Add(mainContent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mainContent);
        }

        private bool MainContentExists(string id)
        {
            return _context.MainContent.Any(e => e.MainID == id);
        }
    }
}
