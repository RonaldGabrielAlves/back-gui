using unifor_aep.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace unifor_aep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecentlyItemLostController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RecentlyItemLostController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select recently_lost_item.id, recently_lost_item.id_user, users.name, recently_lost_item.description, recently_lost_item.place_lost, DATE_FORMAT(recently_lost_item.date,'%d-%m-%Y')as date
                    from recently_lost_item join users on users.id = recently_lost_item.id_user
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UniforAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);

        }

        [HttpGet("{id}")]
        public ActionResult<RecentlyItemLost> Get(int id)
        {
            string query = @"
                    select id, id_user, description, place_lost, DATE_FORMAT(recently_lost_item.date,'%d-%m-%Y')as date
                    from recently_lost_item where id = @id
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UniforAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            RecentlyItemLost rli = new RecentlyItemLost
            {
                id = Convert.ToInt32(table.Rows[0]["id"]),
                id_user = Convert.ToInt32(table.Rows[0]["id_user"]),
                description = table.Rows[0]["description"].ToString(),
                place_lost = table.Rows[0]["place_lost"].ToString(),
                date = table.Rows[0]["date"].ToString(),
            };

            return Ok(rli);

        }

        [HttpPost]
        public IActionResult Post(RecentlyItemLost rli)
        {
            string query = @"
                    insert into recently_lost_item (id_user, description, place_lost, date) values
                    (@id_user, @description, @place_lost, @date);
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UniforAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    try
                    {
                        myCommand.Parameters.AddWithValue("@id_user", rli.id_user);
                        myCommand.Parameters.AddWithValue("@description", rli.description);
                        myCommand.Parameters.AddWithValue("@place_lost", rli.place_lost);
                        myCommand.Parameters.AddWithValue("@date", rli.date);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        mycon.Close();
                        return new JsonResult("Adicionado com Sucesso!");
                    }
                    catch (MySql.Data.MySqlClient.MySqlException)
                    {
                        //return new JsonResult("Não foi possível adicionar.  Email já existente, tente com outro email!");
                        return StatusCode(400, "Não foi possível adicionar.");
                    }
                }
            }


        }

        [HttpPut]
        public IActionResult Put(RecentlyItemLost rli)
        {
            string query = @"
                    update recently_lost_item set
                    id_user = @id_user,
                    description = @description,
                    place_lost = @place_lost,
                    date = @date
                    where id = @id;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UniforAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    try
                    {
                        myCommand.Parameters.AddWithValue("@id", rli.id);
                        myCommand.Parameters.AddWithValue("@id_user", rli.id_user);
                        myCommand.Parameters.AddWithValue("@description", rli.description);
                        myCommand.Parameters.AddWithValue("@place_lost", rli.place_lost);
                        myCommand.Parameters.AddWithValue("@date", rli.date);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        mycon.Close();
                        return new JsonResult("Editado com Sucesso!");
                    }
                    catch (MySql.Data.MySqlClient.MySqlException)
                    {
                        //return new JsonResult("Não foi possível Editar.  Email já existente, tente com outro email!");
                        return StatusCode(400, "Não foi possível Editar.");
                    }
                }
            }


        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string query = @"
                    delete from recently_lost_item
                    where id =@id;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UniforAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    try
                    {
                        myCommand.Parameters.AddWithValue("@id", id);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        mycon.Close();
                        return new JsonResult("Deletado com Sucesso!");
                    }
                    catch (MySql.Data.MySqlClient.MySqlException)
                    {
                        //return new JsonResult("Não foi possível deletar.  O cliente possui alugueis cadastrados!");
                        return StatusCode(400, "Não foi possível deletar.");
                    }
                }
            }


        }

    }
}
