﻿using API_emezu.App_Start;
using API_emezu.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API_emezu.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsuarioController : ApiController
    {
        SqlConnection con = conexao.obterConexao();
        private static List<Usuario> usuarios = new List<Usuario>();

        public HttpResponseMessage Get()
        {
            List<Usuario> usuarios = new List<Usuario>();
            string query = "SELECT Id, nome, endereco, telefone FROM usuario";
            SqlCommand cmd = new SqlCommand(query, con);
            conexao.obterConexao();
            cmd.CommandType = CommandType.Text;
            //cmd.ExecuteNonQuery();     
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable usuario = new DataTable();
            if (da.TableMappings.Count >= 0)
            {
                da.Fill(usuario);
                foreach (DataRow row in usuario.Rows)
                {
                    Usuario usu = new Usuario()
                    {
                        Id = row["id"] == DBNull.Value ? 0 : Convert.ToInt32(row["id"]),
                        nome = row["nome"] == DBNull.Value ? string.Empty : row["nome"].ToString(),
                        endereco = row["endereco"] == DBNull.Value ? string.Empty : row["endereco"].ToString(),
                        telefone = row["telefone"] == DBNull.Value ? string.Empty : row["telefone"].ToString()
                    };

                    usuarios.Add(usu);
                }
            }
            else
            {
                usuario = null;
            }
            return Request.CreateResponse(HttpStatusCode.OK, usuarios.ToArray());
        }
        public DataTable Get(int Id)
        {
            string query = "SELECT Id, nome, endereco, telefone FROM usuario WHERE Id="+Id;
            SqlCommand cmd = new SqlCommand(query, con);
            conexao.obterConexao();
            cmd.CommandType = CommandType.Text;
            //cmd.ExecuteNonQuery();     
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable usuario = new DataTable();
            if (da.TableMappings.Count >= 0)
            {
                Convert.ToString(usuario);
                da.Fill(usuario);
            }
            else
            {
                usuario = null;
            }
            return usuario;
        }

        public void Post(Usuario user)
        {
            string query = "INSERT INTO usuario(nome, endereco, telefone) VALUES ('"+user.nome+"','"+user.endereco+"','"+user.telefone+"');";
            SqlCommand cmd = new SqlCommand(query, con);
            conexao.obterConexao();
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            conexao.fecharConexao();
            //usuarios.Add(user);
        }

        public IHttpActionResult Delete(int Id)
        {
            if (Id <= 0)
                return BadRequest("Not a valid id");
            string query = "DELETE FROM usuario WHERE Id=" + Id;
            SqlCommand cmd = new SqlCommand(query, con);
            conexao.obterConexao();
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            conexao.fecharConexao();
            //Usuario usuario = usuarios.Find(delegate(Usuario us) { return us.Id== Id; });
            //usuarios.Remove(usuario);
            if (true)
            {
                return Ok("Excluido com Sucesso!");
            }
            else
            {
                return NotFound();
            }
        }
        public HttpResponseMessage PutUsuario(int id, Usuario usuario)
        {
            string query = "UPDATE usuario SET (nome, endereco, telefone) VALUES ('" + usuario.nome + "','" + usuario.endereco + "','" + usuario.telefone + "');";
            Convert.ToInt32(usuario.nome);
            Convert.ToString(usuario.endereco);
            Convert.ToString(usuario.telefone);
            SqlCommand cmd = new SqlCommand(query, con);
            conexao.obterConexao();
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            conexao.fecharConexao();
            return Request.CreateResponse(HttpStatusCode.OK);
            
        }
    }
}