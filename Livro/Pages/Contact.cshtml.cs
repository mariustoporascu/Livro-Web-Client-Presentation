using Livro.Extras;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace Livro.Pages
{

    public class ContactModel : PageModel
    {
        private readonly IConfiguration _config;

        public ContactModel(IConfiguration config)
        {
            _config = config;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            public string Text { get; set; }
            [Required]
            public string Nume { get; set; }
            [Required]
            public string NrTelefon { get; set; }
        }

        public IActionResult OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                var emailResp = new EmailSender(_config).SendEmail("support@livro.ro", $"Mesaj pagina contact livro de la : {Input.Email}",
                $"{Input.Nume}, {Input.NrTelefon}, {Input.Text}");

                if (!emailResp)
                {
                    StatusMessage = "Mesajul tau nu a putut fi transmis, te rugam sa reincerci.";

                    return RedirectToPage("/Contact");

                }


                StatusMessage = "Mesajul s-a transmis cu succes, un raspuns v-a fi trimis catre tine in curand!";

                return RedirectToPage("/Contact");

            }

            return RedirectToPage("/Contact");

        }
    }
}
