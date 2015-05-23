using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using ChatterREST.Models;
using Microsoft.AspNet.Identity;

namespace ChatterREST.Controllers
{
    public class BetsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Bets
        public IQueryable<BetDTO> GetBets()
        {
            return from bet in db.Bets
                   select new BetDTO
                   {
                       DateCreated = bet.DateCreated,
                       Description = bet.Description,
                       EndDate = bet.EndDate,
                       Id = bet.Id,
                       RequiredPoints = bet.RequiredPoints,
                       Result = bet.Result,
                       Title = bet.Title,
                       UserName = bet.ApplicationUser.UserName,
                   };
        }

        // GET: api/Bets/5
        [ResponseType(typeof(ICollection<BetDTO>))]
        public async Task<IHttpActionResult> GetBet(int id)
        {
            ICollection<BetDTO> betList;
            Bet bet = await db.Bets.FindAsync(id);
            if (bet == null)
            {
                return NotFound();
            }
            if (id != 0)
            {
                Mapper.CreateMap<Bet, BetDTO>();
                var dto = Mapper.Map<BetDTO>(bet);
                betList = new List<BetDTO> {dto};
            }
            else
            {
                var userBets = from b in db.Bets
                               where b.ApplicationUserId == User.Identity.GetUserId()
                               select new BetDTO
                               {
                                   DateCreated = b.DateCreated,
                                   Description = b.Description,
                                   EndDate = b.EndDate,
                                   Id = b.Id,
                                   RequiredPoints = b.RequiredPoints,
                                   Result = b.Result,
                                   Title = b.Title,
                                   UserName = b.ApplicationUser.UserName,
                               };
                betList = userBets.ToList();
            }
            return Ok(betList);
        }

        // PUT: api/Bets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBet(int id, Bet bet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bet.Id)
            {
                return BadRequest();
            }

            db.Entry(bet).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Bets
        [ResponseType(typeof(Bet))]
        public async Task<IHttpActionResult> PostBet(Bet bet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bet.ApplicationUserId = User.Identity.GetUserId();
            db.Bets.Add(bet);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = bet.Id }, bet);
        }

        // DELETE: api/Bets/5
        [ResponseType(typeof(Bet))]
        public async Task<IHttpActionResult> DeleteBet(int id)
        {
            Bet bet = await db.Bets.FindAsync(id);
            if (bet == null)
            {
                return NotFound();
            }

            db.Bets.Remove(bet);
            await db.SaveChangesAsync();

            return Ok(bet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BetExists(int id)
        {
            return db.Bets.Count(e => e.Id == id) > 0;
        }
    }
}