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
    public class ItemLostController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ItemLostController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select item_lost.id, item_lost.id_user,
                    users.name, item_lost.name,
                    item_lost.color, item_lost.with_label,
                    item_lost.metal, item_lost.colored,
                    item_lost.broken, item_lost.dirty,
                    item_lost.opaque, item_lost.fragile,
                    item_lost.missing_parts, item_lost.heavy,
                    item_lost.with_pockets, item_lost.with_buttons,
                    item_lost.other,
                    item_lost.image, DATE_FORMAT(item_lost.date,'%d-%m-%Y')as date
                    from item_lost join users on users.id = item_lost.id_user
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
        public ActionResult<ItemLost> Get(int id)
        {
            string query = @"
                    select id, id_user, name,
                    color, with_label, metal,
                    colored, broken, dirty,
                    opaque, fragile, missing_parts,
                    heavy, with_pockets, with_buttons,
                    other, image, DATE_FORMAT(item_lost.date,'%d-%m-%Y')as date
                    from item_lost where id = @id
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

            ItemLost li = new ItemLost
            {
                id = Convert.ToInt32(table.Rows[0]["id"]),
                id_user = Convert.ToInt32(table.Rows[0]["id_user"]),
                name = table.Rows[0]["name"].ToString(),
                color = table.Rows[0]["color"].ToString(),
                with_label = Convert.ToBoolean(table.Rows[0]["with_label"]),
                metal = Convert.ToBoolean(table.Rows[0]["metal"]),
                colored = Convert.ToBoolean(table.Rows[0]["colored"]),
                broken = Convert.ToBoolean(table.Rows[0]["broken"]),
                dirty = Convert.ToBoolean(table.Rows[0]["dirty"]),
                opaque = Convert.ToBoolean(table.Rows[0]["opaque"]),
                fragile = Convert.ToBoolean(table.Rows[0]["fragile"]),
                missing_parts = Convert.ToBoolean(table.Rows[0]["missing_parts"]),
                heavy = Convert.ToBoolean(table.Rows[0]["heavy"]),
                with_pockets = Convert.ToBoolean(table.Rows[0]["with_pockets"]),
                with_buttons = Convert.ToBoolean(table.Rows[0]["with_buttons"]),
                other = table.Rows[0]["other"].ToString(),
                image = table.Rows[0]["image"].ToString(),
                date = table.Rows[0]["date"].ToString(),
            };

            return Ok(li);

        }

        [HttpPost]
        public IActionResult Post(ItemLost li)
        {
            string query = @"
                    insert into item_lost (name, color, with_label, metal, colored, broken, dirty, opaque, fragile, missing_parts, heavy, with_pockets, with_buttons, other, id_user, date, image) values
                    (@name, @color, @with_label, @metal, @colored, @broken, @dirty, @opaque, @fragile, @missing_parts, @heavy, @with_pockets, @with_buttons, @other, @id_user, @date, @image);
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
                        myCommand.Parameters.AddWithValue("@name", li.name);
                        myCommand.Parameters.AddWithValue("@color", li.color);
                        myCommand.Parameters.AddWithValue("@with_label", li.with_label);
                        myCommand.Parameters.AddWithValue("@metal", li.metal);
                        myCommand.Parameters.AddWithValue("@colored", li.colored);
                        myCommand.Parameters.AddWithValue("@broken", li.broken);
                        myCommand.Parameters.AddWithValue("@dirty", li.dirty);
                        myCommand.Parameters.AddWithValue("@opaque", li.opaque);
                        myCommand.Parameters.AddWithValue("@fragile", li.fragile);
                        myCommand.Parameters.AddWithValue("@missing_parts", li.missing_parts);
                        myCommand.Parameters.AddWithValue("@heavy", li.heavy);
                        myCommand.Parameters.AddWithValue("@with_pockets", li.with_pockets);
                        myCommand.Parameters.AddWithValue("@with_buttons", li.with_buttons);
                        myCommand.Parameters.AddWithValue("@other", li.other);
                        myCommand.Parameters.AddWithValue("@id_user", li.id_user);
                        myCommand.Parameters.AddWithValue("@date", li.date);
                        myCommand.Parameters.AddWithValue("@image", li.image);
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
        public IActionResult Put(ItemLost li)
        {
            string query = @"
                    update item_lost set
                    name = @name,
                    color = @color,
                    with_label = @with_label,
                    metal = @metal,
                    colored = @colored,
                    broken = @broken,
                    dirty = @dirty,
                    opaque = @opaque,
                    fragile = @fragile,
                    missing_parts = @missing_parts,
                    heavy = @heavy,
                    with_pockets = @with_pockets,
                    with_buttons = @with_buttons,
                    other = @other,
                    id_user = @id_user,
                    date = @date,
                    image = @image
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
                        myCommand.Parameters.AddWithValue("@id", li.id);
                        myCommand.Parameters.AddWithValue("@name", li.name);
                        myCommand.Parameters.AddWithValue("@color", li.color);
                        myCommand.Parameters.AddWithValue("@with_label", li.with_label);
                        myCommand.Parameters.AddWithValue("@metal", li.metal);
                        myCommand.Parameters.AddWithValue("@colored", li.colored);
                        myCommand.Parameters.AddWithValue("@broken", li.broken);
                        myCommand.Parameters.AddWithValue("@dirty", li.dirty);
                        myCommand.Parameters.AddWithValue("@opaque", li.opaque);
                        myCommand.Parameters.AddWithValue("@fragile", li.fragile);
                        myCommand.Parameters.AddWithValue("@missing_parts", li.missing_parts);
                        myCommand.Parameters.AddWithValue("@heavy", li.heavy);
                        myCommand.Parameters.AddWithValue("@with_pockets", li.with_pockets);
                        myCommand.Parameters.AddWithValue("@with_buttons", li.with_buttons);
                        myCommand.Parameters.AddWithValue("@other", li.other);
                        myCommand.Parameters.AddWithValue("@id_user", li.id_user);
                        myCommand.Parameters.AddWithValue("@date", li.date);
                        myCommand.Parameters.AddWithValue("@image", li.image);
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
                    delete from item_lost
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
