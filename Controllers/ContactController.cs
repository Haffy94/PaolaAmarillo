using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using PaolaAmarillo.Models;
public class ContactController : Controller
{
    [HttpPost]
    public IActionResult SendEmail(ContactFormModel model)
    {
     
        try
        {
            var fromAddress = new MailAddress("lucasezequielhafner@gmail.com", "Formulario Web");
            var toAddress = new MailAddress("lucasezequielhafner@gmail.com", "Destino");
            const string fromPassword = "wdcp zvtx hwmu soao";
            string subject = $"Nuevo mensaje de {model.Name}";
            string body = $"Nombre: {model.Name}\nEmail: {model.Email}\nMensaje:\n{model.Mesagge}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }

            return Json(new { success = true, message = "Message sent successfully!" });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Json(new { success = false, message = "Error sending the message. Please try again later." });
        }
    }

    public IActionResult Success()
    {
        return View();
    }
}
