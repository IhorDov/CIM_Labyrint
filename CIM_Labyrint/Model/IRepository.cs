using System;
using System.Collections.Generic;
using System.Text;

namespace database
{
   public interface IRepository
    {
        void AddScore(int score);


         Character GetAllScore(int score);

        

        void Open();

        void Close();
    }
}
