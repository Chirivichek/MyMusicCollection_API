using DataAccess.Entities;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using MyMusicCollection_API.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MyMusicCollection_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingAndReviewController : ControllerBase
    {
        private DataAccess.Data.MusicCollectionBDcontext ctx;
        private readonly IMapper mapper;
        public RatingAndReviewController(IMapper mapper, MusicCollectionBDcontext ctx)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }
        [HttpGet("GetAllRatingsAndReviews")]
        public IActionResult GetAllRatingsAndReviews()
        {
            // Logic to get all ratings and reviews
            var ratingsAndReviews = ctx.RatingsAndReviews
                .Include(rr => rr.Album)
                .Include(rr => rr.User)
                .ToList();
            var result = mapper.Map<List<GetAllRatingAndReviewModel>>(ratingsAndReviews);
            return Ok(result);
        }
        [HttpGet("GetRatingAndReviewById/{id}")]
        public IActionResult GetRatingAndReviewById(int id)
        {
            // Logic to get a specific rating and review by ID
            var ratingAndReview = ctx.RatingsAndReviews
                .Include(rr => rr.Album)
                .Include(rr => rr.User)
                .FirstOrDefault(rr => rr.RatingAndReviewId == id);
            if (ratingAndReview == null)
            {
                return NotFound("Rating and review not found");
            }
            var result = mapper.Map<GetAllRatingAndReviewModel>(ratingAndReview);
            return Ok(result);
        }

        [HttpPost("CreateRatingAndReview")]
        public IActionResult CreateRatingAndReview(int UserId, int AlbumId, CreateRatingAndReviewModel model)
        {
            // Logic to create a new rating and review
            var user = ctx.Users.Find(UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var album = ctx.Albums.Find(AlbumId);
            if (album == null)
            {
                return NotFound("Album not found");
            }
            var ratingAndReview = mapper.Map<RatingAndReview>(model);
            ratingAndReview.UserId = UserId;
            ratingAndReview.AlbumId = AlbumId;
            ctx.RatingsAndReviews.Add(ratingAndReview);
            ctx.SaveChanges();
            return Ok("Rating and review created successfully");

        }
        [HttpPut("UpdateRatingAndReview/{id}")]
        public IActionResult UpdateRatingAndReview(int id, UpdateRatingAndReviewModel model)
        {
            // Logic to update an existing rating and review
            var ratingAndReview = ctx.RatingsAndReviews.Find(id);
            if (ratingAndReview == null)
            {
                return NotFound("Rating and review not found");
            }
            mapper.Map(model, ratingAndReview);
            ctx.SaveChanges();
            return Ok("Rating and review updated successfully");
        }
        [HttpDelete("DeleteRatingAndReview/{id}")]
        public IActionResult DeleteRatingAndReview(int id)
        {
            // Logic to delete a rating and review
            var ratingAndReview = ctx.RatingsAndReviews.Find(id);
            if (ratingAndReview == null)
            {
                return NotFound("Rating and review not found");
            }
            ctx.RatingsAndReviews.Remove(ratingAndReview);
            ctx.SaveChanges();
            return Ok("Rating and review deleted successfully");
        }
    }
}
