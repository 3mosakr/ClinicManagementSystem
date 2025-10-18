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

		public VisitService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public List<VisitViewModel> GetAllVisits()
		{
			var visits = _unitOfWork.VisitRepository.GetAllWithDetails();
			return visits.Select(v => new VisitViewModel
			{
				Id = v.Id,
				AppointmentId = v.AppointmentId,
				Diagnosis = v.Diagnosis,
				Prescription = v.Prescription,
				VisitDate = v.VisitDate,
				DoctorNotes = v.DoctorNotes,
				PatientName = v.Appointment?.Patient?.FullName,
				DoctorName = v.Appointment?.Doctor?.FullName
			}).ToList();
		}

		public VisitDetailsViewModel GetVisitDetails(int id)
		{
			var visit = _unitOfWork.VisitRepository.GetVisitWithAppointment(id);
			if (visit == null) return null;

			return new VisitDetailsViewModel
			{
				Id = visit.Id,
				AppointmentId = visit.AppointmentId,
				Diagnosis = visit.Diagnosis,
				Prescription = visit.Prescription,
				VisitDate = visit.VisitDate,
				DoctorNotes = visit.DoctorNotes,
				PatientName = visit.Appointment?.Patient?.FullName,
				DoctorName = visit.Appointment?.Doctor?.FullName,
			};
		}

		public void CreateVisit(VisitViewModel vm)
		{
			if (vm.VisitDate == null)
				vm.VisitDate = DateTime.Now;

			var visit = new Visit
			{
				AppointmentId = vm.AppointmentId,
				Diagnosis = vm.Diagnosis,
				Prescription = vm.Prescription,
				VisitDate = vm.VisitDate,
				DoctorNotes = vm.DoctorNotes
			};

			_unitOfWork.VisitRepository.Add(visit);
			_unitOfWork.Save();
		}

		public VisitViewModel GetVisitForEdit(int id)
		{
			var visit = _unitOfWork.VisitRepository.GetVisitWithAppointment(id);
			if (visit == null) return null;

			return new VisitViewModel
			{
				Id = visit.Id,
				AppointmentId = visit.AppointmentId,
				Diagnosis = visit.Diagnosis,
				Prescription = visit.Prescription,
				VisitDate = visit.VisitDate,
				DoctorNotes = visit.DoctorNotes
			};
		}

		public void UpdateVisit(VisitViewModel vm)
		{
			var visit = _unitOfWork.VisitRepository.GetById(vm.Id);
			if (visit == null) return;

			visit.AppointmentId = vm.AppointmentId;
			visit.Diagnosis = vm.Diagnosis;
			visit.Prescription = vm.Prescription;
			visit.VisitDate = vm.VisitDate;
			visit.DoctorNotes = vm.DoctorNotes;

			_unitOfWork.VisitRepository.Update(visit);
			_unitOfWork.Save();
		}

		public VisitViewModel GetVisitForDelete(int id)
		{
			var visit = _unitOfWork.VisitRepository.GetById(id);
			if (visit == null) return null;

			return new VisitViewModel
			{
				Id = visit.Id,
				AppointmentId = visit.AppointmentId,
				Diagnosis = visit.Diagnosis,
				Prescription = visit.Prescription,
				VisitDate = visit.VisitDate,
				DoctorNotes = visit.DoctorNotes
			};
		}

		public void DeleteVisit(int id)
		{
			var visit = _unitOfWork.VisitRepository.GetById(id);
			if (visit == null) return;

			_unitOfWork.VisitRepository.Delete(visit);
			_unitOfWork.Save();
		}

		public List<SelectListItem> GetAppointmentsSelectList(int? selectedAppointmentId = null)
		{
			var appointments = _unitOfWork.VisitRepository.GetAppointmentsWithDetails(selectedAppointmentId);
			return appointments.Select(a => new SelectListItem
			{
				Value = a.Id.ToString(),
				Text = $"{a.Patient.FullName} - {a.Doctor.FullName} ({a.Date:yyyy-MM-dd HH:mm})"
			}).ToList();
		}
	}
}
