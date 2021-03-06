﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using TomatoLog.Common.Interface;
using TomatoLog.Server.Models;
using TomatoLog.Server.ViewModels;

namespace TomatoLog.Server.Controllers
{
    public class HomeController : BaseController
    {
        public static string[] FIELDS = new string[] { "EventId", "Timestamp", "IP", "IPList", "ErrorMessage", "ProcessName", "ProjectName" };
        private ILogWriter logWriter;
        private HttpClient client;
        private IConfiguration cfg;
        public HomeController(ILogWriter logWriter, HttpClient client, IConfiguration cfg, ILogger<LogController> logger) : base(cfg, logger)
        {
            this.logWriter = logWriter;
            this.client = client;
            this.cfg = cfg;
        }

        public async Task<IActionResult> Index()
        {
            var projs = await logWriter.GetProjects();
            ViewBag.Projects = projs;
            return View();
        }

        //public async Task<IActionResult> Detail([FromQuery]string proj, [FromQuery]string label, [FromQuery]string keyword, int page = 1, int pageSize = 100)
        public async Task<IActionResult> Detail([FromQuery]MessageViewModel model)
        {
            ViewBag.Message = model;
            ViewBag.FIELDS = FIELDS;

            //ViewBag.Proj = model.Project;
            //ViewBag.Label = model.Label;
            //ViewBag.FIELDS = FIELDS;
            //ViewBag.Keyword = model.Keyword;
            var result = await logWriter.List(model.Project, model.Label, model.Keyword, model.Page, model.PageSize);
            return View(result);
        }


        [HttpPost]
        // public IActionResult Detail([FromForm]string[] fields, [FromForm]string proj, [FromForm]string label, [FromForm]string keyword, int page = 1, int pageSize = 100)
        public IActionResult Detail([FromForm]string[] fields, [FromForm]MessageViewModel model)
        {
            FIELDS = fields;
            //return RedirectToAction("Detail", new { proj, label, keyword, page, pageSize });
            return RedirectToAction("Detail", model);
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
