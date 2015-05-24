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
using AutoMapper;
using ChatterREST.Models;

namespace ChatterREST.Controllers
{
    public class BetCommentsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BetComments
        public IQueryable<BetCommentDTO> GetBetComments()
        {
            return from comment in db.BetComments
                   select new BetCommentDTO
                   {
                       Id = comment.Id,
                       BetId = comment.BetId,
                       Commment = comment.Commment,
                       DateCreated = comment.DateCreated,
                       UserName = comment.ApplicationUser.UserName,
                   };
        }

        // GET: api/BetComments/5
        [ResponseType(typeof(BetCommentDTO))]
        public async Task<IHttpActionResult> GetBetComment(int id)
        {
            BetComment betComment = await db.BetComments.FindAsync(id);
            if (betComment == null)
            {
                return NotFound();
            }
            return Ok(betComment);
        }

        // PUT: api/BetComments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBetComment(int id, BetComment betComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != betComment.Id)
            {
                return BadRequest();
            }

            db.Entry(betComment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BetCommentExists(id))
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

        // POST: api/BetComments
        [ResponseType(typeof(BetComment))]
        public async Task<IHttpActionResult> PostBetComment(BetComment betComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BetComments.Add(betComment);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = betComment.Id }, betComment);
        }

        // DELETE: api/BetComments/5
        [ResponseType(typeof(BetComment))]
        public async Task<IHttpActionResult> DeleteBetComment(int id)
        {
            BetComment betComment = await db.BetComments.FindAsync(id);
            if (betComment == null)
            {
                return NotFound();
            }

            db.BetComments.Remove(betComment);
            await db.SaveChangesAsync();

            return Ok(betComment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BetCommentExists(int id)
        {
            return db.BetComments.Count(e => e.Id == id) > 0;
        }
    }
}