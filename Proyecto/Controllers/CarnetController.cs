using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers  
{
    [Authorize]
    public class CarnetController : Controller
    {
        // GET: Carnet
        public ActionResult Index()
        {
            var URLRuta = ConfigurationManager.AppSettings["URLRuta"].ToString();

            ViewBag.Identificacion = Session["Identificacion"].ToString();
            ViewBag.Telefono = Session["Telefono"].ToString();
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(URLRuta +"idEmp="+ Session["Empresa"].ToString(), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ViewBag.imageBytes = ms.ToArray();
                    //imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
            }
            return View();
        }
    }
}