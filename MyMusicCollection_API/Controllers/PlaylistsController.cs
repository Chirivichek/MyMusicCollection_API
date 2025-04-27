using AutoMapper;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMusicCollection_API.Models;

namespace MyMusicCollection_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private DataAccess.Data.MusicCollectionBDcontext ctx;
        private readonly IMapper mapper;
        public PlaylistsController(IMapper mapper, MusicCollectionBDcontext ctx)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }
        [HttpGet("GetAllPlaylists")]
        public IActionResult GetAllPlaylists()
        {
            // Logic to get all playlists
            var playlists = ctx.PlayLists
                .Include(p => p.Tracks)
                .Include(p => p.User)
                .ToList();
            var result = mapper.Map<List<GetAllPlaylistModel>>(playlists);
            return Ok(result);
        }
        [HttpGet("GetPlaylistById/{id}")]
        public IActionResult GetPlaylistById(int id)
        {
            // Logic to get a specific playlist by ID
            var playlist = ctx.PlayLists
                .Include(p => p.Tracks)
                .Include(p => p.User)
                .FirstOrDefault(p => p.PlayListId == id);
            if (playlist == null)
            {
                return NotFound("Playlist not found");
            }
            var result = mapper.Map<GetAllPlaylistModel>(playlist);
            return Ok(result);
        }
        [HttpPost("GetAllTracksInPlaylist")]
        public IActionResult GetAllTracksInPlaylist(int playlistId)
        {
            // Logic to get all tracks in a specific playlist
            var playlist = ctx.PlayLists
                .Include(p => p.Tracks)
                .FirstOrDefault(p => p.PlayListId == playlistId);
            if (playlist == null)
            {
                return NotFound("Playlist not found");
            }
            var result = mapper.Map<List<GetAllTracksInPlaylistModel>>(playlist.Tracks);
            return Ok(result);
        }

        [HttpPost("CreatePlaylist")]
        public IActionResult CreatePlaylist(CreatePlaylistModel model)
        {
            // Logic to create a new playlist
            var playlist = mapper.Map<PlayList>(model);
            ctx.PlayLists.Add(playlist);
            ctx.SaveChanges();
            return Ok("Playlist created successfully");
        }
        [HttpPut("UpdatePlaylist/{id}")]
        public IActionResult UpdatePlaylist(int id, UpdatePlaylistModel model)
        {
            // Logic to update an existing playlist
            var playlist = ctx.PlayLists.Find(id);
            if (playlist == null)
            {
                return NotFound("Playlist not found");
            }
            mapper.Map(model, playlist);
            ctx.SaveChanges();
            return Ok("Playlist updated successfully");
        }
        [HttpPost("AddTrackToPlaylist/{PlaylistId}")]
        public IActionResult AddTrackToPlaylist(int PlaylistId, int TrackId)
        {
            // Logic to add a track to a playlist
            var playlist = ctx.PlayLists
                .Include(p => p.Tracks)
                .FirstOrDefault(p => p.PlayListId == PlaylistId);
            if (playlist == null)
            {
                return NotFound("Playlist not found");
            }
            var track = ctx.Tracks.Find(TrackId);
            if (track == null)
            {
                return NotFound("Track not found");
            }
            playlist.Tracks.Add(track);
            ctx.SaveChanges();
            return Ok("Track added to playlist successfully");
        }
        [HttpPost("DeleteTrackInPlaylist")]
        public IActionResult DeleteTrackInPlaylist(int PlaylistId, int TrackId)
        {
            // Logic to delete a track from a playlist
            var playlist = ctx.PlayLists
                .Include(p => p.Tracks)
                .FirstOrDefault(p => p.PlayListId == PlaylistId);
            if (playlist == null)
            {
                return NotFound("Playlist not found");
            }
            var track = playlist.Tracks.FirstOrDefault(t => t.TrackId == TrackId);
            if (track == null)
            {
                return NotFound("Track not found in the playlist");
            }
            playlist.Tracks.Remove(track);
            ctx.SaveChanges();
            return Ok("Track removed from playlist successfully");
        }

        [HttpDelete("DeletePlaylist/{id}")]
        public IActionResult DeletePlaylist(int id)
        {
            // Logic to delete a playlist
            var playlist = ctx.PlayLists.Find(id);
            if (playlist == null)
            {
                return NotFound("Playlist not found");
            }
            ctx.PlayLists.Remove(playlist);
            ctx.SaveChanges();
            return Ok("Playlist deleted successfully");
        }
    }
}
