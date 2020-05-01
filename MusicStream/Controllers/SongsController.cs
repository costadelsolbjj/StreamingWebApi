using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStream.Models;

namespace MusicStream.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly MusicStreamContext _context;

        public SongsController(MusicStreamContext context)
        {
            _context = context;
        }

        // GET: api/Songs1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Songs>>> GetSongs()
        {
            return await _context.Songs.ToListAsync();
        }
        [HttpGet("{pageNumber}/{pageSize}")]
        public IQueryable<Songs> GetSongs(int? pageNumber, int? pageSize)
        {
            var songs = (from s in _context.Songs.
                    OrderBy(a => a.SongId)
                         select s).AsQueryable();

            int currentPage = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 5;


            var items = songs.Skip((currentPage - 1) * currentPageSize).Take(currentPageSize).ToList();
            return items.AsQueryable();
        }
        [HttpGet("{searchSongQuery}")]
        public List<Songs> SearchSongs(string searchSongQuery)
        {
            var songs = _context.Songs.Where(s => s.SongName.StartsWith(searchSongQuery));
            return songs.ToList();
        }

        // GET: api/Songs1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Songs>> GetSongs(int id)
        {
            var songs = await _context.Songs.FindAsync(id);

            if (songs == null)
            {
                return NotFound();
            }

            return songs;
        }

        // PUT: api/Songs1/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSongs(int id, Songs songs)
        {
            if (id != songs.SongId)
            {
                return BadRequest();
            }

            _context.Entry(songs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            return NoContent();
        }

        // POST: api/Songs1
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Songs>> PostSongs(Songs songs)
        {
            _context.Songs.Add(songs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSongs", new { id = songs.SongId }, songs);
        }

        // DELETE: api/Songs1/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Songs>> DeleteSongs(int id)
        {
            var songs = await _context.Songs.FindAsync(id);
            if (songs == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(songs);
            await _context.SaveChangesAsync();

            return songs;
        }

        private bool SongsExists(int id)
        {
            return _context.Songs.Any(e => e.SongId == id);
        }
    }
}
