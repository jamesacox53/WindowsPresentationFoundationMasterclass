using Section_11___Notes_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section_11___Notes_App.ViewModel.Helpers.Database
{
    public interface IDatabaseHelper
    {
        public string DBPath { get; }

        public Task<bool> Insert<T>(T item);

        public Task<bool> Update<T>(T item) where T : IHasId;

        public Task<bool> Delete<T>(T item) where T : IHasId;

        public Task<List<T>> Read<T>() where T : new();
    }
}
