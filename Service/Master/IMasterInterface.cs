using Models;

namespace Service.Master
{
    public interface IMasterInterface
    {
        Task<List<CountryDTO>> getCountry();
        Task<List<StateDTO>> getState(int countryId);
        Task<List<CityDTO>> getCity(int stateId);
        Task<List<GenderDTO>> getGender(int genderId);
        Task<List<LanguageDTO>> getLanguage(int languageId);
        Task<List<QualificationDTO>> getQualification(int qualificationId);
        Task<List<SkillDTO>> getSkill(int skillId);
    }
}
