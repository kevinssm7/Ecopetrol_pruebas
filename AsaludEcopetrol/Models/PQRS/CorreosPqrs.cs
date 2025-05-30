using System;
using System.IO;
using System.Net.Mail;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using AsaludEcopetrol.BussinesManager;
using Outlook = Microsoft.Office.Interop.Outlook;
using ECOPETROL_COMMON.ENTIDADES;
using System.Collections.Generic;
using System.Linq;
using J2N.Text;

namespace AsaludEcopetrol.Models.PQRS
{
    public class CorreosPqrs
    {
        public string Correo { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        #region PROPIEDADES

        private Facede.Facade _BusClass;
        public Facede.Facade BusClass
        {
            get
            {
                if (_BusClass != null)
                {
                    return _BusClass;
                }
                else
                {
                    return _BusClass = new Facede.Facade();
                }

            }
            set { _BusClass = value; }
        }

        private SessionState _SesionVar;
        public SessionState SesionVar
        {
            get
            {
                if (_SesionVar == null)
                {
                    _SesionVar = new SessionState();
                }
                return _SesionVar;
            }
            set { _SesionVar = value; }
        }

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();
        #endregion

        //public int SaveEmailAsEML(MailMessage mailMessage, string rutaEML, int? idPqr, ref MessageResponseOBJ MsgRes)
        //{
        //    try
        //    {
        //        // Crea un archivo EML
        //        using (FileStream fs = new FileStream(rutaEML, FileMode.Create))
        //        using (StreamWriter writer = new StreamWriter(fs))
        //        {
        //            // Escribe las cabeceras del correo
        //            writer.WriteLine("From: " + mailMessage.From);
        //            writer.WriteLine("To: " + string.Join(", ", mailMessage.To));
        //            writer.WriteLine("Subject: " + mailMessage.Subject);
        //            writer.WriteLine("Date: " + DateTime.Now.ToString("r"));
        //            writer.WriteLine("MIME-Version: 1.0");
        //            writer.WriteLine("Content-Type: text/html; charset=\"utf-8\"");
        //            writer.WriteLine();

        //            writer.WriteLine(mailMessage.Body);

        //            // Cierra el archivo
        //            writer.Close();
        //        }

        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
        //        MsgRes.DescriptionResponse = ex.Message;
        //        return 0;
        //    }
        //}

        public int SaveEmailAsEML(MailMessage mailMessage, string rutaEML, int? idPqr, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                // Crea un archivo EML
                using (FileStream fs = new FileStream(rutaEML, FileMode.Create))
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    // Escribe las cabeceras del correo
                    writer.WriteLine("From: " + mailMessage.From);
                    writer.WriteLine("To: " + string.Join(", ", mailMessage.To));
                    writer.WriteLine("Subject: " + mailMessage.Subject);
                    writer.WriteLine("Date: " + DateTime.Now.ToString("r"));
                    writer.WriteLine("MIME-Version: 1.0");
                    writer.WriteLine("Content-Type: multipart/mixed; boundary=\"boundary123\"");
                    writer.WriteLine();

                    // Inicio del mensaje
                    writer.WriteLine("--boundary123");
                    writer.WriteLine("Content-Type: text/html; charset=\"utf-8\"");
                    writer.WriteLine();
                    writer.WriteLine(mailMessage.Body);

                    //Adjuntar archivos
                    foreach (var attachment in mailMessage.Attachments)
                    {
                        writer.WriteLine("--boundary123");

                        // Verifica si el nombre del archivo contiene ".pdf" para establecer el tipo MIME
                        if (attachment.Name.Contains(".pdf"))
                        {
                            writer.WriteLine("Content-Type: application/pdf; name=\"" + attachment.Name + "\"");
                        }
                        else
                        {
                            writer.WriteLine("Content-Type: application/octet-stream; name=\"" + attachment.Name + "\"");
                        }

                        writer.WriteLine("Content-Transfer-Encoding: base64");
                        writer.WriteLine();

                        byte[] attachmentBytes;
                        using (var stream = attachment.ContentStream)
                        {
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                byte[] buffer = new byte[16384];
                                int bytesRead;

                                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    memoryStream.Write(buffer, 0, bytesRead);
                                }

                                // Obtén los bytes del archivo adjunto desde la memoria
                                attachmentBytes = memoryStream.ToArray();
                            }
                        }

                        // Convierte los bytes del archivo adjunto a una cadena Base64
                        string base64Data = Convert.ToBase64String(attachmentBytes);

                        // Escribe los datos Base64 en el archivo EML
                        writer.WriteLine(base64Data);

                        // Finaliza la parte del archivo adjunto
                        writer.WriteLine();
                    }


                    // Final del mensaje
                    writer.WriteLine("--boundary123--");

                    // Cierra el archivo
                    writer.Close();
                }

                return 1;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

        public int SaveEmailAsMSG(MailMessage mailMessage, string ruta, int? idPqr, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                var outlookApp = new Outlook.Application();
                var mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);

                List<GestionDocumentalPQRS2> listaArchivos = new List<GestionDocumentalPQRS2>();

                var posicion = 0;

                mailItem.Subject = mailMessage.Subject;
                mailItem.Body = mailMessage.Body;
                mailItem.HTMLBody = mailMessage.Body;

                // Agrega los destinatarios al correo de Outlook
                foreach (var to in mailMessage.To)
                {
                    mailItem.Recipients.Add(to.ToString());
                }

                listaArchivos = BusClass.GetUrlProyeccion((int)idPqr, ref MsgRes);

                listaArchivos = listaArchivos.Where(x => x.id_tipo_documental != 12 && x.id_tipo_documental != 13).ToList();
                if (listaArchivos.Count() > 0)
                {
                    foreach (var arc in listaArchivos)
                    {
                        Microsoft.Office.Interop.Outlook.OlAttachmentType outlook = new Microsoft.Office.Interop.Outlook.OlAttachmentType();

                        var nombreCompleto = arc.ruta;
                        string[] nombrePartido = nombreCompleto.Split('\\');
                        var nombreArchivo = nombrePartido[4];
                        mailItem.Attachments.Add(nombreCompleto, Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue, Type.Missing, Type.Missing);
                    }
                }

                // Guarda el correo como archivo MSG
                mailItem.SaveAs(ruta, Microsoft.Office.Interop.Outlook.OlSaveAsType.olMSG);

                // Libera los recursos
                mailItem.Close(Microsoft.Office.Interop.Outlook.OlInspectorClose.olDiscard);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(mailItem);

                outlookApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(outlookApp);

                return 1;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return 0;
            }
        }

    }
}