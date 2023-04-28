using Microsoft.AspNetCore.Mvc;
using MvcApiPersonajesJavierPantoja.Models;
using MvcApiPersonajesJavierPantoja.Services;

namespace MvcApiPersonajesJavierPantoja.Controllers
{
    public class PersonajesController : Controller
    {
        //INYECTAMOS EL SERVICE
        private ServiceApiPersonajes service;

        public PersonajesController(ServiceApiPersonajes service)
        {
            this.service = service;
        }

        //MOSTRAMOS TODOS LOS PERSONAJES
        public async  Task<IActionResult> Index()
        {
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            return View(personajes);
        }

        //MOSTRAMOS LOS DETALLES
        public async Task<IActionResult> Details(int id)
        {

            Personaje per = await this.service.FindPersonajeAsync(id);
            return View(per);
        }

        //CREAMOS UN NUEVO PERSONAJE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Personaje per)
        {
            await this.service.InsertPersonajes
                (per.IdPersonaje, per.Nombre, per.Imagen, per.IdSerie );

            return RedirectToAction("Index");
        }

        //MODIFICAMOS PERSONAJE
        public async Task<IActionResult> Edit(int id)
        {
            Personaje per = await this.service.FindPersonajeAsync(id);
            return View(per);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Personaje per)
        {
            await this.service.UpdatePersonajes
                (per.IdPersonaje, per.Nombre, per.Imagen, per.IdSerie);

            return RedirectToAction("Index");
        }

        //ELIMINAMOS PERSONAJE
        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePersonajesAsync(id);
            return RedirectToAction("Index");
        }
    }
}
