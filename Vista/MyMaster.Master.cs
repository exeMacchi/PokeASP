﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Vista
{
    public partial class MyMaster : System.Web.UI.MasterPage
    {
        public List<Pokemon> Pokemons { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Esto verifica si ya existe una lista de Pokemons en sesión
            if (!(Session["Pokemons"] != null))
            {
                Session["Pokemons"] = PokemonBBL.GetPokemons();
            }
            Pokemons = (List<Pokemon>)Session["Pokemons"];
        }

        // Esto ocurre antes que el Page_Load() de la página solicitada.
        protected void Page_Init(object sender, EventArgs e)
        {
            // Si no es alguna de las páginas predeterminadas...
            if (!(Page is Login || Page is Register || Page is Default || Page is PokeDetail))
            {
                // y no hay una sesión activa, se obliga a loguear.
                if ((Session["UserSession"] == null))
                {
                    Session["AlertMessage"] = "No tienes los permisos necesarios para ingresar en esa página.";
                    Response.Redirect("/Pages/Auth/Login.aspx?alert=true");
                }
            }
        }

        /// <summary>
        /// Un usuario cierra su sesión.
        /// </summary>
        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/Default.aspx");
        }
    }
}