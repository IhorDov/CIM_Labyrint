using System;
using System.Collections.Generic;
using System.Text;

namespace database
{
   public interface IAdventurerRepository
    {
        void AddAdmin(int buget);


         Character GetAllAdmin(int buget);

        

        void Open();

        void Close();
    }
}
