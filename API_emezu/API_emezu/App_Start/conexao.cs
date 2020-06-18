﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API_emezu.App_Start
{
    public class conexao
    {
        private static string str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\marqu\\Documents\\API_emerson.mdf;Integrated Security=True;Connect Timeout=30";
        private static SqlConnection con = null;
        public static SqlConnection obterConexao()
        {
            con = new SqlConnection(str);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();
            }
            catch (SqlException sqle)
            {
                con = null;
            }
            return con;
        }
        public static void fecharConexao()
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }
}