using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Web.Hubs;
using Web.Models;
using Web.Repositories;

namespace Web.Controllers
{
   public class PeopleController : Controller
   {
      private readonly RepositoryPeopleBase RepositoryPeople;
      private readonly IHubContext<CountHub, ICountHub> HubCountContext;
      private readonly IUnitOfWork UnitOfWork;

      private async Task SendHubCountAsync()
      {
         await HubCountContext.Clients.All.SendCountAsync(await RepositoryPeople.CountAsync());
      }

      private async Task<People?> GetPeopleByIdAsync(long? id)
      {
         if (id == null)
         {
            return null;
         }
         return await RepositoryPeople.FindAsync(id);
      }

      private async Task<bool> PeopleExists(long id)
      {
         return await RepositoryPeople.AnyAsync(e => e.Id == id);
      }

      public PeopleController(
         IHubContext<CountHub, ICountHub> hubCountContext,
         RepositoryPeopleBase repositoryPeople,
         IUnitOfWork unitOfWork)
      {
         UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
         RepositoryPeople = repositoryPeople ?? throw new ArgumentNullException(nameof(repositoryPeople));
         HubCountContext = hubCountContext ?? throw new ArgumentNullException(nameof(hubCountContext));
      }

      public IActionResult Index()
      {
         return View(RepositoryPeople.AllAsync(x => x.OrderBy(a => a.Name)));
      }

      public async Task<IActionResult> Details(long? id)
      {
         if (id == null)
         {
            return NotFound();
         }
         var people = await GetPeopleByIdAsync(id);
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
         try
         {
            if (ModelState.IsValid)
            {
               await RepositoryPeople.AddAsync(people);
               if ((await UnitOfWork.CommitAsync()) > 0)
               {
                  await SendHubCountAsync();
               }
               return RedirectToAction(nameof(Index));
            }
            return View(people);
         }
         catch (Exception)
         {
            throw;
         }
         
      }

      public async Task<IActionResult> Edit(long? id)
      {
         if (id == null)
         {
            return NotFound();
         }
         var people = await GetPeopleByIdAsync(id);
         if (people == null)
         {
            return NotFound();
         }
         return View(people);
      }

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
               RepositoryPeople.UpdateAsync(people);
               if ((await UnitOfWork.CommitAsync()) > 0)
               {
                  await SendHubCountAsync();
               }
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!await PeopleExists(people.Id))
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
         if (id == null)
         {
            return NotFound();
         }
         var people = await GetPeopleByIdAsync(id);
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
         var people = await GetPeopleByIdAsync(id);
         if (people != null)
         {
            RepositoryPeople.Remove(people);
         }
         if ((await UnitOfWork.CommitAsync()) > 0)
         {
            await SendHubCountAsync();
         }
         return RedirectToAction(nameof(Index));
      }
   }
}
