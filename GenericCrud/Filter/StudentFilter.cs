
using GenericCrud.Filter.Base;
using GenericCrud.Filter.Interfaces;

namespace GenericCrud.Filter
{
    public class StudentFilter:DtoFilter
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

    }
}
