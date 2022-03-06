using CadastroLogin.Data;
using CadastroLogin.Models;
using CadastroLogin.Models.Entities;
using CadastroLogin.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CadastroLogin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CadastroContext Context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, CadastroContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            Context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var items = Context.Usuarios.ToList();
            return View(items);
        }

        public IActionResult Create() 
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(UsuarioViewModel vm)
        {
            string stringFileName = UploadFile(vm);
            var usuario = new Usuario
            {
                Name = vm.Name,
                Email = "micael@teste",
                ProfileImagem = stringFileName
            };
            Context.Usuarios.Add(usuario);
            Context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private string UploadFile(UsuarioViewModel vm)
        {
            string fileName = null;
            if (vm.ProfileImagem != null) 
            {
                string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + vm.ProfileImagem.FileName;
                string filePath = Path.Combine(uploadDir, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create)) 
                { 
                    vm.ProfileImagem.CopyTo(fileStream);
                };
            }

            return fileName;
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