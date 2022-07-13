using System;
using System.ComponentModel.DataAnnotations;

namespace App.Repository.Entities
{
	public class UserSession
	{
		[Key]
		public long Id { get; set; }
		public long UserId { get; set; }
		public string? InputComplete { get; set; }
		public string? UrlComplete { get; set; }
		public string? CategoryComplete { get; set; }
		public bool? PrintFirstComplete { get; set; }
		public bool? PrintSecondComplete { get; set; }
		public bool? PrintThirdComplete { get; set; }
		public bool? PrintFourthComplete { get; set; }
	}
}