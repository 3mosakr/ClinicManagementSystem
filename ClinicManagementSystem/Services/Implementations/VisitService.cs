using System.Globalization;
using AutoMapper;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repository.Interfaces;
using ClinicManagementSystem.Services.Interfaces;
using ClinicManagementSystem.ViewModel.Visit;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagementSystem.Services.Implementations
{
    public class VisitService : IVisitService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public VisitService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		// Get all visits
		public List<VisitViewModel> GetAllVisits()
		{

			var visits = _unitOfWork.VisitRepository.GetAllWithDetails();
			return _mapper.Map<List<VisitViewModel>>(visits);

		}

		// Get visit details
		public VisitDetailsViewModel GetVisitDetails(int id)
		{
			var visit = _unitOfWork.VisitRepository.GetVisitWithAppointment(id);
			return _mapper.Map<VisitDetailsViewModel>(visit);
		}

		// Get visit for edit
		public VisitViewModel GetVisitForEdit(int id)
		{
			var visit = _unitOfWork.VisitRepository.GetVisitWithAppointment(id);
			return _mapper.Map<VisitViewModel>(visit);
		}

		// Create visit
		public void CreateVisit(VisitViewModel vm)
		{
			if (vm.VisitDate == null)
				vm.VisitDate = DateTime.Now;

			var visit = _mapper.Map<Visit>(vm);
			_unitOfWork.VisitRepository.Add(visit);
			_unitOfWork.Save();

		}

		// Update visit
		public void UpdateVisit(VisitViewModel vm)
		{
			var existing = _unitOfWork.VisitRepository.GetById(vm.Id);
			if (existing == null) return;

			_mapper.Map(vm, existing);
			_unitOfWork.VisitRepository.Update(existing);
			_unitOfWork.Save();

		}

		// Delete visit
		public void DeleteVisit(int id)
		{
			var visit = _unitOfWork.VisitRepository.GetById(id);
			if (visit == null) return;

			_unitOfWork.VisitRepository.Delete(visit);
			_unitOfWork.Save();
		}

		// Get appointment select list (Create)
		public List<SelectListItem> GetAppointmentsSelectList()
		{
			var appointments = _unitOfWork.VisitRepository.GetAppointmentsWithDetails();

			return appointments.Select(a => new SelectListItem
			{
				Value = a.Id.ToString(),
				Text = $"{a.Patient.FullName} - {a.Doctor.FullName} ({a.Date:yyyy-MM-dd HH:mm})"
			}).ToList();
		}

		// Get appointment select list (Edit)
		public List<SelectListItem> GetAppointmentsSelectListForEdit(int currentAppointmentId)
		{
			var appointments = _unitOfWork.VisitRepository.GetAppointmentsForEdit(currentAppointmentId);

			return appointments.Select(a => new SelectListItem
			{
				Value = a.Id.ToString(),
				Text = $"{a.Patient.FullName} - {a.Doctor.FullName} ({a.Date:yyyy-MM-dd HH:mm})",
				Selected = a.Id == currentAppointmentId
			}).ToList();
		}
	}
}
