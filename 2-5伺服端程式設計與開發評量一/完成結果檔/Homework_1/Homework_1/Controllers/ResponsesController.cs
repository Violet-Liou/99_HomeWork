using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Homework_1.Models;
using System.Net;

namespace Homework_1.Controllers
{
    public class ResponsesController : Controller
    {
        private readonly MessageBoardContext _context;

        public ResponsesController(MessageBoardContext context)
        {
            _context = context;
        }

        public IActionResult Create(string MainID)
        {
            //ViewData["MainID"] = new SelectList(_context.MainContent, "MainID", "MainID");
            ViewData["MainID"] = MainID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResponseID,RContent,RAuthor,CreatedDate,MainID")] Response response)
        {
            if (ModelState.IsValid)
            {
                _context.Add(response);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return Json(response);
            }
            //ViewData["MainID"] = new SelectList(_context.MainContent, "MainID", "MainID", response.MainID);
            //return View(response);

            return Json(response);
        }

        public IActionResult GetResponseByViewComponent(string MainID)
        {

            return ViewComponent("VCPost", new { bookID = MainID });

        }

        private bool ResponseExists(string id)
        {
            return _context.Response.Any(e => e.ResponseID == id);
        }
    }
}
