using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ApiFormulario.Models;

namespace ApiFormulario.Controllers
{
    public class formsController : Controller
    {
        private conn db = new conn();

        // GET: forms
        public async Task<ActionResult> Index()
        {
            return View(await db.CADASTRO.ToListAsync());
        }

        // GET: forms/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            form form = await db.CADASTRO.FindAsync(id);
            if (form == null)
            {
                return HttpNotFound();
            }
            return View(form);
        }

        // GET: forms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: forms/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,NOME,SOBRENOME,TELEFONE")] form form)
        {
            if (ModelState.IsValid)
            {
                db.CADASTRO.Add(form);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(form);
        }

        // GET: forms/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            form form = await db.CADASTRO.FindAsync(id);
            if (form == null)
            {
                return HttpNotFound();
            }
            return View(form);
        }

        // POST: forms/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NOME,SOBRENOME,TELEFONE")] form form)
        {
            if (ModelState.IsValid)
            {
                db.Entry(form).State =  System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(form);
        }

        // GET: forms/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            form form = await db.CADASTRO.FindAsync(id);
            if (form == null)
            {
                return HttpNotFound();
            }
            return View(form);
        }

        // POST: forms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            form form = await db.CADASTRO.FindAsync(id);
            db.CADASTRO.Remove(form);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
