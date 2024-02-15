using System.ComponentModel.DataAnnotations.Schema;
using NuGet.Protocol.Core.Types;

namespace MVC2024.Models
{
	public class VehiculoModelo
	{
		public int Id { get; set; }
		public string Matricula { get; set; }
		public string Color { get; set; }

		public SerieModelo Serie { get; set; }
		public int serieId { get; set; }

		//Para el ejercicio de tabla M:M
		[NotMapped]
		public List<int> ExtrasSelecionados { get; set; }
		public List<VehiculoExtraModelo> VehiculoExtras { get; set; }

	}
}
