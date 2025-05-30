using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Mvc;
using System.IO;


namespace AsaludEcopetrol.Models
{
    /// <summary>
    /// Clase que define un resultado al Browser de tipo PDF
    /// </summary>
    public class PDFResult : ActionResult
    {
        private String _ContentType = "application/pdf";
        public byte[] FileBytes { get; set; }
        public String SourceFilename { get; set; }
        public String AttachmentName { get; set; }

        /// <summary>
        /// Constructor que recibe la ubicación del archivo PDF a ser enviado al browser
        /// </summary>
        /// <param name="sourceFilename">Ruta fisica en la cual se encuentra ubicado el archivo PDF</param>
        public PDFResult(String sourceFilename)
        {
            SourceFilename = sourceFilename;
        }
        
        /// <summary>
        /// Constructor que recibe la ruta del archivo a generar y el nombre del archivo con que se generará en el response
        /// </summary>
        /// <param name="sourceFilename">Ubicación del archivo PDF</param>
        /// <param name="attachmentName">Nombre con que se generará el archivo</param>
        public PDFResult(String sourceFilename, string attachmentName)
        {
            SourceFilename = sourceFilename;
            AttachmentName = attachmentName;
        }

        /// <summary>
        /// Genera el resultado del archivo como un stream al Response del contexto Web para que sea enviado al navegador
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.Clear();
            response.AddHeader("Content-disposition", "attachment; filename=" + (string.IsNullOrEmpty(AttachmentName) ? "archivo.pdf" : AttachmentName));
            response.Cache.SetCacheability(HttpCacheability.NoCache);
            response.ContentType = _ContentType;

            if (FileBytes != null)
            {
                var stream = new MemoryStream(FileBytes);
                stream.WriteTo(response.OutputStream);
                stream.Dispose();
            }
            else
            {
                response.TransmitFile(SourceFilename);
            }
        }
    }
}
