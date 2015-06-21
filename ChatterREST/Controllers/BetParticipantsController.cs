using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ChatterREST.Models;
using Microsoft.AspNet.Identity;

namespace ChatterREST.Controllers
{
    public class BetParticipantsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BetParticipants
        public IQueryable<BetParticipantDTO> GetBetParticipants()
        {
            return from participant in db.BetParticipants
                   select new BetParticipantDTO
                   {
                       BetId = participant.BetId,
                       Id = participant.Id,
                       Option = participant.Option,
                       UserName = participant.ApplicationUser.UserName,
                   };
        }

        // GET: api/BetParticipants/5
        [ResponseType(typeof(BetParticipant))]
        public async Task<IHttpActionResult> GetBetParticipant(int id)
        {
            BetParticipant betParticipant = await db.BetParticipants.FindAsync(id);
            if (betParticipant == null)
            {
                return NotFound();
            }

            return Ok(betParticipant);
        }

        // PUT: api/BetParticipants/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBetParticipant(int id, BetParticipant betParticipant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != betParticipant.Id)
            {
                return BadRequest();
            }

            db.Entry(betParticipant).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BetParticipantExists(id))
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

        // POST: api/BetParticipants
        [ResponseType(typeof(BetParticipant))]
        public async Task<IHttpActionResult> PostBetParticipant(BetParticipant betParticipant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            betParticipant.ApplicationUserId = User.Identity.GetUserId();
            betParticipant.ApplicationUser.Point -= betParticipant.Bet.RequiredPoints;
            db.BetParticipants.Add(betParticipant);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = betParticipant.Id }, betParticipant);
        }

        // DELETE: api/BetParticipants/5
        [ResponseType(typeof(BetParticipant))]
        public async Task<IHttpActionResult> DeleteBetParticipant(int id)
        {
            BetParticipant betParticipant = await db.BetParticipants.FindAsync(id);
            if (betParticipant == null)
            {
                return NotFound();
            }

            db.BetParticipants.Remove(betParticipant);
            await db.SaveChangesAsync();

            return Ok(betParticipant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BetParticipantExists(int id)
        {
            return db.BetParticipants.Count(e => e.Id == id) > 0;
        }
    }
}