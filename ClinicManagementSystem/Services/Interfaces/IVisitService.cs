using ClinicManagementSystem.ViewModel.Visit;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagementSystem.Services.Interfaces
{
	public interface IVisitService
	{
		List<VisitViewModel> GetAllVisits();
		VisitDetailsViewModel GetVisitDetails(int id);
		void CreateVisit(VisitViewModel vm);
		VisitViewModel GetVisitForEdit(int id);
		void UpdateVisit(VisitViewModel vm);
		VisitViewModel GetVisitForDelete(int id);
		void DeleteVisit(int id);
		List<SelectListItem> GetAppointmentsSelectList(int? selectedAppointmentId = null);
	}
}
