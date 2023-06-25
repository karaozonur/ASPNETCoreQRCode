using Microsoft.AspNetCore.Mvc;
using MVCCoreQRCode.Models;
using QRCoder;
using System.Diagnostics;
using System.Drawing;

namespace MVCCoreQRCode.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult CreateQRCode()
        {
            QRCodeModel model =new QRCodeModel();

            return View(model);
        }
        [HttpPost]
        public IActionResult CreateQRCode(QRCodeModel qRCode)
        {
            QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qrCodeGenerator.CreateQrCode(qRCode.QRCodeText, QRCodeGenerator.ECCLevel.Q);
            QRCode QRCode = new QRCode(qRCodeData);
            Bitmap qrbitmap = QRCode.GetGraphic(60);
            byte[] bitmapArray=qrbitmap.BitmapToByteArray();
            string Url = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(bitmapArray));

            qRCode.returnQRURL = Url;


            return View(qRCode);
        }













        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}