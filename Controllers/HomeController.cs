using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rosseti.Models;
using Rosseti.ViewModels;

namespace Rosseti.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly rossetiContext _context;
        public HomeController(ILogger<HomeController> logger, rossetiContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(string name, int step, double[] minVal, double[] maxVal)
        {
            SprOscillograms sprOscillograms = new SprOscillograms { Name = name, Step = step };
            _context.Add(sprOscillograms);
            _context.SaveChanges();
            if (minVal.Count() > 0 && maxVal.Count() >= 0)
            {
                List<RightOscillogramsStep> rightOscillogramsStep = new List<RightOscillogramsStep>();
                for (int i = 0; i < maxVal.Count(); i++)
                {
                    rightOscillogramsStep.Add(new RightOscillogramsStep { MaxValue = maxVal[i], MinValue = minVal[i], SprOscillogramId = sprOscillograms.Id });
                }
                _context.AddRange(rightOscillogramsStep);
                _context.SaveChanges();
            }
            
            return View();
        }
        [HttpPost]
        public void AddError(int startIndex, int endIndex, string errorName, int sprOscillId, int dataOscillId)
        {
            var dataVal = _context.Oscillograms.Where(x => x.DateOscillogramId == dataOscillId).Take(1000).Select(s => s.Value).ToList();
            dataVal.RemoveRange(endIndex, dataVal.Count() - endIndex - 1);
            dataVal.RemoveRange(0, startIndex);
            var x = dataVal;
            SprErrors sprErrors = new SprErrors() { Name = errorName, SprOscillogramId = sprOscillId };
            _context.Add(sprErrors);
            _context.SaveChanges();

            List<SprErrorOscillograms> SprErrorOscillograms = new List<SprErrorOscillograms>();
            for (int i=0;i<dataVal.Count(); i++)
            {
                SprErrorOscillograms.Add(new SprErrorOscillograms { Inaccuracy = 20, SprErrorId = sprErrors.Id, Value = dataVal[i] });
            }

            _context.AddRange(SprErrorOscillograms);
            _context.SaveChanges();
            
        }


        [HttpGet]
        public IActionResult TableOscillogram()
        {
            var model = _context.SprOscillograms;
            return PartialView("Index/TableOscillogram", model);
        }
        [HttpGet]
        public IActionResult TableNormalize(int id)
        {
            IndexVievModel model = new IndexVievModel()
            {
                rightOscillogramsSteps = _context.RightOscillogramsStep.Where(x => x.SprOscillogramId == id),
                DateOscillograms = _context.DateOscillograms.Where(x => x.SprOscillogramId == id),
                SprErrors = _context.SprErrors.Where(x=>x.SprOscillogramId == id)
            };
            return PartialView("Index/TableNormalize", model);
        }
        [HttpGet]
        public IActionResult TableReceivedOscillograms(int id)
        {
            var model = _context.Oscillograms.Where(x => x.DateOscillogramId == id);
            return PartialView("Index/TableReceivedOscillograms", model);
        }


        int stepIndex = 0;
        [HttpGet]
        public JsonResult ChartsData(int id)
        {
            int sproscilogramid = _context.DateOscillograms.FirstOrDefault(x => x.Id == id).SprOscillogramId;
            var minMax =  _context.RightOscillogramsStep.Where(x => x.SprOscillogramId == sproscilogramid).Select(s => new { s.MaxValue, s.MinValue }).ToList();
            ChartViewModel model = new ChartViewModel();
            model.dataVal = _context.Oscillograms.Where(x => x.DateOscillogramId == id).Take(1000).Select(s => s.Value).ToArray<double>();
            model.dataMin = new List<double>();
            model.dataMax = new List<double>();
            int stepCount = minMax.Count();
            
            foreach (var item in model.dataVal)
            {
                
                if (stepIndex == stepCount)
                {
                    stepIndex = 0;
                }
                model.dataMin.Add(minMax[stepIndex].MinValue);
                model.dataMax.Add(minMax[stepIndex].MaxValue);
                stepIndex++;
            }          
            return Json(model);
        }
        [HttpGet]
        public JsonResult ChartsDataError(int id)
        {
            var model = _context.SprErrorOscillograms.Where(x => x.SprErrorId == id).Select(x=>x.Value);         
            return Json(model);
        }

    }
}
