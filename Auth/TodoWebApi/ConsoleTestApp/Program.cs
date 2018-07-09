using System;
using Todo.BL;

namespace ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new TodoServiceInMemory();
            WriteAll(service);

            //edit
            var dto1 = service.GetById(1);
            dto1.Description = "Edited description";            
            service.Save(dto1);

            //delete
            service.Delete(0);

            //create
            var dto2 = service.Create();
            dto2.Description = "Created item";
            dto2.Name = "Created item";
            dto2.DueDate = DateTime.Now;            
            service.Save(dto2);
            
            WriteAll(service);
            Console.ReadKey();
        }

        private static void WriteAll(ITodoService service)
        {
            foreach (var dto in service.GetAll())
            {
                Console.WriteLine(dto);
            }
            Console.WriteLine("--------------------");
        }
    }
}
