using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Models;
using RepositoryPattern.ViewModel;

namespace RepositoryPattern.IRepositories
{
    public interface IStudentRepository
    {
        Task<VmResponseMessage> CreateStudent(Student student);
        Task<List<Student>> GetStudentList();
        Task<VmResponseMessage> EditStudent(int? id, Student student);
        Task<Student> EditStudent(int? id);
        Task<VmResponseMessage> DeleteConfirmed(int? id);
        Task<Student> DeleteStudent(int? id);
    }
}
