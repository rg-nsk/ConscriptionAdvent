using ConscriptionAdvent.Domain.Enums;
using System;

namespace ConscriptionAdvent.Domain.DomainModels.Medicine
{
    public class Health : IEquatable<Health>
    {
        public const string DefaultAdditionalRequirementsTableGraphs = "1-10";
        public const string DefaultVision = "1,0/1,0";

        public MedicineRank MedicineRank { get; private set; }
        public string AdditionalRequirementsTableGraphs { get; private set; }
        public string DiseaseArticles { get; private set; }
        public string Vision { get; private set; }
        public BloodType BloodType { get; private set; }
        public VaccinationType VaccinationType { get; private set; }
        public DateTime VaccinationDate { get; private set; }

        private bool _isHaveVaccination;
        public bool IsHaveVaccination
        {
            get
            {
                switch (VaccinationType)
                {
                    case VaccinationType.K:
                    case VaccinationType.K_1:
                    case VaccinationType.K_2:
                        _isHaveVaccination = true;
                        break;
                    default:
                        _isHaveVaccination = false;
                        break;
                }
                return _isHaveVaccination;
            }
        }

        public Health(MedicineRank medicineRank,
            string additionalRequirementsTable,
            string diseaseArticles,
            string vision,
            BloodType bloodType,
            VaccinationType vaccinationType,
            DateTime vaccinationDate = default)
        {
            ChangeMedicineRank(medicineRank);
            ChangeAdditionalRequirementsTableGraphs(additionalRequirementsTable);
            ChangeDiseaseArticles(diseaseArticles);
            ChangeVision(vision);
            ChangeBloodType(bloodType);
            ChangeVaccinationType(vaccinationType);
            ChangeVaccinationDate(vaccinationDate);
        }

        public void ChangeMedicineRank(MedicineRank medicineRank)
        {
            MedicineRank = medicineRank;
        }

        public void ChangeAdditionalRequirementsTableGraphs(string additionalRequirementsTable)
        {
            AdditionalRequirementsTableGraphs = additionalRequirementsTable;
        }

        public void ChangeDiseaseArticles(string diseaseArticles)
        {
            DiseaseArticles = diseaseArticles;
        }

        public void ChangeVision(string vision)
        {
            if (string.IsNullOrWhiteSpace(vision))
            {
                throw new ArgumentNullException(nameof(vision));
            }

            Vision = vision;
        }

        public void ChangeBloodType(BloodType bloodType)
        {
            BloodType = bloodType;
        }

        public void ChangeVaccinationType(VaccinationType vaccinationType)
        {
            VaccinationType = vaccinationType;
        }

        public void ChangeVaccinationDate(DateTime issueDate)
        {
            VaccinationDate = issueDate;
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as Health;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return MedicineRank.GetHashCode() ^ AdditionalRequirementsTableGraphs.GetHashCode() ^
                   DiseaseArticles.GetHashCode() ^ Vision.GetHashCode() ^ BloodType.GetHashCode() ^ 
                   VaccinationType.GetHashCode() ^ VaccinationDate.GetHashCode();
        }

        public bool Equals(Health other)
        {
            if (other == null) return false;

            return MedicineRank == other.MedicineRank &&
                   AdditionalRequirementsTableGraphs == other.AdditionalRequirementsTableGraphs &&
                   DiseaseArticles == other.DiseaseArticles &&
                   Vision == other.Vision &&
                   BloodType == other.BloodType &&
                   VaccinationType == other.VaccinationType &&
                   VaccinationDate == other.VaccinationDate;
        }

        public static bool operator ==(Health left, Health right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(Health left, Health right)
        {
            return !(left == right);
        }

        #endregion
    }
}
