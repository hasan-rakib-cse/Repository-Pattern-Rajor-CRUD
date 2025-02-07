using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RepositoryPattern.Data;
using RepositoryPattern.IRepositories;
using RepositoryPattern.Models;
using RepositoryPattern.ViewModel;

namespace RepositoryPattern.Services
{
    public class StudentService : IStudentRepository
    {
        private readonly AppDbContext _db;
        public StudentService(AppDbContext db)
        {
            _db = db;
        }

        //public async Task<VmResponseMessage> CreateStudent (VmStudent vm)
        //{
        //    var response = new VmResponseMessage();
        //    var student = new Student
        //    {
        //        Name = vm.Name,
        //        Email = vm.Email,
        //        Phone = vm.Phone,
        //        Subscribed = vm.Subscribed,
        //    };
        //    await _db.Student.AddAsync(student);
        //    await _db.SaveChangesAsync();
        //    response.Type = "Success";
        //    response.Message = "Supplier Create Successfully!";
        //    return response;
        //}

        public async Task<VmResponseMessage> CreateStudent(Student student)
        {
            var response = new VmResponseMessage();            
            await _db.Student.AddAsync(student);
            await _db.SaveChangesAsync();
            response.Type = "Success";
            response.Message = "Student Created Successfully!";
            return response;         
        }

        public async Task<List<Student>> GetStudentList()
        {
            var studentList = new List<Student>();
            studentList = await _db.Student.OrderByDescending(x => x.Id).ToListAsync();
            return studentList;
        }


        public async Task<Student> EditStudent(int? id)
        {
            if( id is null || _db.Student is null)
            {
                return null; // Returning NotFound() if student does not exist
            }
            var studentData = await _db.Student.FindAsync(id);
            if(studentData is null)
            {
                return null;
            }
            return studentData;
        }

        public async Task<VmResponseMessage> EditStudent(int? id, Student student)
        {
            var response = new VmResponseMessage();
            if (id == null || id != student.Id)
            {
                response.Type = "Error";
                response.Message = "Invalid student ID!";
                return response;
            }

            var existingStudent = await _db.Student.FindAsync(id);
            if(existingStudent is null)
            {
                response.Type = "Error";
                response.Message = "Student not found!";
                return response;
            }
            //var existStd = new Student()
            //{
            //    Name = student.Name,
            //    Email = student.Email,
            //    Phone = student.Phone,
            //    Subscribed = student.Subscribed,
            //};
            //_db.Update(existStd);

            existingStudent.Name = student.Name;
            existingStudent.Email = student.Email;
            existingStudent.Phone = student.Phone;
            existingStudent.Subscribed = student.Subscribed;
            _db.Update(existingStudent);

            await _db.SaveChangesAsync();
            response.Type = "Success";
            response.Message = "Student Updated Successfully!";
            return response;
        }

        public async Task<Student> DeleteStudent(int? id)
        {
            if (id is null || _db.Student is null)
            {
                return null;
            }
            var studentData = await _db.Student.FindAsync(id);
            if (studentData is null)
            {
                return null;
            }
            return studentData;
        }

        public async Task<VmResponseMessage> DeleteConfirmed (int? id)
        {
            var response = new VmResponseMessage();
            if (id == null)
            {
                response.Type = "Error";
                response.Message = "Invalid student ID!";
                return response;
            }
            var studentData = await _db.Student.FindAsync(id);
            if(studentData is null)
            {
                return null;
            }
            _db.Student.Remove(studentData);
            await _db.SaveChangesAsync();
            response.Type = "Success";
            response.Message = "Student Deleted Successfully!";
            return response;
        }

        public async Task<Student> DetailsStudent(int? id)
        {
            if(id  is null || _db.Student is null)
            {
                return null;
            }
            var studentData = await _db.Student.FindAsync(id);
            if(studentData is null)
            {
                return null;
            }
            return studentData;
        }
    }
}
