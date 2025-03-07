using Microsoft.AspNetCore.Mvc;
using MvcNetCorePracticaZapatillas.Models;
using MvcNetCorePracticaZapatillas.Repositories;

namespace MvcNetCorePracticaZapatillas.Controllers
{
    public class ZapatillasController : Controller
    {

        private RepositoryZapatillas repo;

        public ZapatillasController(RepositoryZapatillas repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Zapatilla> zapatillas = await this.repo.GetZapatillasAsync();
            return View(zapatillas);
        }

        public async Task<IActionResult> Details(int idZapatilla)
        {
            Zapatilla z = await this.repo.FindZapatillaAsync(idZapatilla);
            return View(z);
        }
        public async Task<IActionResult> _ImagenesZapatillas(int? posicion, int idProducto)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            ModelImagenesZapatilla model = await this.repo.GetImagenesZapatillaOutAsync(posicion.Value, idProducto);
            ViewData["REGISTROS"] = model.NumeroRegistros;
            ViewData["POSICION"] = posicion;
            int siguiente = posicion.Value + 1;
            if (siguiente > model.NumeroRegistros)
            {
                siguiente = model.NumeroRegistros;
            }
            int anterior = posicion.Value - 1;
            if (anterior < 1)
            {
                anterior = 1;
            }
            ViewData["ULTIMO"] = model.NumeroRegistros;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            ViewData["ZAPATILLA"] = model.Zapatilla;
            return PartialView("_ImagenesZapatillas", model.ImagenesZapatilla);
        }
    }
}
