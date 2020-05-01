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
using RestModule1.Models;

namespace RestModule1.Controllers
{
    public class SongsController : ApiController
    {
        private RestModule1Context db = new RestModule1Context();

        // GET: api/Songs
        public IQueryable<Songs> GetSongs(int? pageNumber , int? pageSize)
        {
            var songs = (from s in db.Songs.
                    OrderBy(a => a.SongId)
                select s).AsQueryable();

            int currentPage = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 5;


            var items = songs.Skip((currentPage - 1) * currentPageSize).Take(currentPageSize).ToList();
            return items.AsQueryable();
        }

        [HttpGet]
        public List<Songs> SearchSongs(string searchSongQuery)
        {
            var songs = db.Songs.Where(s => s.SongName.StartsWith(searchSongQuery));
            return songs.ToList();
        }
        

   

    // PUT: api/Songs/5
    [ResponseType(typeof(void))]
    public async Task<IHttpActionResult> PutSongs(int id, Songs songs)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != songs.SongId)
        {
            return BadRequest();
        }

        db.Entry(songs).State = EntityState.Modified;

        try
        {
            await db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SongsExists(id))
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

    // POST: api/Songs
    [ResponseType(typeof(Songs))]
    public async Task<IHttpActionResult> PostSongs(Songs songs)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        db.Songs.Add(songs);
        await db.SaveChangesAsync();

        return CreatedAtRoute("DefaultApi", new { id = songs.SongId }, songs);
    }

    // DELETE: api/Songs/5
    [ResponseType(typeof(Songs))]
    public async Task<IHttpActionResult> DeleteSongs(int id)
    {
        Songs songs = await db.Songs.FindAsync(id);
        if (songs == null)
        {
            return NotFound();
        }

        db.Songs.Remove(songs);
        await db.SaveChangesAsync();

        return Ok(songs);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            db.Dispose();
        }
        base.Dispose(disposing);
    }

    private bool SongsExists(int id)
    {
        return db.Songs.Count(e => e.SongId == id) > 0;
    }
}
}