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
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select id, type, name, email from users 
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
        public ActionResult<User> Get(int id)
        {
            string query = @"
                    select id, type, name, email, password from users where id = @id
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

            User userr = new User
            {
                id = Convert.ToInt32(table.Rows[0]["id"]),
                type = Convert.ToBoolean(table.Rows[0]["type"]),
                name = table.Rows[0]["name"].ToString(),
                email = table.Rows[0]["email"].ToString(),
                password = table.Rows[0]["password"].ToString(),
            };

            return Ok(userr);

        }

        [HttpGet("by-email/{email}")]
        public ActionResult<User> GetEmail(string email)
        {
            string query = @"
        select id, type, name, email, password from users where email = @email
    ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UniforAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@email", email);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            if (table.Rows.Count == 0)
            {
                return NotFound("Usuário não encontrado.");
            }

            User userr = new User
            {
                id = Convert.ToInt32(table.Rows[0]["id"]),
                type = Convert.ToBoolean(table.Rows[0]["type"]),
                name = table.Rows[0]["name"].ToString(),
                email = table.Rows[0]["email"].ToString(),
                password = table.Rows[0]["password"].ToString(),
            };

            return Ok(userr);
        }


        [HttpPost]
        public IActionResult Post(User cli)
        {
            string query = @"
                    insert into users (type, name, email, password) values
                    (@type, @name, @email, @password);
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
                        myCommand.Parameters.AddWithValue("@type", cli.type);
                        myCommand.Parameters.AddWithValue("@name", cli.name);
                        myCommand.Parameters.AddWithValue("@email", cli.email);
                        myCommand.Parameters.AddWithValue("@password", cli.password);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        mycon.Close();
                        return new JsonResult("Adicionado com Sucesso!");
                    }
                    catch (MySql.Data.MySqlClient.MySqlException)
                    {
                        //return new JsonResult("Não foi possível adicionar.  Email já existente, tente com outro email!");
                        return StatusCode(400, "Não foi possível adicionar. Email já existente, tente com outro email!");
                    }
                }
            }


        }

        [HttpPut]
        public IActionResult Put(User cli)
        {
            string query = @"
                    update users set
                    type = @type,
                    name = @name,
                    email = @email,
                    password = @password
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
                        myCommand.Parameters.AddWithValue("@id", cli.id);
                        myCommand.Parameters.AddWithValue("@type", cli.type);
                        myCommand.Parameters.AddWithValue("@name", cli.name);
                        myCommand.Parameters.AddWithValue("@email", cli.email);
                        myCommand.Parameters.AddWithValue("@password", cli.password);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        mycon.Close();
                        return new JsonResult("Editado com Sucesso!");
                    }
                    catch (MySql.Data.MySqlClient.MySqlException)
                    {
                        //return new JsonResult("Não foi possível Editar.  Email já existente, tente com outro email!");
                        return StatusCode(400, "Não foi possível Editar. Email já existente, tente com outro email!");
                    }
                }
            }


        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string query = @"
                    delete from users
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
                        return StatusCode(400, "Não foi possível deletar.  O usuário possui itens cadastrados!");
                    }
                }
            }


        }

    }
}
