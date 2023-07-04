using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Web.Hubs;
using Web.Models;
namespace Web.Controllers
{
   public class PeopleController : Controller
   {
      private readonly DataAccessContext Context;

      private readonly IHubContext<CountHub, ICountHub> HubCountContext;

      private async Task SendHubCountAsync()
      {
         await HubCountContext.Clients.All.SendCountAsync(await Context.Peoples.AsNoTracking().LongCountAsync());
      }

      public PeopleController(DataAccessContext context, IHubContext<CountHub, ICountHub> hubCountContext)
      {
         Context = context;
         HubCountContext = hubCountContext;
      }

      public async Task<IActionResult> Index()
      {
         return Context.Peoples != null ?
                     View(await Context.Peoples.ToListAsync()) :
                     Problem("Entity set 'DataAccessContext.Peoples'  is null.");
      }

      public async Task<IActionResult> Details(long? id)
      {
         if (id == null || Context.Peoples == null)
         {
            return NotFound();
         }

         var people = await Context.Peoples
             .FirstOrDefaultAsync(m => m.Id == id);
         if (people == null)
         {
            return NotFound();
         }

         return View(people);
      }

      public IActionResult Create()
      {
         return View();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("Id,Name")] People people)
      {
         if (ModelState.IsValid)
         {
            Context.Add(people);
            if ((await Context.SaveChangesAsync()) > 0)
            {
               await SendHubCountAsync();
            }
            return RedirectToAction(nameof(Index));
         }
         return View(people);
      }

      // GET: People/Edit/5
      public async Task<IActionResult> Edit(long? id)
      {
         if (id == null || Context.Peoples == null)
         {
            return NotFound();
         }

         var people = await Context.Peoples.FindAsync(id);
         if (people == null)
         {
            return NotFound();
         }
         return View(people);
      }

      // POST: People/Edit/5
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] People people)
      {
         if (id != people.Id)
         {
            return NotFound();
         }

         if (ModelState.IsValid)
         {
            try
            {
               Context.Update(people);
               if ((await Context.SaveChangesAsync()) > 0)
               {
                  await SendHubCountAsync();
               }
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!PeopleExists(people.Id))
               {
                  return NotFound();
               }
               else
               {
                  throw;
               }
            }
            return RedirectToAction(nameof(Index));
         }
         return View(people);
      }

      public async Task<IActionResult> Delete(long? id)
      {
         if (id == null || Context.Peoples == null)
         {
            return NotFound();
         }

         var people = await Context.Peoples.FirstOrDefaultAsync(m => m.Id == id);
         if (people == null)
         {
            return NotFound();
         }

         return View(people);
      }

      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(long id)
      {
         if (Context.Peoples == null)
         {
            return Problem("Entity set 'DataAccessContext.Peoples'  is null.");
         }
         var people = await Context.Peoples.FindAsync(id);
         if (people != null)
         {
            Context.Peoples.Remove(people);
         }

         if ((await Context.SaveChangesAsync()) > 0)
         {
            await SendHubCountAsync();
         }
         return RedirectToAction(nameof(Index));
      }

      private bool PeopleExists(long id)
      {
         return (Context.Peoples?.Any(e => e.Id == id)).GetValueOrDefault();
      }
   }
}
