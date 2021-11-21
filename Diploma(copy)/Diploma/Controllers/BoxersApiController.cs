using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Diploma;
using Diploma.ViewModels.Boxers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Controllers
{
    [Route("api/boxers")]
    [ApiController]
    public class BoxersApiController : ControllerBase
    {
         private readonly BoxContext _context;
        private readonly IMapper _mapper;

        public BoxersApiController(BoxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet] // GET: /api/boxers
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoxerViewModel>))]  
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<BoxerViewModel>> GetBoxers()
        {
            var boxers = _mapper.Map<IEnumerable<Boxers>, IEnumerable<BoxerViewModel>>(_context.Boxers.ToList());
            return Ok(boxers);
        }
        
        
        
        [HttpGet("{id}")] // GET: /api/boxers/5
        [ProducesResponseType(200, Type = typeof(BoxerViewModel))]  
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            var boxer = _mapper.Map<BoxerViewModel>(_context.Boxers.FirstOrDefault(m => m.BoxerId == id));
            if (boxer == null) return NotFound();  
            return Ok(boxer);
        }
        
        [HttpPost] // POST: api/boxers
        public ActionResult<InputBoxerViewModel> PostBoxer(InputBoxerViewModel inputModel)
        {
            
            var boxer = _context.Add( _mapper.Map<Boxers>(inputModel)).Entity;
            _context.SaveChanges();

            return CreatedAtAction("GetById", new { id = boxer.BoxerId }, _mapper.Map<InputBoxerViewModel>(inputModel));
        }
        
        [HttpPut("{id}")] // PUT: api/boxers/5
        public IActionResult UpdateBoxer(int id, EditBoxerViewModel editModel)
        {
            try
            {
                var boxers = _mapper.Map<Boxers>(editModel);
                boxers.BoxerId = id;
                
                _context.Update(boxers);
                _context.SaveChanges();
                
                return Ok(_mapper.Map<EditBoxerViewModel>(boxers));
            }
            catch (DbUpdateException)
            {
                if (!BoxerExists(id))
                {
                    return BadRequest();
                }
                else
                {
                    throw;
                }
            }
        }
        
        [HttpDelete("{id}")] // DELETE: api/movie/5
        public ActionResult<DeleteBoxerViewModel> DeleteBoxer(int id)
        {
            var boxer = _context.Boxers.Find(id);
            if (boxer == null) return NotFound();  
            _context.Boxers.Remove(boxer);
            _context.SaveChanges();
            return Ok(_mapper.Map<DeleteBoxerViewModel>(boxer));
        }

        private bool BoxerExists(int id)
        {
            return _context.Boxers.Any(e => e.BoxerId == id);
        }
    }
}