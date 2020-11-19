﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamXTicTacToe.TicTacToe;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamXTicTacToe.Controllers
{
    [Route("players")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private IPlayerDAO playerDAO;

        public PlayerController(IPlayerDAO playerDAO)
        {
            this.playerDAO = playerDAO;
        }

        //GET players/Bob
        [HttpGet("{id}")]
        public Task<Player> GetPlayer(string id)
        {           
            return playerDAO.GetPlayer(id); ;
        }
        
        // POST api/<PlayerController>
        [HttpPost()]
        public async Task<IActionResult> CreatePlayer([FromBody] string id)
        {
            Player player = new Player();
            player.Id = id;
            if (await playerDAO.CreatePlayer(player)) // Player successfully created
            {
                return Ok(player);
            }
            else // Failed to create player
            {
                return BadRequest();
            }
        }

        // PUT api/<PlayerController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(string id, [FromBody] Player player)
        {
            if (id != player.Id) return BadRequest(); //ID from URI and payload don't match

            if (await playerDAO.UpdatePlayer(player)) // Player successfully updated
            {
                return Ok(player);
            }
            else // Failed to update player
            {
                return BadRequest();
            }
        }
    }
}
