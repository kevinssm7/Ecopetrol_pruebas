using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using DocuSign.Integrations.Client;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Activities;
using System.Web;
using System.Web.Mvc;
using Document = DocuSign.eSign.Model.Document;

namespace AsaludEcopetrol.Controllers.Docusign
{
    public class DocusignController : Controller
    {
        MyCredential credential = new MyCredential();
        private string INTEGRATOR_KEY = "8c5f9d1c-0684-4fef-8ff6-dd0ef1e6cabf";


        public ActionResult IngresoDocumentosDocuSign()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult IngresoDocumentosDocuSign(Models.FirmaElectronica.Recipient model, HttpPostedFileBase file)
        {
            Models.FirmaElectronica.Recipient recipientModel = new Models.FirmaElectronica.Recipient();
            string directorypath = Server.MapPath("~/Resources");
            if (!Directory.Exists(directorypath))
            {
                Directory.CreateDirectory(directorypath);

            }

            byte[] data;
            using (Stream inputStream = file.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }

            var serverpath = directorypath + model.Name.Trim() + ".pdf";        
            System.IO.File.WriteAllBytes(serverpath, data);
            docusign(serverpath, model.Name, model.Email);
            return View();

        }

        public JsonResult SaveDocumentosDocuSign(HttpPostedFileBase file, Models.FirmaElectronica.Recipient model)
        {

            String mensaje = "";
            String tipoAlerta = "";
            Models.General General = new Models.General();

            if (this.Request.Files["file"].ContentLength > 0)
            {

                try
                {
                    Models.FirmaElectronica.Recipient recipientModel = new Models.FirmaElectronica.Recipient();
                    string directorypath = Server.MapPath("~/Resources");
                    if (!Directory.Exists(directorypath))
                    {
                        Directory.CreateDirectory(directorypath);

                    }

                    byte[] data;
                    using (Stream inputStream = file.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        data = memoryStream.ToArray();
                    }

                    var serverpath = directorypath + model.Name.Trim() + ".pdf";
                    System.IO.File.WriteAllBytes(serverpath, data);
                    docusign(serverpath, model.Name, model.Email);


                    mensaje = "SE INGRESO CORRECTAMENTE....";
                    tipoAlerta = "2";

                    return Json(new { success = true, message = mensaje, tipo = tipoAlerta }, JsonRequestBehavior.AllowGet);


                }
                catch (Exception ex)
                {

                    mensaje = "*---ERROR -- - *" + ex.Message;
                    tipoAlerta = "3";

                    return Json(new { success = false, message = mensaje, tipo = tipoAlerta }, JsonRequestBehavior.AllowGet);


                }
            }
            else
            {
                mensaje = "*---DEBE SELECCIONAR UN ARCHIVO PARA REALIZAR EL CARGUE -- - *";
                tipoAlerta = "3";

                return Json(new { success = false, message = mensaje, tipo = tipoAlerta }, JsonRequestBehavior.AllowGet);

            }


        }

        public string loginApi(string usr, string pwd)
        {
            // we set the api client in global config when we configured the client 
            ApiClient apiClient = Configuration.Default.ApiClient;
            string authHeader = "{\"Username\":\"" + usr + "\", \"Password\":\"" + pwd + "\", \"IntegratorKey\":\"" + INTEGRATOR_KEY + "\"}";
            Configuration.Default.AddDefaultHeader("X-DocuSign-Authentication", authHeader);

            // we will retrieve this from the login() results
            string accountId = null;

            // the authentication api uses the apiClient (and X-DocuSign-Authentication header) that are set in Configuration object
            AuthenticationApi authApi = new AuthenticationApi();
            LoginInformation loginInfo = authApi.Login();

            // find the default account for this user
            foreach (DocuSign.eSign.Model.LoginAccount loginAcct in loginInfo.LoginAccounts)
            {
                if (loginAcct.IsDefault == "true")
                {
                    accountId = loginAcct.AccountId;
                    break;
                }
            }
            if (accountId == null)
            { // if no default found set to first account
                accountId = loginInfo.LoginAccounts[0].AccountId;
            }
            return accountId;
        }

        public void docusign(string path, string recipientName, string recipientEmail)
        {

            ApiClient apiClient = new ApiClient("https://demo.docusign.net/restapi");
            Configuration.Default.ApiClient = apiClient;

            //Verify Account Details
            string accountId = loginApi(credential.UserName, credential.Password);

            // Read a file from disk to use as a document.
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);

            EnvelopeDefinition envDef = new EnvelopeDefinition();
            envDef.EmailSubject = "Por favor firmar este documento";

            // Add a document to the envelope
            Document doc = new Document();
            doc.DocumentBase64 = System.Convert.ToBase64String(fileBytes);
            doc.Name = Path.GetFileName(path);
            doc.DocumentId = "1";

            envDef.Documents = new List<Document>();
            envDef.Documents.Add(doc);

            // Add a recipient to sign the documeent
            DocuSign.eSign.Model.Signer signer = new DocuSign.eSign.Model.Signer();
            signer.Email = recipientEmail;
            signer.Name = recipientName;
            signer.RecipientId = "1";

            envDef.Recipients = new DocuSign.eSign.Model.Recipients();
            envDef.Recipients.Signers = new List<DocuSign.eSign.Model.Signer>();
            envDef.Recipients.Signers.Add(signer);

            //set envelope status to "sent" to immediately send the signature request
            envDef.Status = "sent";

            // |EnvelopesApi| contains methods related to creating and sending Envelopes (aka signature requests)
            EnvelopesApi envelopesApi = new EnvelopesApi();
            EnvelopeSummary envelopeSummary = envelopesApi.CreateEnvelope(accountId, envDef);

            // print the JSON response
            var result = JsonConvert.SerializeObject(envelopeSummary);
        }
    }

    public class MyCredential
    {
        public string UserName { get; set; } = "freleogarcia@gmail.com";
        public string Password { get; set; } = "Leo80240877#";
    }
}