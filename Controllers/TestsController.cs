using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using si.dezo.test.DotNetAudit.Misc;
using si.dezo.test.DotNetAudit.Models;
using si.dezo.test.DotNetAudit.Persistence;

namespace si.dezo.test.DotNetAudit.Controllers {
    [Produces ("application/json")]
    [Route ("[controller]")]
    [ApiController]
    public class TestsController : ControllerBase {
        private readonly TestDbContext _context;

        public TestsController (
            TestDbContext context
        ) {
            this._context = context;
        }

        // To add 'New Publication', browse to:
        //  http://localhost:5000/tests/publication/add?name=New%20Publication
        [HttpGet ("publication/add")]
        public async Task<IActionResult> AddPublication (
            [FromQuery (Name = "name")] string name
        ) {
            var item = new Publication (0, name, DateTime.Now);
            await _context.Publications.AddAsync (item);
            await _context.SaveChangesAsync ();

            var result = await _context.Publications.FindAsync (item.Id);
            return new CreatedResult ($"bla/{result.Id}", result);
        }

        // To add an Article, browse to:
        //  http://localhost:5000/tests/article/add?type=ReviewArticle&title=Test%20article%202018&note=My%20test%20note&publicationId=1
        [HttpGet ("article/add")]
        public async Task<IActionResult> AddArticle (
            [FromQuery (Name = "type")] string typeStr, [FromQuery (Name = "title")] string title, [FromQuery (Name = "note")] string note, [FromQuery (Name = "publicationId")] int publicationId
        ) {
            if (!Enum.TryParse<ArticleType> (typeStr, true, out var type))
                type = ArticleType.UNKNOWN;
            if (typeStr == null || title == null || publicationId == 0 || type == ArticleType.UNKNOWN) {
                return new CustomErrorResource (
                    HttpStatusCode.BadRequest,
                    "One or more mandatory fields are missing or invalid");
            }

            var item = new Article (0, type, title, note, publicationId, DateTime.Now);
            _context.AddAuditCustomField ("AuditProcessAction", ProcessAction.AddedDirectly.ToString ());
            await _context.Articles.AddAsync (item);
            await _context.Publications.FindAsync (item.PublicationId);
            await _context.SaveChangesAsync ();

            var result = await _context.Articles.FindAsync (item.Id);
            return new CreatedResult ($"article/{result.Id}", result);
        }

        // To add an Article Proposal, browse to:
        //  http://localhost:5000/tests/proposal/add?type=ReviewArticle&title=Test proposal 1&note=Proposal%20note%201
        [HttpGet ("proposal/add")]
        public async Task<IActionResult> AddArticleProposal (
            [FromQuery (Name = "type")] string typeStr, [FromQuery (Name = "title")] string title, [FromQuery (Name = "note")] string note
        ) {
            if (!Enum.TryParse<ArticleType> (typeStr, true, out var type))
                type = ArticleType.UNKNOWN;
            if (typeStr == null || title == null || type == ArticleType.UNKNOWN) {
                return new CustomErrorResource (
                    HttpStatusCode.BadRequest,
                    "One or more mandatory fields are missing or invalid");
            }

            var item = new ArticleProposal (0, type, title, note, DateTime.Now);
            _context.AddAuditCustomField ("AuditProcessAction", ProcessAction.AddProposal.ToString ());
            await _context.ArticleProposals.AddAsync (item);
            await _context.SaveChangesAsync ();

            var result = await _context.ArticleProposals.FindAsync (item.Id);
            return new CreatedResult ($"proposal/{result.Id}", result);
        }

        // Accept the article proposal
        //  http://localhost:5000/tests/accept/1?publicationId=2
        [HttpGet ("accept/{id}")]
        public async Task<IActionResult> AcceptProposal (
            [FromQuery (Name = "publicationId")] int publicationId,
            int id
        ) {
            var proposal = await _context.ArticleProposals.FindAsync (id);
            if (proposal == null)
                return new CustomErrorResource (HttpStatusCode.NotFound, $"Cannot find specified article proposal ID={id}");
            var publication = await _context.Publications.FindAsync (publicationId);
            if (publication == null)
                return new CustomErrorResource (HttpStatusCode.NotFound, $"Invalid publication with ID={publicationId} specified");
            Article item = new Article (0, proposal.Type, proposal.Title, proposal.Note, publicationId, DateTime.Now);
            _context.AddAuditCustomField ("AuditProcessAction", ProcessAction.AcceptProposal.ToString ());
            _context.ArticleProposals.Remove (proposal);
            await _context.Articles.AddAsync (item);
            await _context.Publications.FindAsync (item.PublicationId);
            await _context.SaveChangesAsync ();

            var result = await _context.Articles
                .Include (d => d.Publication)
                .FirstOrDefaultAsync (d => d.Id == item.Id);
            return new CreatedResult ($"proposal/{result.Id}", result);
        }

        // Accept the article proposal
        //  http://localhost:5000/tests/audit-records
        [HttpGet ("audit-records")]
        public IActionResult AllAuditRecors () {
            var items = _context
                .Audit_Articles
                .OrderByDescending (d => d.AuditDt)
                .ToList ();
            return new OkObjectResult (items);
        }

    }
}