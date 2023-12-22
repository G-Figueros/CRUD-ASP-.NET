using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebInformacionDocenteGF.Models;
using System.Data;

namespace WebInformacionDocenteGF.Controllers
{
    public class ContactoGFController : Controller
    {
        private static string lStrConexion = ConfigurationManager.ConnectionStrings["cadena"].ToString();
        private static List<Contacto> lObjContactos= new List<Contacto>();


        // GET: ContactoGF
        public ActionResult Inicio()
        {
            lObjContactos = new List<Contacto>();
            using (SqlConnection lObjConnection = new SqlConnection(lStrConexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM CONTACTO", lObjConnection);
                cmd.CommandType = CommandType.Text;
                lObjConnection.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Contacto nuevoContacto = new Contacto();
                        nuevoContacto.IdContacto = Convert.ToInt32(dr["IdContacto"]);
                        nuevoContacto.Nombres = dr["Nombres"].ToString();
                        nuevoContacto.Apellidos = dr["Apellidos"].ToString();
                        nuevoContacto.Telefono = dr["Telefono"].ToString();
                        nuevoContacto.Salario = Math.Round(Convert.ToDecimal(dr["Salario"]), 2);
                        nuevoContacto.FechaNacimiento = DateTime.Parse(dr["FechaNacimiento"].ToString());
                        nuevoContacto.Correo = dr["Correo"].ToString();

                        lObjContactos.Add(nuevoContacto);
                    }
                }
            }
            return View(lObjContactos);
        }

        [HttpGet]
        public ActionResult Registrar()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int? lIntIdContacto)
        {
            if(lIntIdContacto == null)
            {
                return RedirectToAction("Inicio", "ContactoGF");
            }
            Contacto lObjEditar = lObjContactos.Where(c => c.IdContacto == lIntIdContacto).FirstOrDefault();
            return View(lObjEditar);
        }

        [HttpGet]
        public ActionResult Eliminar(int? lIntIdContacto)
        {
            if (lIntIdContacto == null)
            {
                return RedirectToAction("Inicio", "ContactoGF");
            }
            Contacto lObjEliminar = lObjContactos.Where(c => c.IdContacto == lIntIdContacto).FirstOrDefault();
            return View(lObjEliminar);
        }

        [HttpPost]
        public ActionResult Create(Contacto lObjContacto)
        {
            using (SqlConnection lObjConnection = new SqlConnection(lStrConexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Registrar", lObjConnection);
                cmd.Parameters.AddWithValue("Nombres", lObjContacto.Nombres);
                cmd.Parameters.AddWithValue("Apellidos", lObjContacto.Apellidos);
                cmd.Parameters.AddWithValue("Telefono", lObjContacto.Telefono);
                cmd.Parameters.AddWithValue("Salario", lObjContacto.Salario);
                cmd.Parameters.AddWithValue("FechaNacimiento", lObjContacto.FechaNacimiento);
                cmd.Parameters.AddWithValue("Correo", lObjContacto.Correo);
                cmd.CommandType = CommandType.StoredProcedure;
                lObjConnection.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Inicio", "ContactoGF");
        }

        [HttpPost]
        public ActionResult Update(Contacto lObjContacto)
        {
            using (SqlConnection lObjConnection = new SqlConnection(lStrConexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Editar", lObjConnection);
                cmd.Parameters.AddWithValue("IdContacto", lObjContacto.IdContacto);
                cmd.Parameters.AddWithValue("Nombres", lObjContacto.Nombres);
                cmd.Parameters.AddWithValue("Apellidos", lObjContacto.Apellidos);
                cmd.Parameters.AddWithValue("Telefono", lObjContacto.Telefono);
                cmd.Parameters.AddWithValue("Salario", lObjContacto.Salario);
                cmd.Parameters.AddWithValue("FechaNacimiento", lObjContacto.FechaNacimiento);
                cmd.Parameters.AddWithValue("Correo", lObjContacto.Correo);
                cmd.CommandType = CommandType.StoredProcedure;
                lObjConnection.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Inicio", "ContactoGF");
        }

        [HttpPost]
        public ActionResult Delete(Contacto lObjContacto)
        {
            using (SqlConnection lObjConnection = new SqlConnection(lStrConexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Eliminar", lObjConnection);
                cmd.Parameters.AddWithValue("IdContacto", lObjContacto.IdContacto);
                cmd.CommandType = CommandType.StoredProcedure;
                lObjConnection.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Inicio", "ContactoGF");
        }

        public ActionResult AcercaDe()
        {
            ViewBag.Message = "ACERCA DE LA APLICACIÓN";

            return View();
        }

        public ActionResult Contacto()
        {
            ViewBag.Message = "DETALLES";

            return View();
        }

    }
}