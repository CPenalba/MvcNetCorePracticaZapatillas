using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MvcNetCorePracticaZapatillas.Data;
using MvcNetCorePracticaZapatillas.Models;
using System.Data;

#region
//CREATE PROCEDURE SP_GRUPO_IMAGENES_ZAPATILLA_OUT
//(@posicion int, @idproducto int, @registros int out)
//AS
//SELECT @registros = COUNT(IDIMAGEN) FROM IMAGENESZAPASPRACTICA WHERE IDPRODUCTO = @idproducto
//select IDIMAGEN, IDPRODUCTO, IMAGEN  FROM (SELECT ROW_NUMBER() OVER (ORDER BY IDPRODUCTO) AS POSICION, IDIMAGEN, IDPRODUCTO, IMAGEN FROM IMAGENESZAPASPRACTICA
//WHERE IDPRODUCTO = @idproducto) QUERY
//WHERE POSICION = @posicion
//GO
#endregion
namespace MvcNetCorePracticaZapatillas.Repositories
{
    public class RepositoryZapatillas
    {
        private ZapatillaContext context;

        public RepositoryZapatillas(ZapatillaContext context)
        {
            this.context = context;
        }

        public async Task<List<Zapatilla>> GetZapatillasAsync()
        {
            return await this.context.Zapatillas.ToListAsync();
        }

        public async Task<Zapatilla> FindZapatillaAsync(int idZapatilla)
        {
            var consulta = from datos in this.context.Zapatillas where datos.IdProducto == idZapatilla select datos;
            return await consulta.FirstOrDefaultAsync();
        }

        public async Task<ModelImagenesZapatilla> GetImagenesZapatillaOutAsync(int posicion, int idProducto)
        {
            string sql = "SP_GRUPO_IMAGENES_ZAPATILLA_OUT @posicion, @idproducto, @registros out";
            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamIdProducto = new SqlParameter("@idproducto", idProducto);
            SqlParameter pamRegistros = new SqlParameter("@registros", 0);
            pamRegistros.Direction = ParameterDirection.Output;

            Zapatilla zapa = await this.FindZapatillaAsync(idProducto);
            var consulta = this.context.ImagenesZapatillas.FromSqlRaw(sql, pamPosicion, pamIdProducto, pamRegistros);
            List<ImagenZapatilla> imagenes = await consulta.ToListAsync();
            int registros = int.Parse(pamRegistros.Value.ToString());
            return new ModelImagenesZapatilla
            {
                ImagenesZapatilla = imagenes.FirstOrDefault(),
                NumeroRegistros = registros,
                Zapatilla = zapa
            };
        }
    }
}
