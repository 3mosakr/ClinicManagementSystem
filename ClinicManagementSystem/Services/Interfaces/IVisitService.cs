using ClinicManagementSystem.Models;
using ClinicManagementSystem.ViewModel.Visit;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagementSystem.Services.Interfaces
{
	public interface IVisitService
	{
		// CRUD Operations Visits
		List<VisitViewModel> GetAllVisits();
		VisitDetailsViewModel GetVisitDetails(int id);
		VisitViewModel GetVisitForEdit(int id);
		void CreateVisit(VisitCreateViewModel vm);
		void UpdateVisit(VisitViewModel vm);
		void DeleteVisit(int id);

		// Appointment Lists
		List<SelectListItem> GetAppointmentsSelectList(); // For Create
		List<SelectListItem> GetAppointmentsSelectListForEdit(int currentAppointmentId); // For Edit


	}
}
