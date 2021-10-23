using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidacijaZklj
{
	public class Primer : INotifyPropertyChanged, IDataErrorInfo
	{
		private int _broj;
		public int Broj 
		{ 
			get => _broj; 
			set
			{
				_broj = value;
				Promena("Broj");
			}
		}


		private string _tekst = "qwe";
		public string Tekst 
		{ 
			get => _tekst; 
			set
			{
				_tekst = value;
				Promena("Tekst");
			}
		}

		public string Error => throw new NotImplementedException();

		private PrimerValidator _validator = new();

		public string this[string columnName]
		{
			get
			{
				var rez = _validator.Validate(this);

				var greska =
					rez.Errors.Where(err => err.PropertyName == columnName).FirstOrDefault();
				/*
				if (greska is null)
					return string.Empty;
				else
					return greska.ErrorMessage;*/
				return greska is null ? string.Empty : greska.ErrorMessage;
			}
		}

		private void Promena(string naziv)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(naziv));

		public event PropertyChangedEventHandler PropertyChanged;
	}

	public class PrimerValidator : AbstractValidator<Primer>
	{
		public PrimerValidator()
		{
			RuleFor(primer => primer.Broj).InclusiveBetween(1, 10).WithMessage("Joook");
			RuleFor(primer => primer.Tekst).NotEmpty().WithMessage("Jok prazan")
				.MinimumLength(4).WithMessage("Jok premaleno");
		}

	}
}
