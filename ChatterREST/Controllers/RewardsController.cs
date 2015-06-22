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
    public class RewardsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Rewards
        public IQueryable<Reward> GetRewards()
        {
            return db.Rewards;
        }

        // GET: api/Rewards/5
        [ResponseType(typeof(Reward))]
        public async Task<IHttpActionResult> GetReward(int id)
        {
            Reward reward = await db.Rewards.FindAsync(id);
            if (reward == null)
            {
                return NotFound();
            }
            
            return Ok(reward);
        }

        // PUT: api/Rewards/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutReward(int id, Reward reward)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reward.Id)
            {
                return BadRequest();
            }
            reward.Quantity -= 1;
            db.Users.Find(User.Identity.GetUserId()).Point -= reward.Value;
            db.Entry(reward).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RewardExists(id))
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

        // POST: api/Rewards
        [ResponseType(typeof(Reward))]
        public async Task<IHttpActionResult> PostReward(Reward reward)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rewards.Add(reward);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = reward.Id }, reward);
        }

        // DELETE: api/Rewards/5
        [ResponseType(typeof(Reward))]
        public async Task<IHttpActionResult> DeleteReward(int id)
        {
            Reward reward = await db.Rewards.FindAsync(id);
            if (reward == null)
            {
                return NotFound();
            }

            db.Rewards.Remove(reward);
            await db.SaveChangesAsync();

            return Ok(reward);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RewardExists(int id)
        {
            return db.Rewards.Count(e => e.Id == id) > 0;
        }
    }
}