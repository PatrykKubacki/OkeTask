using System;
using System.ComponentModel.DataAnnotations;

namespace OkeTask.Models
{

	[Serializable]
	public class Country
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Nazwa państwa jest wymagana")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Nazwa stolicy jest wymagana")]
		public string Capital { get; set; }

		[Required(ErrorMessage = "Język jest wymagany")]
		public string Language { get; set; }

		[Required(ErrorMessage = "Liczba populacji jest wymagana i musi być liczbą"),
		Range(0, int.MaxValue, ErrorMessage = "Podaj liczbę dodatnią")]
		public int Population { get; set; }

		[Required(ErrorMessage = "Rozmiar powierzchni jest wymagany i musi być liczbą"),
		Range(0, int.MaxValue, ErrorMessage = "Podaj liczbę dodatnią")]
		public int Area { get; set; }
		
	}

}
